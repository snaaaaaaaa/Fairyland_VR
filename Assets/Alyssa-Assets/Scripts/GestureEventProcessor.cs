using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiVRy;



public class GestureEventProcessor : MonoBehaviour
{
    public MagicLine magicLine;

    // Start is called before the first frame update
    void Start()
    {
        if (magicLine == null)
        {
            magicLine = GetComponent<MagicLine>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Called when a gesture is finished (i.e. wand pressed and the player does something)
    public void OnGestureCompleted(GestureCompletionData gestureCompletionData)
    {

        if (gestureCompletionData.gestureID < 0)
        {
            string errorMessage = GestureRecognition.getErrorMessage(gestureCompletionData.gestureID);
            ///...
            return;
        }

        //checking similarity to existing patterns
        if (gestureCompletionData.similarity >= 0.5)
        {
            Debug.Log("Yippeeeee");
            Debug.Log(gestureCompletionData.gestureName);

            // Choosing spell
            switch (gestureCompletionData.gestureName)
            {
                case "Loop":
                    if (magicLine != null)
                    {
                        magicLine.ShootSingleProjectile();
                        Debug.Log("called projectile");
                    }
                    else
                    {
                        Debug.LogError("MagicLine reference is missing on GestureEventProcessor.");
                    }
                    break;

                case "Shake":
                    break;

                case "SwipeLeft":
                if (magicLine != null)
                    {
                        magicLine.ShootFanProjectile();
                        Debug.Log("called projectile");
                    }
                    else
                    {
                        Debug.LogError("MagicLine reference is missing on GestureEventProcessor.");
                    }
                    break;
                
                case "SwipeRight":
                if (magicLine != null)
                    {
                        magicLine.ShootFanProjectile();
                        Debug.Log("called projectile");
                    }
                    else
                    {
                        Debug.LogError("MagicLine reference is missing on GestureEventProcessor.");
                    }
                    break;
                   
            }
        }



        else
        {
            Debug.Log("Breh");
        }
    }
}
