using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour, IInteractible
{
    private bool pressed = false;
    private Animator buttonAnimator;
    private Lock lockController;

    [SerializeField] private float value = 0f;
    [SerializeField] private GameObject _lock;

    private void Awake()
    {
        buttonAnimator = gameObject.GetComponent<Animator>();
        lockController = _lock.GetComponent<Lock>();
    }

    public void Interact()
    {
        PlayAnimation();
        lockController.Check(GetValue());
    }

    public float GetValue()
    {
        return !pressed ? 0 : value;
    }

    public void Reset()
    {
        if(pressed)
        {
            buttonAnimator.Play("button_up", 0 , 0.0f);
            pressed = false;
        }
    }

    public void PlayAnimation()
    {
        if(!pressed)
        {
            buttonAnimator.Play("button_down", 0 , 0.0f);
            pressed = true;
        }
        else
        {
            buttonAnimator.Play("button_up", 0 , 0.0f);
            pressed = false;
        }
    }
}
