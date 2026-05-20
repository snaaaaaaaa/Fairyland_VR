using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FacePlayerOnGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Transform playerCamera;

    private bool isGrabbed = false;

    [Header("Outline Object (child mesh)")]
    public GameObject outlineObject;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        playerCamera = Camera.main.transform;

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;

        if (outlineObject != null)
            outlineObject.SetActive(false);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;

        if (outlineObject != null)
            outlineObject.SetActive(true);
    }

    void Update()
    {
        if (!isGrabbed) return;

        Vector3 directionToPlayer =
            (playerCamera.position - transform.position).normalized;

        Quaternion targetRotation =
            Quaternion.FromToRotation(-transform.up, directionToPlayer) * transform.rotation;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * 10f
        );
    }
}