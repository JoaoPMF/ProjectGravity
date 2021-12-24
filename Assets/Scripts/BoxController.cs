using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour, IInteractible
{
    private bool open = false;
    private Animator boxAnimator;

    private void Awake()
    {
        boxAnimator = gameObject.GetComponent<Animator>();
    }

    public void Interact()
    {
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        if(!open)
        {
            boxAnimator.Play("box_open", 0 , 0.0f);
            open = true;
        }
        else
        {
            boxAnimator.Play("box_close", 0 , 0.0f);
            open = false;
        }
    }
}
