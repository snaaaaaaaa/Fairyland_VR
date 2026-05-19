using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingSceneObjects : MonoBehaviour
{
    public GameObject wandObject;
    [SerializeField] private GameObject wand;
    // public GameObject[] rayInteractorsArray;
    // public GameObject rayInteractorLeft;
    // public GameObject rayInteractorRight;

    public GameObject clover;
    public GameObject key;

    public GameObject bookMove;
    public GameObject bookGrab;
    public GameObject bookPocket;

    public GameObject bookExtend;
    public Animator bookAnimation;
    public AudioSource wandAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Start: rayInteractors Length = " + rayInteractors.Length);
        // for (int i = 0; i < rayInteractors.Length; i++)
        // {
        //     Debug.Log("rayInteractors[" + i + "] = " + rayInteractors[i].name);
        // }

        // rayInteractorsArray = [rayInteractorLeft, rayInteractorRight];
        // defineRayInteractors();
        enableWand(false);
        // bookContent();

    }

    // Update is called once per frame
    void Update()
    {
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     // Debug.Log("Collided with: " + other.gameObject.name);
    //     // defineRayInteractors(rayInteractorsArray);

    //     // Check if the colliding object is tagged as gamecontroller (left or right hand controller)
    //     if (other.CompareTag("GameController"))
    //     {
    //         // Activate the specified GameObject
    //         if (wandObject != null)
    //         {
    //             enableWand(true);
    //         }
    //     }
    // }

    public void enableWand(bool enable)
    {
        Debug.Log("enable wand now");

        // Debug.Log(rayInteractors.Length);

        // defineRayInteractors(rayInteractorsArray);

        wand.SetActive(enable);

        wandObject.SetActive(!enable);

        // foreach (GameObject ray in rayInteractorsArray)
        // {
        //     ray.SetActive(enable);
        // }

        // rayInteractorLeft.SetActive(enable);
        // rayInteractorRight.SetActive(enable);



        if (enable)
        {
            if (wandAudioSource != null)
            {
                wandAudioSource.Play();
            }
        }


    }

    // public void defineRayInteractors()
    // {
    //     rayInteractorsArray = GameObject.FindGameObjectsWithTag("RayInteractor");
    //     Debug.Log("defineRayInteractors: rayInteractors Length = " + rayInteractorsArray.Length);
    //     for (int i = 0; i < rayInteractorsArray.Length; i++)
    //     {
    //         Debug.Log("rayInteractors[" + i + "] = " + rayInteractors[i].name);
    //     }
    // }

    // public void bookContent
    // {
    //     case 
    // }


}
