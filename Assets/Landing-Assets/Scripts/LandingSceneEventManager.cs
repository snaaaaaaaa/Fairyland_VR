using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandingSceneEventManager : MonoBehaviour
{
    public static LandingSceneEventManager Instance { get; private set; }

    public GameObject PICO;

    private const string LandingCountKey = "LandingScene_OpenCount";
    private int lastIncrementFrame = -1;

    private void Awake()
    {
        // enforce singleton and persist this GameObject across scene loads
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Listen for both sceneLoaded and activeSceneChanged - use frame check to avoid double-counting.
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }
    }

    public void Start()
    {
        StartCoroutine(LoadMainMenuScene());
    }

    private IEnumerator LoadMainMenuScene()
    {
        // placeholder coroutine to preserve original behavior.
        // add your actual scene-loading logic here if needed.
        yield return null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LandingScene")
        {
            IncrementLandingOpenCount();
        }
    }

    private void OnActiveSceneChanged(Scene previous, Scene next)
    {
        if (next.name == "LandingScene")
        {
            IncrementLandingOpenCount();
        }
    }

    private void IncrementLandingOpenCount()
    {
        // avoid double increment if multiple callbacks happen in same frame
        if (Time.frameCount == lastIncrementFrame) return;

        int count = PlayerPrefs.GetInt(LandingCountKey, 0) + 1;
        PlayerPrefs.SetInt(LandingCountKey, count);
        PlayerPrefs.Save();
        lastIncrementFrame = Time.frameCount;

        Debug.Log($"LandingScene opened {count} time(s).");
    }

    // Public accessor
    public int GetLandingSceneOpenCount()
    {
        return PlayerPrefs.GetInt(LandingCountKey, 0);
    }
}