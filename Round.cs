using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// class to handle round logic
public class Round : MonoBehaviour
{
    // initialize variables
    [SerializeField] GameObject ENEMY_ROUND;
    [SerializeField] GameObject LIVES;
    [SerializeField] GameObject MONEY;
    public static int round = 0;
    bool last_round = false;


    // show the current round and the path arros depending on the round
    public IEnumerator ShowRound()
    {

        // last round logic
        if (round == EnemyRound.ROUND_ARR.Count - 1)
        {
            last_round = true;
            gameObject.GetComponent<Text>().text = "Last Round!";
            gameObject.GetComponent<Text>().color = Color.blue;
            gameObject.GetComponent<Text>().enabled = true;
            yield return new WaitForSeconds(2f);
            gameObject.GetComponent<Text>().enabled = false;
            last_round = false;
        }
        // regular round
        else
        {
            gameObject.GetComponent<Text>().enabled = true;
            yield return new WaitForSeconds(2f);
            gameObject.GetComponent<Text>().enabled = false;
        }

        // first round (path arrows) to show user where start and exit are
        if (round == 0)
        {
            for (int i = 0; i < 3; ++i)
            {
                Debug.Log("bruh");
                foreach (Transform path_arrow in GameObject.Find("PathArrowParent").transform)
                {
                    path_arrow.gameObject.SetActive(true);
                }
                yield return new WaitForSeconds(1f);
                foreach (Transform path_arrow in GameObject.Find("PathArrowParent").transform)
                {
                    path_arrow.gameObject.SetActive(false);
                }
                yield return new WaitForSeconds(0.5f);
            }


            yield return new WaitForSeconds(1f);
        }
        round += 1;
    }

    // check if the user has won or lost
    void Update()
    {
        // don't do any of this if it is displaying last round stuff
        if (!last_round)
        {
            // set to win
            if (LIVES.GetComponent<Lives>().lives <= 0)
            {
                PlayerPrefs.SetInt("Win", 0);
                SceneManager.LoadScene("GameOver");

            }
            // set to lose
            else if (ENEMY_ROUND.GetComponent<EnemyRound>().spawn_done && GameObject.FindWithTag("Enemy") == null)
            {
                PlayerPrefs.SetInt("Win", 1);
                SceneManager.LoadScene("GameOver");
            }
            // show round
            else
            {
                gameObject.GetComponent<Text>().text = $"Round {round}";
                gameObject.GetComponent<Text>().color = Color.black;
            }
        }
    }
}
