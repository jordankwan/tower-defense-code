using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // initialize variables
    public static Transform[] waypoint_arr = new Transform[45345];
    public static int waypoint_count = 0;

    // get all the waypoints and put them in the array
    void Start()
    {
        foreach (Transform waypoint in transform)
        {
            waypoint_arr[waypoint_count++] = waypoint;
        }
    }
}
