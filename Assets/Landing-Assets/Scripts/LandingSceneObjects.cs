using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingSceneObjects : MonoBehaviour
{
    public GameObject wandObject;
    [SerializeField] private GameObject wand;
    public GameObject[] rayInteractors;
    // public GameObject rayInteractorLeft;
    // public GameObject rayInteractorRight;

    public GameObject clover;
    public GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Start: rayInteractors Length = " + rayInteractors.Length);
        // for (int i = 0; i < rayInteractors.Length; i++)
        // {
        //     Debug.Log("rayInteractors[" + i + "] = " + rayInteractors[i].name);
        // }

        // enableWand(false);
        // defineRayInteractors();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
        defineRayInteractors(rayInteractors);

        // Check if the colliding object is tagged as gamecontroller (left or right hand controller)
        if (other.CompareTag("GameController"))
        {
            // Activate the specified GameObject
            if (wandObject != null)
            {
                enableWand(true);
            }
        }
    }

    public void enableWand(bool enable)
    {
        Debug.Log("enable wand now");

        // Debug.Log(rayInteractors.Length);

        defineRayInteractors(rayInteractors);

        wand.SetActive(enable);

        foreach (GameObject ray in rayInteractors)
        {
            ray.SetActive(enable);
        }

        // rayInteractorLeft.SetActive(enable);
        // rayInteractorRight.SetActive(enable);



        if (enable)
        {
            // one criteria for door met
        }


    }

    public void defineRayInteractors(GameObject[] rayInteractors)
    {
        rayInteractors = GameObject.FindGameObjectsWithTag("RayInteractor");
        Debug.Log("defineRayInteractors: rayInteractors Length = " + rayInteractors.Length);
        for (int i = 0; i < rayInteractors.Length; i++)
        {
            Debug.Log("rayInteractors[" + i + "] = " + rayInteractors[i].name);
        }
    }
}
