using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pocket : MonoBehaviour
{
    public string requiredTagA; // e.g., "KeyStone"
    public string requiredTagB; // e.g., "AncientCoin"

    public string heldTag { get; private set; } = ""; // tracks what's currently inside
    public bool isFilled => heldTag != "";

    private XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnItemPlaced);
        socket.selectExited.AddListener(OnItemRemoved);
    }

    bool IsValidItem(string tag) => tag == requiredTagA || tag == requiredTagB;

    void OnItemPlaced(SelectEnterEventArgs args)
    {
        string tag = args.interactableObject.transform.tag;
        if (IsValidItem(tag))
        {
            heldTag = tag;
            PortalManager.Instance.CheckConditions();
        }
    }

    void OnItemRemoved(SelectExitEventArgs args)
    {
        string tag = args.interactableObject.transform.tag;
        if (tag == heldTag)
        {
            heldTag = "";
            PortalManager.Instance.CheckConditions();
        }
    }
}
