using UnityEngine;

public class FlowerBloomTrigger : MonoBehaviour
{
    private Animator animator;
    private bool hasBloomed = false;

    [Header("Bloom Audio")]
    public AudioSource bloomAudioSource;
    public AudioClip bloomSound;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("No Animator found on this flower.");
        }

        if (bloomAudioSource == null)
        {
            Debug.LogError("No Bloom AudioSource assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered trigger: " + other.name);

        if (hasBloomed) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");

            // Trigger bloom animation
            animator.SetTrigger("Bloom");

            // Play bloom sparkle sound
            if (bloomAudioSource != null && bloomSound != null)
            {
                bloomAudioSource.PlayOneShot(bloomSound);
            }

            hasBloomed = true;
        }
    }
}
