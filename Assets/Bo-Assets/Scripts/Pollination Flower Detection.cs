using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PollenTarget : MonoBehaviour
{
    private Animator animator;
    private bool hasBloomed = false;

    [SerializeField] private GameObject pollenPrefab;
    [SerializeField] private Transform pollenSpawnPoint;

    [Header("Bloom Audio")]
    [SerializeField] private AudioSource bloomAudioSource;
    [SerializeField] private AudioClip bloomSound;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("No Animator found on this flower.");
        }

        if (pollenPrefab == null)
        {
            Debug.LogError("No pollen prefab assigned.");
        }

        if (pollenSpawnPoint == null)
        {
            Debug.LogError("No pollen spawn point assigned.");
        }

        if (bloomAudioSource == null)
        {
            Debug.LogError("No bloom AudioSource assigned.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (hasBloomed) return;

        Transform root = other.transform.root;

        if (!root.CompareTag("Pollen")) return;

        XRGrabInteractable grabInteractable = root.GetComponent<XRGrabInteractable>();

        if (grabInteractable == null) return;

        if (!grabInteractable.isSelected)
        {
            animator.SetTrigger("Bloom");

            if (bloomAudioSource != null && bloomSound != null)
            {
                bloomAudioSource.PlayOneShot(bloomSound);
            }

            hasBloomed = true;

            SpawnNewPollen();
            Destroy(root.gameObject);
        }
    }

    private void SpawnNewPollen()
    {
        if (pollenPrefab == null || pollenSpawnPoint == null) return;

        GameObject newPollen = Instantiate(
            pollenPrefab,
            pollenSpawnPoint.position,
            pollenSpawnPoint.rotation
        );

        Rigidbody rb = newPollen.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}