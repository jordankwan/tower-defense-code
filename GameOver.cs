using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// class to display if they have won or lost based on a variable from the game scene
public class GameOver : MonoBehaviour {

  // set text to win or lose
  void Start() 
  {
    gameObject.GetComponent<Text>().fontSize = 100;

    if (PlayerPrefs.GetInt("Score") == 1) 
    {
      gameObject.GetComponent<Text>().color = new Color32(20, 255, 20, 255);
      gameObject.GetComponent<Text>().text = $"you are still in business good job";
    }
    else
    {
      gameObject.GetComponent<Text>().color = Color.red;
      gameObject.GetComponent<Text>().text = $"the insects ate all your fresh fruit!\nyou must find another job";
    }
  }
}
