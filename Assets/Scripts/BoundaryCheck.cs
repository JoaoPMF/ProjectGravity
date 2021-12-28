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

    void OnCollisionEnter(Collision collision)
     {
        if (collision.gameObject.CompareTag("Ceiling"))
        {
            transform.position = lastPosition;
        }
     }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        if (transform.position.y < -5 && rb.detectCollisions == true)
        {
            transform.position = new Vector3(lastPosition.x,lastPosition.y+0.5f,lastPosition.z);
        }
    }
}
