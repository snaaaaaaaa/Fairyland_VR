using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReturnToLandingScene : MonoBehaviour
{
    public InputActionReference landingSceneReturnButton;
    public string LandingSceneName;

    void Start()
    {
        Debug.Log("loaded");
    }

    private void OnEnable()
    {
        // Enable the input action so Unity starts listening for trigger input
        if (landingSceneReturnButton != null)
        {
            Debug.Log("listening");

            landingSceneReturnButton.action.Enable();
        }
    }

    // Called when this object becomes disabled
    private void OnDisable()
    {
        // Disable the input action when this object is not active
        if (landingSceneReturnButton != null)
        {
            Debug.Log("disabled");

            landingSceneReturnButton.action.Disable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("waiting");

        if (landingSceneReturnButton != null && landingSceneReturnButton.action.IsPressed())
        {
            Debug.Log("buttonpressed");

            SceneManager.LoadScene(LandingSceneName);

        }
    }
}
