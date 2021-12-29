using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour, IInteractible
{
    private bool pressed = false;
    private Animator keyAnimator;

    [SerializeField] private float value = 0f;
    [SerializeField] private Lock lockController;

    private void Awake()
    {
        keyAnimator = gameObject.GetComponent<Animator>();
    }

    public void Interact()
    {
        PlayAnimation();
        lockController.Check(GetValue(), this);
    }

    public float GetValue()
    {
        return !pressed ? 0 : value;
    }

    public void Reset()
    {
        if(pressed)
        {
            keyAnimator.Play("key_up", 0 , 0.0f);
            pressed = false;
        }
    }

    public void PlayAnimation()
    {
        if(!pressed)
        {
            pressed = true;
        }
        else
        {
            pressed = false;
        }
    }

    public void Confirm(int success)
    {
        if(success == 1)
        {
            keyAnimator.Play("key_down", 0 , 0.0f);
        }
        else if (success == 0)
        {
            keyAnimator.Play("key_error", 0 , 0.0f);
            pressed = false;
        }
        else if (success == -1)
        {
            keyAnimator.Play("key_up", 0 , 0.0f);
            pressed = false;
        }
        else if (success == 2)
        {
            keyAnimator.Play("key_success", 0 , 0.0f);
        }
    }
}