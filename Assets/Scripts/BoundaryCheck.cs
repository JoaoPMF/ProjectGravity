using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCheck : MonoBehaviour
{
    public Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        BoxCollider bc = gameObject.GetComponent<BoxCollider>();

        if (transform.position.y < -2 && rb.detectCollisions == true)
        {
            transform.position = lastPosition;
        }
    }
}
