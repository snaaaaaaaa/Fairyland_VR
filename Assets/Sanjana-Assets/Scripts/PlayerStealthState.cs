using UnityEngine;

public class PlayerStealthState : MonoBehaviour
{
    public bool isHidden { get; private set; }

    public void SetHidden(bool hidden)
    {
        isHidden = hidden;
        Debug.Log("Hidden state: " + hidden);
    }

}

