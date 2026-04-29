using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioSource soundEffect; // Assign your audio source in the Inspector

    public string collisionTag = "";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision npc");
        // Only trigger if the Main Camera enters this object's trigger collider
        if (!other.CompareTag(tag)) return;

        // Play the sound effect
        Debug.Log("playbonk");
        soundEffect.Play();

        // Print a message to the Console so we can check that it worked
        Debug.Log("PlayBonk");
    }
}