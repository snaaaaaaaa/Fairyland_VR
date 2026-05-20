
using UnityEngine;
// using static EnemyManager;

public class EnemyMove : MonoBehaviour
{
    public float speed = 1.5f;

    public EnemyManager enemyManager;
    Vector3 targetPosition;
    private bool hasBeenHit = false;
    public AudioSource damage;

    void Start()
    {
        // Find the EnemyManager in the scene if not already assigned
        if (enemyManager == null)
        {
            enemyManager = FindObjectOfType<EnemyManager>();
        }
        targetPosition.Set(0, 1, 0);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    // if enemy detects it has been hit it will destroy itself
    private void OnTriggerEnter(Collider other)
    {
        // Prevent counting the same enemy multiple times
        if (hasBeenHit)
            return;

        Debug.Log("enemy hit");
        // Check if the object entering is the player camera
        if (other.name.Contains("Projectile"))
        {
            Debug.Log("Kill yourself now");
            hasBeenHit = true;
            if (enemyManager != null)
            {
                enemyManager.enemiesDefeated++;
                damage.Play();
            }
            else
            {
                Debug.LogError("enemyManager is null!");
            }
            Destroy(gameObject);

        }

        if (other.name.Contains("Mushroom"))
        {
            hasBeenHit = true;
            Debug.Log("Mushroom hit");
            if (enemyManager != null)
            {
                enemyManager.playerHits++;
            }
            else
            {
                Debug.LogError("enemyManager is null!");

            }
            Destroy(gameObject);

        }
    }
}

