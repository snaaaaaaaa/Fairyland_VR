using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class animalfeeding : MonoBehaviour
{
    // Drag the food tag here (create a "Food" tag)
    public string foodTag = "Food";
    public GameObject rewardPrefab; // Drag the new item/food prefab here
    public Transform spawnPoint;   // Drag the animal's mouth or hand here

    // Optional: Add a reference to your animal's animator
    public Animator animalAnimator;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the item entering the mouth has the "Food" tag
        if (other.CompareTag(foodTag))
        {
            // 1. Play eating animation
            if (animalAnimator != null)
            {
                animalAnimator.SetTrigger("Eat");
            }

            // 2. Destroy the food item
            Destroy(other.gameObject);

            // 3. Optional: Play eating sound
          //  Debug.Log("Animal ate: " + other.name);
        }
    }
    public void SpawnReward()
    {
        if (rewardPrefab != null && spawnPoint != null)
        {
            // Creates the new item
            Instantiate(rewardPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Animal spawned a new item!");
        }
        else
        {
            Debug.LogWarning("Missing Reward Prefab or Spawn Point on " + gameObject.name);
        }
    }
}

