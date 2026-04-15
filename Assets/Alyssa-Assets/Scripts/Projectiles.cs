using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=T5y7L1siFSY

public class Projectiles : MonoBehaviour
{
    private bool collided;

    public GameObject impactVFX;
    public GameObject impactExplodeVFX;

    //setting up what happens when spell hits something - prevent bouncing
    void OnCollisionEnter (Collision collision){
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided){
            collided = true;

            GameObject vfxInstance = null;

            // Normal impact VFX
            if (impactVFX != null && impactVFX.name == "vfx_Impact"){
                vfxInstance = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity) as GameObject;
            }
            // Exploding impact VFX that should affect nearby enemies
            else if (impactExplodeVFX != null && impactExplodeVFX.name == "vfx_ImpactExplode"){
                vfxInstance = Instantiate(impactExplodeVFX, collision.contacts[0].point, Quaternion.identity) as GameObject;
                // If the prefab doesn't already have ExplosionVFX attached, add it so the explosion creates a trigger collider
                if (vfxInstance.GetComponent<ExplosionVFX>() == null){
                    vfxInstance.AddComponent<ExplosionVFX>();
                }
            }

            if (vfxInstance != null){
                Destroy (vfxInstance, 2f);
            }

            Destroy (gameObject);
        }
    }

}
