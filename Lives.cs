using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// class to handle lives
public class Lives : MonoBehaviour
{
    // Initialize variables
    [SerializeField] public int lives;
    [SerializeField] public GameObject bruh;

    // constantly update lives
    void Update()
    {
        gameObject.GetComponent<Text>().text = $"Fresh Fruits: {lives}";
        gameObject.GetComponent<Text>().color = new Color32(74, 56, 13, 255);
    }
}
