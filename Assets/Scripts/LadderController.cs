using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    
    [SerializeField] private Transform orientation;
	[SerializeField] private float climbSpeed = 3.2f;
	
    private PlayerMovement playerController;
    private bool onLadder = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerMovement>();
        onLadder = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Ladder")
        {
            playerController.enabled = false;
            onLadder = !onLadder;
            rb.useGravity = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Ladder")
        {
            playerController.enabled = true;
            onLadder = !onLadder;
            rb.useGravity = true;
            if (playerController.isGrounded)
                rb.AddForce(orientation.forward * -10f, ForceMode.Impulse);
            else
                rb.AddForce(orientation.up * 5f + orientation.forward * 5f, ForceMode.Impulse);
        }
    }
            
    void Update()
    {
        if(onLadder == true)
        {
            transform.position += orientation.up * Input.GetAxisRaw("Vertical") / climbSpeed;
        }
    }
}