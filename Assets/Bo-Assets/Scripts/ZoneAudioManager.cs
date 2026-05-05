using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAudioManager : MonoBehaviour
{
    // Enum to define which zone this object represents
    // This lets us reuse the same script for multiple zones
    public enum ZoneType { Zone1, Zone2 }

    public ZoneType zoneType; // Set this in the Inspector for each zone

    [Header("Audio Sources")]
    public AudioSource music1; // Background music for Zone 1
    public AudioSource music2; // Background music for Zone 2
    public AudioSource whoosh; // Sound effect when entering/exiting zones

    // Static variable shared across all zone instances
    // Tracks which zone the player is currently in
    private static int currentZone = 0;

    // Called when something enters this trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Only respond if the Main Camera enters the zone
        // In XR, the camera usually represents the user's head position
        if (!other.CompareTag("MainCamera")) return;

        // Play transition sound when entering a zone
        whoosh.Play();

        // Determine which zone this object represents
        int newZone = (zoneType == ZoneType.Zone1) ? 1 : 2;

        // If already in this zone, do nothing
        // This prevents restarting the same music repeatedly
        if (currentZone == newZone) return;

        // Update current zone
        currentZone = newZone;

        // Stop both music tracks first to avoid overlap
        music1.Stop();
        music2.Stop();

        // Play the correct music based on the zone entered
        if (newZone == 1)
            music1.Play();
        else
            music2.Play();
    }

    // Called when something exits this trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Only respond to the Main Camera exiting
        if (!other.CompareTag("MainCamera")) return;

        // Play transition sound when leaving a zone
        whoosh.Play();
    }
}