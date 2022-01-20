using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// handle main menu stuff
public class MainMenu : MonoBehaviour
{
    // function to load the menu scene when the click the menu button in the game
    public void Click()
    {
        SceneManager.LoadScene("Menu");
    }

}

