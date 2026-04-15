
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 1f;

    Vector3 targetPosition;

    void Start()
    {
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
        // Check if the object entering is the player camera
        if ( other.name.Contains("Projectile"))
        {
            Debug.Log("Kill yourself now");
            Destroy (gameObject);

        }
    }
}

