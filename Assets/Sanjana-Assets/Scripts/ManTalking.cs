using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomNPCSounds : MonoBehaviour
{
    public AudioClip[] soundClips;

    public float minDelay = 3f;
    public float maxDelay = 8f;

    private AudioSource audioSource;
    private int lastClipIndex = -1;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.spatialBlend = 1f;
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void Start()
    {
        StartCoroutine(PlayRandomSounds());
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            if (soundClips.Length == 0) continue;

            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, soundClips.Length);
            }
            while (randomIndex == lastClipIndex && soundClips.Length > 1);

            lastClipIndex = randomIndex;

            audioSource.PlayOneShot(soundClips[randomIndex]);
        }
    }
}