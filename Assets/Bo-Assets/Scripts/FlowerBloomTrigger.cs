using UnityEngine;

public class FlowerBloomTrigger : MonoBehaviour
{
    private Animator animator;
    private bool hasBloomed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("No Animator found on this flower.");
        }
    }

    private void OnTriggerEnter(Collider other)
{
    Debug.Log("Something entered trigger: " + other.name);

    if (hasBloomed) return;

    if (other.CompareTag("Player"))
    {
        Debug.Log("Player detected!");
        animator.SetTrigger("Bloom");
        hasBloomed = true;
    }
}
}

