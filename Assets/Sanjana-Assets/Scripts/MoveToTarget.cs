using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveToTarget : MonoBehaviour
{
    public Transform xrRig;
    public Transform target;
    public float speed = 5f;

    public InputActionReference moveAction;

    void OnEnable()
    {
        if (moveAction != null)
            moveAction.action.Enable();
    }

    void OnDisable()
    {
        if (moveAction != null)
            moveAction.action.Disable();
    }

    void Update()
    {
        if (xrRig == null || target == null || moveAction == null) return;

        if (moveAction.action.IsPressed())
        {
            Vector3 targetPosition = new Vector3(
              target.position.x,
              xrRig.position.y,
              target.position.z
            );

            xrRig.position = Vector3.MoveTowards(
              xrRig.position,
              targetPosition,
              speed * Time.deltaTime
            );
        }
    }
}