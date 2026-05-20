using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderTrigger : MonoBehaviour
{
    public string NextSceneName;

    [Header("Portal Audio")]
    [SerializeField] private AudioSource portalAudioSource;
    [SerializeField] private AudioClip portalSound1;

    private bool isTeleporting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isTeleporting) return;

        Debug.Log("trigger");
        Debug.Log(other.tag);

        if (other.CompareTag("MainCamera") || other.name.Contains("Camera"))
        {
            Debug.Log("Player entered trigger zone");

            StartCoroutine(PlaySoundThenLoadScene());
        }
    }

    private IEnumerator PlaySoundThenLoadScene()
    {
        isTeleporting = true;

        if (portalAudioSource != null && portalSound1 != null)
        {
            portalAudioSource.PlayOneShot(portalSound1);

            // Wait briefly before switching scenes
            yield return new WaitForSeconds(1.25f);
        }
        else
        {
            Debug.LogWarning("Portal audio source or PortalSound1 has not been assigned.");
        }

        Debug.Log("about to load?");

        SceneManager.LoadScene(NextSceneName);
    }
}