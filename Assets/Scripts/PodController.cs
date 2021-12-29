using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodController : MonoBehaviour, IInteractible
{
    private bool closed = false;
    private Animator podAnimator;
    private Lock lockController;

    [SerializeField] private float value = 0f;
    [SerializeField] private GameObject _lock;

    private void Awake()
    {
        podAnimator = gameObject.GetComponent<Animator>();
        lockController = _lock.GetComponent<Lock>();
    }

    public void Interact()
    {
        PlayAnimation();
        lockController.Check(GetValue(), this);
    }

    public float GetValue()
    {
        return !closed ? 0 : value;
    }

    public void Reset()
    {
        if(closed)
        {
            podAnimator.Play("capsule_open", 0 , 0.0f);
            closed = false;
        }
    }

    public void PlayAnimation()
    {
        if(!closed)
        {
            closed = true;
        }
        else
        {
            closed = false;
        }
    }

    public void Confirm(int success)
    {
        if(success == 1)
        {
            podAnimator.Play("capsule_close", 0 , 0.0f);
        }
        else if (success == 0)
        {
            podAnimator.Play("capsule_error", 0 , 0.0f);
            closed = false;
        }
        else if (success == -1)
        {
            podAnimator.Play("capsule_open", 0 , 0.0f);
            closed = false;
        }
    }
}

