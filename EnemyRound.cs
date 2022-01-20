using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

// Enum for all the enemies
public enum EnemyType
{
    Fly,
    LadyBug,
    None,
    Connor,
}


// Class to handle enemy rounds
public class EnemyRound : MonoBehaviour
{
    // initialize variables
    [SerializeField] GameObject START_WAYPOINT;
    [SerializeField] GameObject FLY;
    [SerializeField] GameObject LADY_BUG;
    [SerializeField] GameObject CONNOR;
    [SerializeField] GameObject ROUND;

    // hold the rounds in a list of list
    public static List<List<string>> ROUND_ARR = new List<List<string>> {
      new List<string> {"l1 d3", "f5 d5.0", "l2 d1", "d1", "f3 d3.0", "d5"},
      new List<string> {"f5 d1.0", "l4 d0.5", "d0.5", "f3 d0.5", "d3"},
      new List<string> {"f5 d0.5", "l3 d0.3", "d3", "f2 d0.5", "l1 d0.2", "d4"},
      new List<string> {"f10 d0.25", "d10"},
      new List<string> {"c1 d0"},
    };


    public static int curr_round = 0;
    public bool spawn_done = false;
    string wave_reg = @"^((?<type_enemy>.)(?<amount_enemy>\d+) )?d(?<delay>\d+\.?\d*?)$";

    // start coroutine to spawn enemies
    void Start()
    {
        Debug.Log("text");
        Debug.Log(GameObject.Find("Round").GetComponent<Text>().text);
        StartCoroutine("StartSpawn");
    }

    // coroutine to spawn the enemies
    public IEnumerator StartSpawn()
    {

        foreach (List<string> round in ROUND_ARR)
        {

            yield return StartCoroutine(ROUND.GetComponent<Round>().ShowRound());

            foreach (string item in round)
            {
                Debug.Log(item);
                GroupCollection m = Regex.Match(item, wave_reg).Groups;
                EnemyType enemy_type = EnemyType.None;

                if (m["delay"].Value == "")
                {
                    Debug.Log("some error with item parsing");
                    Debug.Break();
                }


                // get the type of enemy from the string
                switch (m["type_enemy"].Value)
                {
                    case "f":
                        enemy_type = EnemyType.Fly;
                        break;
                    case "l":
                        enemy_type = EnemyType.LadyBug;
                        break;
                    case "c":
                        enemy_type = EnemyType.Connor;
                        break;
                    default:
                        break;
                }

                Debug.Log($"type: {enemy_type}");


                // check if the enemy is valid and start spawning however many of them
                if (enemy_type != EnemyType.None)
                {
                    for (int i = 0; i < int.Parse(m["amount_enemy"].Value); ++i)
                    {
                        GameObject enemy = null;
                        switch (enemy_type)
                        {
                            case EnemyType.Fly:
                                enemy = FLY;
                                break;
                            case EnemyType.LadyBug:
                                enemy = LADY_BUG;
                                break;
                            case EnemyType.Connor:
                                enemy = CONNOR;
                                break;
                            default:
                                break;
                        }

                        if (enemy != null)
                        {
                            enemy = Instantiate(enemy, START_WAYPOINT.transform.position, Quaternion.identity);
                            enemy.SetActive(true);
                        }
                        yield return new WaitForSeconds(float.Parse(m["delay"].Value));
                    }
                }
                else
                {
                    yield return new WaitForSeconds(float.Parse(m["delay"].Value));
                }

            }
        }

        // set spawning done to true so that we can display that they have won when all the enemies are gone
        spawn_done = true;
    }
}
