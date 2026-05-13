using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public static PortalManager Instance;

    [Header("Pockets")]
    public Pocket leftPocket;
    public Pocket rightPocket;

    [Header("Portal")]
    public GameObject portalObject;
    public Animator portalAnimator;     // optional
    public AudioSource portalAudio;     // optional
    public ParticleSystem portalVFX;    // optional

    private bool portalOpen = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (portalObject != null)
            portalObject.SetActive(false);
    }

    public void CheckConditions()
    {
        bool bothFilled = leftPocket.isFilled && rightPocket.isFilled;
        bool noDuplicates = leftPocket.heldTag != rightPocket.heldTag;

        if (bothFilled && noDuplicates)
            OpenPortal();
        else
            ClosePortal();
    }

    void OpenPortal()
    {
        if (portalOpen) return; // already open, don't re-trigger
        portalOpen = true;

        portalObject.SetActive(true);

        if (portalAnimator != null)
            portalAnimator.SetTrigger("Open");

        if (portalAudio != null)
            portalAudio.Play();

        if (portalVFX != null)
            portalVFX.Play();

        Debug.Log("Portal opened!");
    }

    void ClosePortal()
    {
        if (!portalOpen) return; // already closed, don't re-trigger
        portalOpen = false;

        if (portalAnimator != null)
            portalAnimator.SetTrigger("Close");

        if (portalAudio != null)
            portalAudio.Stop();

        if (portalVFX != null)
            portalVFX.Stop();

        // Delay deactivation if you have a close animation
        // Otherwise just disable immediately
        portalObject.SetActive(false);

        Debug.Log("Portal closed.");
    }
}
