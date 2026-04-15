using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Public variables to store the names of each scene
    // These should match exactly with your scene file names in Unity
    public string LandingSceneName;
    public string Scene1Name;
    public string Scene2Name;
    public string Scene3Name;
    public string Scene4Name;

    // This public function can be called by another script or a Unity event
    public void LoadNextScene()
    {
        // Get the name of the currently active scene
        string currentScene = SceneManager.GetActiveScene().name;

        // Check which scene is active, then load the next one
        if (currentScene == LandingSceneName)
        {
            SceneManager.LoadScene(Scene1Name);
        }
        else if (currentScene == Scene1Name)
        {
            SceneManager.LoadScene(Scene2Name);
        }
        else if (currentScene == Scene2Name)
        {
            SceneManager.LoadScene(Scene3Name);
        }
        else if (currentScene == Scene3Name)
        {
            SceneManager.LoadScene(Scene4Name);
        }
        else if (currentScene == Scene4Name)
        {
            SceneManager.LoadScene(LandingSceneName);
        }
    }
}