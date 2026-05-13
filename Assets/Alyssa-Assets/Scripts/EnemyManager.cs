// https://gameswoods.medium.com/creating-an-enemy-spawn-manager-in-unity-f9baa507a69
// https://limboh27.medium.com/creating-enemy-spawn-manager-in-unity-58b716125a8d
// https://discussions.unity.com/t/spawning-outside-a-radius-and-inside-another-radius/761597/6


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    float randRad;
    float randDist;
    Vector3 enemyTarget;

    int enemiesToDefeated = 15;
    public int enemiesDefeated = 0;
    public int enemyChance = 200;

    public TMP_Text WinLoseText;
    public GameObject ExitPortal;

    bool gameOver = false;



    int playerLives = 5;
    public int playerHits = 0;

    // Update is called once per frame
    void Update()
    {
        // Set a random angle
        randRad = Random.Range(0, Mathf.PI * 2);

        // Set a random distance that will be within a particular range
        randDist = Random.Range(20, 30);

        // Set the coordinates for new potential spawning enemy
        transform.position = new Vector3(Mathf.Cos(randRad), 0, Mathf.Sin(randRad)) * randDist;

        if (!gameOver)
        {
            SpawnEnemy();
        }
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }

            ExitPortal.SetActive(true);

        }

        Debug.Log(enemiesDefeated);

        if (enemiesDefeated >= enemiesToDefeated)
        {
            Debug.Log("You win!");
            WinLoseText.text = "Congratulations! You defeated 15 enemies!";
            gameOver = true;

        }

        if (playerHits >= playerLives)
        {
            Debug.Log("You lose!");
            WinLoseText.text = "You were attacked by too many enemies!";
            gameOver = true;
        }


    }

    void SpawnEnemy()
    {
        // 1 in 50 chance of spawing enemy
        int rand = Random.Range(1, enemyChance);
        enemyTarget.Set(0, 1, 0);

        if (rand == 1)
        {
            // Calculate direction to (0, 0) and create rotation
            Vector3 directionToCenter = enemyTarget - transform.position;
            Quaternion facingRotation = Quaternion.LookRotation(directionToCenter);

            Instantiate(enemyPrefab, transform.position, facingRotation);
        }
    }
}
