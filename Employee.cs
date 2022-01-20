using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for employee (tower) logic
public class Employee : MonoBehaviour
{
    // Initialize variables
    [SerializeField] GameObject RANGE;
    [SerializeField] public int cost;
    [SerializeField] public GameObject BULLET;
    [SerializeField] public float reload;

    GameObject ENEMY;
    public bool spawn_range = true;
    public bool can_place = false;
    public List<Transform> collide_list = new List<Transform>();
    public bool placed = false;

    public GameObject range_clone;

    // Edit the collider radius and constantly run the shoot function based on reload
    void Start()
    {
        transform.GetComponent<CircleCollider2D>().radius *= 0.75f;

        InvokeRepeating("Shoot", 0f, reload);
    }

    // function to make the bullet
    void Fire(GameObject green_clone)
    {
        var bullet_clone = Instantiate(green_clone.GetComponent<Employee>().BULLET, green_clone.transform.position, Quaternion.identity);
        bullet_clone.SetActive(true);

    }

    // function that makes the employee shoot if they are placed and there is an enemy within range
    void Shoot()
    {
        if (placed)
        {
            ENEMY = GameObject.FindWithTag("Enemy");
            if (gameObject != null && ENEMY != null && range_clone.GetComponent<Range>().can_attack)
            {
                Fire(gameObject);
            }
        }
    }

    // function to enable or disable the range
    public void SpawnRange()
    {
        range_clone.GetComponent<SpriteRenderer>().enabled = spawn_range;
    }


    // add collider to the list in order to check if you can place the tower there
    void OnTriggerEnter2D(Collider2D col)
    {
        collide_list.Add(col.gameObject.transform);
    }

    // remove collider to the list in order to check if you can place the tower there
    void OnTriggerExit2D(Collider2D col)
    {
        collide_list.Remove(col.gameObject.transform);
    }

    // function to check if you can place the tower
    public bool CanPlace()
    {
        foreach (Transform tra in collide_list)
        {
            if (tra != null)
            {
                GameObject go = tra.gameObject;
                if (go.tag == "Tower" || go.tag == "UI" || go.tag == "Path")
                {
                    return false;
                }
            }
        }

        return true;
    }

    // function to toggle the range if select another one or something similar
    void ToggleRange()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null && can_place)
        {
            if (hit.collider.gameObject == gameObject)
            {

                spawn_range = !spawn_range;

                SpawnRange();
            }
            else
            {
                spawn_range = false;
                SpawnRange();
            }
        }
        else
        {
            for (int i = 0; i < EmployeeHire.green_clone_ind; ++i)
            {
                if (EmployeeHire.green_clone_arr[i] != null)
                {
                    EmployeeHire.green_clone_arr[i].GetComponent<Employee>().spawn_range = false;
                    EmployeeHire.green_clone_arr[i].GetComponent<Employee>().SpawnRange();
                }
            }
        }
    }

    // function that is called every tick
    void Update()
    {
        can_place = CanPlace();

        if (can_place)
        {
            range_clone.GetComponent<SpriteRenderer>().color = new Color32(58, 58, 58, 165);
        }
        else
        {
            range_clone.GetComponent<SpriteRenderer>().color = new Color32(159, 58, 58, 165);
        }

        if (Input.GetMouseButtonDown(0))
        {
            ToggleRange();
        }
    }
}
