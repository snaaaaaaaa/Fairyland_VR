using UnityEngine;

public class HideZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Match your existing trigger logic
        if (other.CompareTag("MainCamera") || other.name.Contains("Camera"))
        {
            PlayerStealthState state = other.GetComponentInParent<PlayerStealthState>();

            if (state != null)
            {
                state.SetHidden(true);
                Debug.Log("Entered hide zone");
            }
            else
            {
                Debug.Log("PlayerStealthState not found on parent");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera") || other.name.Contains("Camera"))
        {
            PlayerStealthState state = other.GetComponentInParent<PlayerStealthState>();

            if (state != null)
            {
                state.SetHidden(false);
                Debug.Log("Exited hide zone");
            }
        }
    }
}