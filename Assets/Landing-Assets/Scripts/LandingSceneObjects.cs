using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingSceneObjects : MonoBehaviour
{
    public GameObject wandObject;
    [SerializeField] private GameObject wand;
    public GameObject rayInteractorLeft;
    public GameObject rayInteractorRight;

    public GameObject abc;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Start: rayInteractors Length = " + rayInteractors.Length);
        // for (int i = 0; i < rayInteractors.Length; i++)
        // {
        //     Debug.Log("rayInteractors[" + i + "] = " + rayInteractors[i].name);
        // }

        // enableWand(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
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



        wand.SetActive(enable);
        rayInteractorLeft.SetActive(enable);
        rayInteractorRight.SetActive(enable);

        if (enable)
        {
            // one criteria for door met
        }


    }
}
