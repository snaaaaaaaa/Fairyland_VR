using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAudioManager : MonoBehaviour
{
    public enum ZoneType { Zone1, Zone2 }

    public ZoneType zoneType;

    [Header("Audio Sources")]
    public AudioSource myZoneFootsteps;    // Drag THIS zone's footstep AudioSource here
    public AudioSource otherZoneFootsteps; // Drag the OTHER zone's footstep AudioSource here

    [Header("Movement Detection")]
    public Transform playerHead;
    public float moveThreshold = 0.002f;

    [Header("Spawn Settings")]
    public bool playerStartsHere = false; // Tick this on Zone 1 only

    private static int currentZone = 0;
    private Vector3 lastHeadPosition;

    private void Start()
    {
        if (playerHead != null)
            lastHeadPosition = playerHead.position;

        myZoneFootsteps.Stop();
        otherZoneFootsteps.Stop();

        if (playerStartsHere)
            currentZone = (zoneType == ZoneType.Zone1) ? 1 : 2;
    }

    private void Update()
    {
        bool thisZoneIsActive = (zoneType == ZoneType.Zone1 && currentZone == 1)
                             || (zoneType == ZoneType.Zone2 && currentZone == 2);

        if (!thisZoneIsActive || playerHead == null) return;

        float distanceMoved = Vector3.Distance(playerHead.position, lastHeadPosition);
        lastHeadPosition = playerHead.position;

        if (distanceMoved > moveThreshold)
        {
            if (!myZoneFootsteps.isPlaying)
                myZoneFootsteps.Play();
        }
        else
        {
            if (myZoneFootsteps.isPlaying)
                myZoneFootsteps.Pause();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;


        int newZone = (zoneType == ZoneType.Zone1) ? 1 : 2;
        if (currentZone == newZone) return;

        currentZone = newZone;

        // Stop both footstep tracks cleanly on zone switch
        myZoneFootsteps.Stop();
        otherZoneFootsteps.Stop();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

       
    }
}
