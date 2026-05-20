using UnityEngine;

public class OpenDoorTriggerLanding : MonoBehaviour
{
    public Animator DoorAnim;
    public string OpenCloseAnimBoolName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            DoorAnim.SetBool(OpenCloseAnimBoolName, true);
        }
    }

}