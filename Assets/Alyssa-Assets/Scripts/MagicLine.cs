using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=T5y7L1siFSY

public class MagicLine : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera cam;
    public GameObject projectile;
    public Transform firePoint, LHFirePoint, RHFirepoint;
    public float projectileSpeed = 30;
    public float circleRadius = 1f;
    public float circleExpansionSpeed = 5f;

    public Vector3 destination;
    // private bool leftHand;

    // Fan settings
    public int fanCount = 7;            // number of projectiles in the fan
    public float fanAngleSpread = 30f; // total angle spread in degrees

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShootSingleProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }

        else
        {
            destination = ray.GetPoint(1000);
        }

        InstantiateProjectile(firePoint);
    }

    // public void ShootFanProjectile(){}
    public void ShootFanProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        InstantiateFanProjectile(firePoint);
    }

    public void ShootCircleProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        // Instantiate a circle of projectiles around the fire point and shoot them forward
        int circleCount = 12; // number of projectiles in the circle

        Vector3 forward = firePoint.forward;
        if (forward == Vector3.zero)
        {
            forward = cam.transform.forward;
        }

        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
        Vector3 up = Vector3.Cross(forward, right).normalized;

        for (int i = 0; i < circleCount; i++)
        {
            float angle = i * Mathf.PI * 2 / circleCount;
            Vector3 offset = right * Mathf.Cos(angle) * circleRadius + up * Mathf.Sin(angle) * circleRadius;
            Vector3 radialDir = (offset).normalized;
            Vector3 velocity = forward * projectileSpeed + radialDir * circleExpansionSpeed;

            var projObj = Instantiate(projectile, firePoint.position + offset, Quaternion.LookRotation(velocity)) as GameObject;
            projObj.GetComponent<Rigidbody>().velocity = velocity;
        }
    }



    // void InstantiateProjectile(Transform firePoint)
    void InstantiateProjectile(Transform firePoint)

    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        Debug.Log(firePoint.position);
        Debug.Log(Quaternion.identity);


        Vector3 direction = (destination - firePoint.position).normalized;
        Debug.Log(direction);
        if (Vector3.Dot(direction, firePoint.forward) < 0f)
        {
            direction = -direction;
        }
        projectileObj.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
    }

    void InstantiateFanProjectile(Transform firePoint)
    {
        if (fanCount <= 0) return;

        // base direction from fire point to destination
        Vector3 baseDir = (destination - firePoint.position).normalized;
        if (Vector3.Dot(baseDir, firePoint.forward) < 0f)
        {
            baseDir = -baseDir;
        }

        // If only one projectile, behave like single shot
        if (fanCount == 1)
        {
            var p = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
            p.GetComponent<Rigidbody>().velocity = baseDir * projectileSpeed;
            return;
        }

        // Spread the angles evenly across fanAngleSpread
        // Example: fanCount=5, spread=30 -> angles: -15, -7.5, 0, 7.5, 15
        float step = fanAngleSpread / (fanCount - 1);
        float start = -fanAngleSpread / 2f;

        for (int i = 0; i < fanCount; i++)
        {
            float angle = start + step * i;
            // rotate around the firePoint's up vector so the fan is horizontal relative to the firePoint
            Vector3 dir = Quaternion.AngleAxis(angle, firePoint.up) * baseDir;

            var projObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
            projObj.GetComponent<Rigidbody>().velocity = dir * projectileSpeed;
        }
    }

}
