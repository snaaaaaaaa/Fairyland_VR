using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandingSceneEventManager : MonoBehaviour
{
    public static LandingSceneEventManager Instance { get; private set; }

    public LandingSceneObjects LandingSceneObjects;

    public GameObject PICO;

    // Tracks whether this is the first time the Landing scene has been opened during this app run.
    public bool firstTimeLanding { get; private set; } = true;
    private static bool hasOpenedLandingBefore = false;

    // public GameObject wand;

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
        // Listen for sceneLoaded to detect when LandingScene opens
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LandingScene")
        {
            // Runtime-only: determine first open during this app run
            if (!hasOpenedLandingBefore)
            {
                firstTimeLanding = true;
                hasOpenedLandingBefore = true;
            }
            else
            {
                firstTimeLanding = false;
            }
        }
        // Ensure PICO is assigned by searching for a Player object if not set
        if (PICO == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player == null)
            {
                try
                {
                    player = GameObject.FindWithTag("Player");
                }
                catch
                {
                    player = null;
                }
            }

            if (player != null)
            {
                PICO = player;
            }
        }


        // if (wand == null)
        // {
        //     GameObject wandObject = GameObject.Find("Wand");
        //     if (wandObject == null)
        //     {
        //         try
        //         {
        //             wandObject = GameObject.FindWithTag("Wand");
        //         }
        //         catch
        //         {
        //             wandObject = null;
        //         }
        //     }

        //     if (wandObject != null)
        //     {
        //         wand = wandObject;
        //     }
        // }



        // If this is not the first time, apply non-first-open behavior
        if (!firstTimeLanding && PICO != null)
        {
            PICO.transform.position = new Vector3(-1.04f, 0, 1.7f);
            LandingSceneObjects.enableWand(true);
        }
        else
        {
            // Ensure wand is disabled on first open
            LandingSceneObjects.enableWand(false);
        }
    }
}
