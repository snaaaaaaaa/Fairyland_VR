using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class animalfeeding : MonoBehaviour
{
    public string foodTag = "Food";
    public GameObject rewardPrefab;
    public Transform spawnPoint;
    public Animator animalAnimator;
    public AudioSource eatingSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(foodTag))
        {
            if (eatingSound != null)
                eatingSound.Play();

            if (animalAnimator != null)
                animalAnimator.SetBool("Eat", true);

            GameObject food = other.transform.root.gameObject;
            ForceRelease(food);
            Destroy(food);

            Debug.Log("Animal ate: " + other.name);
        }
    }

    private void ForceRelease(GameObject food)
    {
        var grab = food.GetComponentInChildren<XRGrabInteractable>();
        if (grab != null && grab.isSelected)
        {
            var interactors = new System.Collections.Generic.List<IXRSelectInteractor>(grab.interactorsSelecting);
            foreach (var interactor in interactors)
                grab.interactionManager.SelectExit(interactor, grab);
        }
    }

    public void SpawnReward()
    {
        if (rewardPrefab != null && spawnPoint != null)
        {
            Instantiate(rewardPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Animal spawned a new item!");
        }
        else
        {
            Debug.LogWarning("Missing Reward Prefab or Spawn Point on " + gameObject.name);
        }
    }
}
