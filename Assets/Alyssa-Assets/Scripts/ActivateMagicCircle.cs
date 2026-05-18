using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MiVRy;

public class ActivateMagicCircle : MonoBehaviour
{
    // Reference to the trigger input action from the Input Actions asset
    public InputActionReference triggerAction;

    // Reference to the SceneLoader script that will change the scene
    public GameObject magicCircle;
    public GameObject magicWandTip;
    public GameObject magicWandTrail;

    public Material magicWandActiveMaterial;
    public Material magicWandInactiveMaterial;



    // Called when this object becomes enabled
    private void OnEnable()
    {
        // Enable the input action so Unity starts listening for trigger input
        if (triggerAction != null)
        {
            triggerAction.action.Enable();
        }
    }

    // Called when this object becomes disabled
    private void OnDisable()
    {
        // Disable the input action when this object is not active
        if (triggerAction != null)
        {
            triggerAction.action.Disable();
        }
    }

    // Called once every frame
    private void Update()
    {
        // Check whether the trigger was pressed during this frame
        if (triggerAction != null && magicCircle != null && triggerAction.action.IsPressed())
        {


            magicCircle.SetActive(true);
            SetWandMaterial(magicWandActiveMaterial);
             magicWandTrail.SetActive(true);

            // MiVRy.OnInputAction_RightTrigger();

        }

        else
        {
            magicCircle.SetActive(false);
            SetWandMaterial(magicWandInactiveMaterial);
             magicWandTrail.SetActive(false);



        }
    }

    private void SetWandMaterial(Material materialName)
    {
        magicWandTip.GetComponent<MeshRenderer>().material = materialName;
    }
}