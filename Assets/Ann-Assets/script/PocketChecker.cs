using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pocket : MonoBehaviour
{
    [Header("Required Item Tags")]
    public string requiredTagA;   // e.g. "KeyStone" — collected first
    public string requiredTagB;   // e.g. "AncientCoin" — collected second

    [Header("References")]
    public WristBookController wristBook;   // drag in via Inspector

    public string heldTag { get; private set; } = "";
    public bool isFilled => heldTag != "";

    private XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();

        if (socket == null)
        {
            Debug.LogError($"[Pocket] No XRSocketInteractor found on {gameObject.name}!");
            return;
        }

        socket.selectEntered.AddListener(OnItemPlaced);
        socket.selectExited.AddListener(OnItemRemoved);
    }

    void OnDestroy()
    {
        if (socket != null)
        {
            socket.selectEntered.RemoveListener(OnItemPlaced);
            socket.selectExited.RemoveListener(OnItemRemoved);
        }
    }

    bool IsValidItem(string tag)
    {
        if (wristBook == null) return false;

        // Only accept TagA first
        if (wristBook.collectedCount == 0)
            return tag == requiredTagA;

        // Only accept TagB after TagA is collected
        if (wristBook.collectedCount == 1)
            return tag == requiredTagB;

        return false; // both collected, accept nothing
    }

    void OnItemPlaced(SelectEnterEventArgs args)
    {
        string tag = args.interactableObject.transform.tag;

        if (IsValidItem(tag))
        {
            heldTag = tag;
            Debug.Log($"[Pocket] {gameObject.name} received item: {tag}");

            // Tell wrist book an item was collected
            if (wristBook != null)
                wristBook.OnItemCollected();
            else
                Debug.LogWarning("[Pocket] WristBookController not assigned!");

            // Notify portal manager
            if (PortalManager.Instance != null)
                PortalManager.Instance.CheckConditions();
            else
                Debug.LogWarning("[Pocket] PortalManager instance not found!");
        }
        else
        {
            Debug.Log($"[Pocket] Item tag '{tag}' is not valid at this stage.");
        }
    }

    void OnItemRemoved(SelectExitEventArgs args)
    {
        string tag = args.interactableObject.transform.tag;

        if (tag == heldTag)
        {
            Debug.Log($"[Pocket] {gameObject.name} item removed: {tag}");
            heldTag = "";

            if (PortalManager.Instance != null)
                PortalManager.Instance.CheckConditions();
        }
    }
}
