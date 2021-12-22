using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockController : MonoBehaviour
{
    [SerializeField] private GameObject _lock;

    private Animator doorAnimator;
    private Lock lockController;
    private bool doorOpen = false;

    private void Awake()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
        lockController = _lock.GetComponent<Lock>();
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (!lockController.locked)
        {
            if(collider.CompareTag("Player"))
            {
                PlayAnimation();
            }
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if (!lockController.locked)
        {
            if(collider.CompareTag("Player") && doorOpen)
            {
                PlayAnimation();
            }
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
