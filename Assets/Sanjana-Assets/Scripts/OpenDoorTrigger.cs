using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    public Animator DoorAnim; 
    public string OpenCloseAnimBoolName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            DoorAnim.SetBool(OpenCloseAnimBoolName, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            DoorAnim.SetBool(OpenCloseAnimBoolName, false);
        }
    }
}