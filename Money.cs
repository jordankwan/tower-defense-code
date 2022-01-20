
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// class to handle money
public class Money : MonoBehaviour
{
    // intialize variables
    [SerializeField] public int money;

    // constantly update the money depending on the current money
    void Update()
    {
        gameObject.GetComponent<Text>().text = $"Money: {money}";
        gameObject.GetComponent<Text>().color = new Color32(74, 56, 13, 255);
    }
}
