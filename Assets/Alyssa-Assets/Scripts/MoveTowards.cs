using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 1.0f;

    // The target (cylinder) position.
    private Transform target;

    void Awake()
    {
        // Position the cube at the origin.
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        // Create and position the cylinder. Reduce the diameter.
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.localScale = new Vector3(0.15f, 1.0f, 0.15f);

        // Set target value to cylinder position.
        target = cylinder.transform;
        target.transform.position = new Vector3(0.8f, 0.0f, 0.8f);

        // Position the camera.
        Camera.main.transform.position = new Vector3(0.85f, 1.0f, -3.0f);
        Camera.main.transform.localEulerAngles = new Vector3(15.0f, -20.0f, -0.5f);

        // Create and position the floor.
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.transform.position = new Vector3(0.0f, -1.0f, 0.0f);
    }

    void Update()
    {
        // Move our position a step closer to the target.
        float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            // Reset the target position to the original object position.
            target.position *= -1.0f;
        }
    }
}