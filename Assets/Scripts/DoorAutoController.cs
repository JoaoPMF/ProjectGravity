using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAutoController : MonoBehaviour
{
    private Animator doorAnimator;
    private bool doorOpen = false;

    private void Awake()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.CompareTag("Player"))
        {
            PlayAnimation();
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if(collider.CompareTag("Player"))
        {
            PlayAnimation();
        }
    }

    public void PlayAnimation()
    {
        if(!doorOpen)
        {
            doorAnimator.Play("door_open", 0 , 0.0f);
            doorOpen = true;
        }
        else
        {
            doorAnimator.Play("door_close", 0 , 0.0f);
            doorOpen = false;
        }
    }
}
