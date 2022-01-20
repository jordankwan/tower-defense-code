using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

// class to handle range logic
public class Range : MonoBehaviour
{
    // initialize variables
    public bool can_attack = false;
    List<Transform> col_list = new List<Transform>();

    // ignore mouse press to detect if user wants to click on employee to enable / disable range
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    // check if the collider detects an enemy and remove it from the collision list
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.transform != null)
        {
            col_list.Remove(collision.gameObject.transform);
        }
    }


    // check if the collider detects an enemy and add to collision list
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.transform != null)
        {
            col_list.Add(collision.gameObject.transform);
        }
    }

    // check if they collision list if not zero and set attack to true or false
    void Update()
    {
        if (col_list.Count != 0)
        {
            can_attack = true;
        }
        else
        {
            can_attack = false;
        }
    }
}
