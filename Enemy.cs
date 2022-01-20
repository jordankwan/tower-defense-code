using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to handle insects (enemies)
public class Enemy : MonoBehaviour
{
    // initialize variables
    [SerializeField] public int health;
    [SerializeField] public float speed = 0.01f;
    [SerializeField] public int value;
    [SerializeField] GameObject START_WAYPOINT;
    [SerializeField] GameObject LIVES;
    [SerializeField] GameObject MONEY;
    public bool slow = false;
    int waypoint_ind = 0;

    // disable and reenable the collider in order to reupdate collision detection and put the enemy at the start waypoint
    void Start()
    {
        transform.position = START_WAYPOINT.transform.position;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    // called every frame to check if they should go to the next waypoint, if they are done and to remove lives, or if they should be dead and disappear
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Waypoint.waypoint_arr[waypoint_ind].position, speed * Time.deltaTime);
        if (transform.position == Waypoint.waypoint_arr[waypoint_ind].position)
        {
            waypoint_ind += 1;
        }

        if (waypoint_ind == Waypoint.waypoint_count)
        {
            LIVES.GetComponent<Lives>().lives -= health;

            Destroy(gameObject);
        }
        if (health <= 0)
        {
            // Debug.Log($"dead {gameObject.name}");
            MONEY.GetComponent<Money>().money += value;
            Destroy(gameObject);
        }
    }
}
