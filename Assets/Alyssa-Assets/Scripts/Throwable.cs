using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using static ThrowableContainer;


// https://youtu.be/jVmqMy5vusU?si=zhjbP-2H9cqM3Bew

public class Throwable : MonoBehaviour
{



    public InputActionReference triggerActionLeft;

    List<Vector3> trackingPos = new List<Vector3>();
    public float velocity = 1000f;

    bool triggerPressed = false;
    // bool pickedUp = false;
    // public GameObject parentHand;
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
            // pickedUp = true;



        }

        else
        {
            // magicCircle.SetActive(false);
            // SetWandMaterial(magicWandInactiveMaterial);
            // // magicWandTrail.SetActive(false);

            triggerPressed = false;
            // pickedUp = false;

        }


        if (ThrowableContainer.pickedUp == true)
        {
            GetComponent<Rigidbody>().useGravity = false; // turn off gravity

            // match hand movement
            transform.position = ThrowableContainer.parentHand.transform.position;
            transform.rotation = ThrowableContainer.parentHand.transform.rotation;

            if (trackingPos.Count > 15)
            {
                trackingPos.RemoveAt(0);
            }
            trackingPos.Add(transform.position);

            // float triggerRight = ...

            if (!triggerPressed) //trigger released
            {
                ThrowableContainer.pickedUp = false; //let go
                Vector3 direction = trackingPos[trackingPos.Count - 1] - trackingPos[0];
                GetComponent<Rigidbody>().AddForce(direction * velocity);
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Collider>().isTrigger = false; //has physics now
            }
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.tag == "hand" && triggerPressed)
    //     {
    //         pickedUp = true;
    //         parentHand = other.gameObject;
    //     }
    // }
}
