using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    
    void Start()
    {
        // define current enemy position and target position (center of map)
        //Vector3 currentPos = transform.position;
        //Vector3 targetPos = new Vector3(0, 0, 0);
        // Debug.Log (transform.position);
        // Debug.Log (transform.position.x);
        // Debug.Log (transform.position.y);
        // Debug.Log (transform.position.x);
    }

    void Update()
    {
        // Option 0: This direct approach will not work
        // transform.positionn.x += 0.1f
        
        // Option 1
        //currentPos.x += 0.1f;
        //transform.position = currentPos;

        // Option 2
        // transform.position += Vector3.forward * Time.deltaTime;

        // Option 3
        // Move the object 1 unit/second.
        // transform.Translate(Vector3.forward * Time.deltaTime);

        // READ MORE
        // There are also Vector3.back, Vector3.down, Vector3.up
        // Please refer to https://docs.unity3d.com/ScriptReference/Vector3.html
    }
}
