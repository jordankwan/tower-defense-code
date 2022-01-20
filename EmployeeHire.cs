using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// class for employee hiring (buying towers) 
public class EmployeeHire : MonoBehaviour
{
    // Initalize variables
    [SerializeField] GameObject EMPLOYEE;
    [SerializeField] GameObject RANGE;
    [SerializeField] public static GameObject[] green_clone_arr;
    [SerializeField] public static int green_clone_ind;
    [SerializeField] Text TEXT;
    [SerializeField] GameObject MONEY;
    GameObject ENEMY;
    Camera CAM;
    public GameObject green_clone;

    // change the text depending on the cost and such
    void Start()
    {
        CAM = Camera.main;
        TEXT.text = $"Hire {EMPLOYEE.name}\n${EMPLOYEE.GetComponent<Employee>().cost}";
        green_clone = null;
        green_clone_ind = 0;
        green_clone_arr = new GameObject[123423];
    }

    // function ran when they click to hire an employee
    public void Click()
    {
        if (MONEY.GetComponent<Money>().money >= EMPLOYEE.GetComponent<Employee>().cost)
        {
            green_clone = Instantiate(EMPLOYEE, new Vector3(0, 0, 0), Quaternion.identity);
            green_clone.SetActive(true);
            green_clone.GetComponent<Employee>().range_clone = Instantiate(RANGE, gameObject.transform.position, Quaternion.identity);

            green_clone.GetComponent<Employee>().range_clone.SetActive(true);
            green_clone.GetComponent<Employee>().SpawnRange();
        }
    }

    // cancel selection of employee
    void CancelSelect()
    {
        Destroy(green_clone.GetComponent<Employee>().range_clone);
        Destroy(green_clone);
        green_clone = null;
    }

    // place tower if they have the required money and it is a valid spot
    void PlaceTower()
    {
        Vector3 mouse_pos = CAM.ScreenToWorldPoint(Input.mousePosition);

        green_clone.transform.position = new Vector3(mouse_pos.x, mouse_pos.y, 0);
        green_clone.GetComponent<Employee>().range_clone.transform.position = new Vector3(mouse_pos.x, mouse_pos.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            if (green_clone.GetComponent<Employee>().can_place)
            {
                MONEY.GetComponent<Money>().money -= EMPLOYEE.GetComponent<Employee>().cost;
                green_clone.GetComponent<Employee>().placed = true;
                green_clone_arr[green_clone_ind++] = green_clone;
                green_clone = null;
            }

        }
    }

    // function ran every tick needed 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelSelect();
        }

        if (green_clone != null)
        {
            PlaceTower();
        }
    }
}
