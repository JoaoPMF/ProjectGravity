using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float pickUpRange = 5f;
    public float moveForce = 250f;
    public float rotationSpeed = 100f;
    public Transform holdParent;
    
    GameObject heldObj;

    // Update is called once per frame
    void Update()
    {
        if (!PauseControl.gameIsPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (heldObj != null)
                {
                    DropObject();
                } 
                else
                {
                    RaycastHit hit;
                    if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
                    {
                        IInteractible interactible = hit.transform.gameObject.GetComponent<IInteractible>();
                        if (interactible != null) 
                        {
                            interactible.Interact();
                        }
                        else if(heldObj == null)
                        {
                            PickUpObject(hit.transform.gameObject);
                        }
                    } 
                }
            }

            if (Input.GetMouseButton(1) && heldObj != null)
            {
                RotateObject();
            }
            
            if (heldObj != null) 
            {
                MoveObject();
            } 
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position);
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            Rigidbody rb = pickUpObj.GetComponent<Rigidbody>();
            rb.detectCollisions = false;
            rb.freezeRotation = true;
            rb.useGravity = false;
            rb.drag = 10;

            //Outline outline = pickUpObj.GetComponent<Outline>();
            //outline.enabled = true;
            //outline.OutlineMode = Outline.Mode.SilhouetteOnly;

            pickUpObj.GetComponent<BoundaryCheck>().lastPosition = pickUpObj.transform.position;

            rb.transform.parent = holdParent;
            heldObj = pickUpObj;

            heldObj.transform.LookAt(transform.parent.transform);
        }
    }

    void DropObject()
    {
        Rigidbody rb = heldObj.GetComponent<Rigidbody>();
        rb.detectCollisions = true;
        rb.freezeRotation = false;
        rb.useGravity = true;
        rb.drag = 1;

        heldObj.GetComponent<Outline>().enabled = false;

        rb.transform.parent = null;
        heldObj = null;
    }

    void RotateObject()
    {
        heldObj.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}
