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

    [Header("Petal Colour Change")]
    [SerializeField] private Renderer[] petalRenderers;

    [SerializeField] private Color[] bloomColours = new Color[]
    {
        new Color(1f, 0.45f, 0.75f), // Pink
        new Color(1f, 0.85f, 0.25f), // Yellow
        new Color(0.45f, 0.75f, 1f), // Blue
        new Color(0.75f, 0.45f, 1f), // Purple
        new Color(1f, 0.55f, 0.25f)  // Orange
    };

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

            ChangePetalColour();

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

    private void ChangePetalColour()
    {
        if (petalRenderers == null || petalRenderers.Length == 0)
        {
            Debug.LogWarning("No petal renderers assigned.");
            return;
        }

        if (bloomColours == null || bloomColours.Length == 0)
        {
            Debug.LogWarning("No bloom colours assigned.");
            return;
        }

        Color chosenColour = bloomColours[Random.Range(0, bloomColours.Length)];

        foreach (Renderer petal in petalRenderers)
        {
            if (petal != null)
            {
                petal.material.color = chosenColour;
            }
        }
    }
}