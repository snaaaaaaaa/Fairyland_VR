// https://gameswoods.medium.com/creating-an-enemy-spawn-manager-in-unity-f9baa507a69
// https://limboh27.medium.com/creating-enemy-spawn-manager-in-unity-58b716125a8d
// https://discussions.unity.com/t/spawning-outside-a-radius-and-inside-another-radius/761597/6


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public GameObject groundEnemyPrefab;
    public GameObject flyingEnemyPrefab;
    public GameObject[] groundEnemyPrefabs;
    public GameObject[] flyingEnemyPrefabs;
    public float flyingSpawnHeight = 10f;
    public float flyingHeightVariance = 2f;
    public bool spawnEnabled = false;
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
        randDist = Random.Range(30, 40);

        // Set the coordinates for new potential spawning enemy
        transform.position = new Vector3(Mathf.Cos(randRad), 0, Mathf.Sin(randRad)) * randDist;

        if (!gameOver && spawnEnabled)
        {
            SpawnEnemy();
        }
        else if (gameOver)
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
            WinLoseText.text = "Congratulations! You defeated 15 enemies! Now lets go back home through the portal";
            gameOver = true;

        }

        if (playerHits >= playerLives)
        {
            Debug.Log("You lose!");
            WinLoseText.text = "You were attacked by too many enemies! Lets go back through the portal to rest";
            gameOver = true;
        }


    }

    void SpawnEnemy()
    {
        // 1 in enemyChance chance of spawning an enemy
        int rand = Random.Range(1, enemyChance);
        enemyTarget.Set(0, 1, 0);

        if (rand != 1)
        {
            return;
        }

        bool spawnFlying = Random.value < 0.5f;
        GameObject prefabToSpawn = null;

        if (spawnFlying)
        {
            if (flyingEnemyPrefabs != null && flyingEnemyPrefabs.Length > 0)
            {
                prefabToSpawn = flyingEnemyPrefabs[Random.Range(0, flyingEnemyPrefabs.Length)];
            }
            else
            {
                prefabToSpawn = flyingEnemyPrefab;
            }
        }
        else
        {
            if (groundEnemyPrefabs != null && groundEnemyPrefabs.Length > 0)
            {
                prefabToSpawn = groundEnemyPrefabs[Random.Range(0, groundEnemyPrefabs.Length)];
            }
            else
            {
                prefabToSpawn = groundEnemyPrefab;
            }
        }

        if (prefabToSpawn == null)
        {
            Debug.LogWarning("No enemy prefab set for spawning.");
            return;
        }

        Vector3 spawnPosition = transform.position;
        if (spawnFlying)
        {
            float randomHeight = flyingSpawnHeight + Random.Range(-flyingHeightVariance, flyingHeightVariance);
            spawnPosition.y = randomHeight;
        }
        else
        {
            spawnPosition.y = 0f;
        }

        Vector3 directionToCenter = enemyTarget - spawnPosition;
        Quaternion facingRotation = Quaternion.LookRotation(directionToCenter);

        Instantiate(prefabToSpawn, spawnPosition, facingRotation);
    }
}
