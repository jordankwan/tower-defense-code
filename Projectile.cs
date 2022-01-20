using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class to handle projectile logic
public class Projectile : MonoBehaviour
{
    // initialize some variables
    [SerializeField] GameObject ENEMY;
    [SerializeField] float speed;
    [SerializeField] public bool freeze;
    [SerializeField] public float slow_mult;
    [SerializeField] public float slow_time;
    int damage = 1;
    Vector3 move_vec;

    // make sure to ignore mouse detection when trying to toggle range for employee
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

    }

    // destroy projectile when it gets out of view
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // check if the object hit is an enemy
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {

            // apply slowness if hit by freeze
            if (freeze && ENEMY.GetComponent<Enemy>().slow == false)
            {
                StartCoroutine(Slow());
            }

            if (ENEMY != null)
            {

                ENEMY.GetComponent<Enemy>().health -= damage;
                Destroy(gameObject);
            }
        }
    }

    // aply slowness to the enemy a set amount of time
    IEnumerator Slow()
    {
        float orig_speed = ENEMY.GetComponent<Enemy>().speed;
        ENEMY.GetComponent<Enemy>().speed *= slow_mult;
        ENEMY.GetComponent<Enemy>().slow = true;
        yield return new WaitForSeconds(slow_time);
        ENEMY.GetComponent<Enemy>().speed = orig_speed;
        ENEMY.GetComponent<Enemy>().slow = false;
    }

    // function to determine which enemy to shoot
    void GetEnemy()
    {
        Vector3 current_pos = transform.position;
        ENEMY = GameObject.FindWithTag("Enemy");
    }

    // function to make the projectile move towards the enemy
    void MoveTowardsEnemy()
    {

        Vector3 pos_old = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, ENEMY.transform.position, speed * Time.deltaTime);
        move_vec = (transform.position - pos_old).normalized * speed;
        transform.right = ENEMY.transform.position - transform.position;
    }


    // function ran every tick
    void Update()
    {

        if (ENEMY != null)
        {
            GetEnemy();
            MoveTowardsEnemy();
        }
        else
        {
            transform.position += move_vec;
        }
    }
}
