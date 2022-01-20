using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// class for start game button
public class StartGame : MonoBehaviour
{
    // load the game when they click start button
    public void Click()
    {
        SceneManager.LoadScene("Game");
    }
}
