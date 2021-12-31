using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAutoController : MonoBehaviour
{

    public bool isTutorialDoor = false;

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
            if (isTutorialDoor)
                PlayAnimationTutorial();
            else
                PlayAnimation();
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if(collider.CompareTag("Player"))
        {
            if (isTutorialDoor)
                PlayAnimationTutorial();
            else
                PlayAnimation();
        }
    }

    public void PlayAnimationTutorial()
    {
        gameObject.GetComponent<AudioSource>().Play();
        if(!doorOpen)
        {
            doorAnimator.Play("door_open_tut", 0 , 0.0f);
            doorOpen = true;
        }
        else
        {
            doorAnimator.Play("door_close_tut", 0 , 0.0f);
            doorOpen = false;
        }
    }

    public void PlayAnimation()
    {
        gameObject.GetComponent<AudioSource>().Play();
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
