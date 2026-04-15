// https://gameswoods.medium.com/creating-an-enemy-spawn-manager-in-unity-f9baa507a69
// https://limboh27.medium.com/creating-enemy-spawn-manager-in-unity-58b716125a8d
// https://discussions.unity.com/t/spawning-outside-a-radius-and-inside-another-radius/761597/6


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    float randRad;
    float randDist;

    // Update is called once per frame
    void Update()
    {
        // Set a random angle
        randRad = Random.Range(0, Mathf.PI * 2);

        // Set a random distance that will be within a particular range
        randDist = Random.Range(20, 30);

        // Set the coordinates for new potential spawning enemy
        transform.position = new Vector3(Mathf.Cos(randRad), 0, Mathf.Sin(randRad)) * randDist;
        
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        // 1 in 50 chance of spawing enemy
        int rand = Random.Range(1, 100);
        if(rand == 1)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
