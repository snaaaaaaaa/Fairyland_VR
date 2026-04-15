using UnityEngine;

/// <summary>
/// Adds a trigger collider to the spawned explosion VFX and destroys "Enemy"-tagged objects that enter it.
/// Configure radius/duration as needed on the prefab or in inspector after instantiation.
/// Attach this script to your impactExplodeVFX prefab or let Projectiles add it at runtime.
/// </summary>
public class ExplosionVFX : MonoBehaviour
{
    public float radius = 3f;
    public float duration = 1.5f;

    void Start()
    {
        // add a trigger sphere collider to detect nearby enemies
        var col = GetComponent<SphereCollider>();
        if (col == null)
        {
            col = gameObject.AddComponent<SphereCollider>();
        }
        col.isTrigger = true;
        col.radius = radius;

        // auto-cleanup in case the VFX particle system doesn't destroy itself
        Destroy(gameObject, duration);
    }

    void OnTriggerEnter(Collider other)
    {
        // adjust tag as needed ("Enemy" assumed)
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}