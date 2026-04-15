using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// This script detects when the player enters a trigger zone
public class ColliderTrigger : MonoBehaviour
{
    // Reference to your SceneLoader script
    // public SceneLoader sceneLoader;

    //public scene varaibales
    public string NextSceneName;
    public TextMeshPro textMeshPro;
   
    void Start() {
          textMeshPro.text = NextSceneName;
     }

    // This function is automatically called when another collider enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the player camera
        if (other.CompareTag("MainCamera") || other.name.Contains("Camera"))
        {
            Debug.Log("Player entered trigger zone");

            SceneManager.LoadScene(NextSceneName);
        }
    }

    
}