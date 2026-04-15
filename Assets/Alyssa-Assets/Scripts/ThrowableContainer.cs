using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// https://youtu.be/jVmqMy5vusU?si=zhjbP-2H9cqM3Bew

public class ThrowableContainer : MonoBehaviour
{



    public InputActionReference triggerActionLeft;

    public GameObject throwableItem;

    // List<Vector3> trackingPos = new List<Vector3>();
    // public float velocity = 1000f;

    public bool triggerPressed = false;
    public static bool pickedUp = false;
    public static GameObject parentHand;
    // Rigidbody rigidbody;

    private void OnEnable()
    {
        // Enable the input action so Unity starts listening for trigger input
        if (triggerActionLeft != null)
        {
            triggerActionLeft.action.Enable();
        }
    }

    // Called when this object becomes disabled
    private void OnDisable()
    {
        // Disable the input action when this object is not active
        if (triggerActionLeft != null)
        {
            triggerActionLeft.action.Disable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActionLeft != null && triggerActionLeft.action.IsPressed())
        {


            // magicCircle.SetActive(true);
            // SetWandMaterial(magicWandActiveMaterial);
            // // magicWandTrail.SetActive(true);

            // // MiVRy.OnInputAction_RightTrigger();
            triggerPressed = true;
            Debug.Log("trigger pressed");



        }

        else
        {
            // magicCircle.SetActive(false);
            // SetWandMaterial(magicWandInactiveMaterial);
            // // magicWandTrail.SetActive(false);

            triggerPressed = false;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("collision");
        Debug.Log(other.gameObject.tag);
        Debug.Log(triggerPressed);
        if (other.gameObject.tag == "Player" && triggerPressed && pickedUp == false)
        {
            pickedUp = true;
            Debug.Log("make bomb");

            parentHand = other.gameObject;

            var throwable = Instantiate(throwableItem, parentHand.transform.position, Quaternion.identity) as GameObject;
        }
    }
}