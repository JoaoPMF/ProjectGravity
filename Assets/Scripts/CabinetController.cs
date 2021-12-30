using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetController : MonoBehaviour, IInteractible
{
    private bool open = false;
    private Animator cabinetAnimator;
    [SerializeField] private AudioClip cabinetOpen;
    [SerializeField] private AudioClip cabinetClose;

    private void Awake()
    {
        cabinetAnimator = gameObject.GetComponent<Animator>();
    }

    public void Interact()
    {
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        if(!open)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(cabinetOpen);
            cabinetAnimator.Play("cabinet_open", 0 , 0.0f);
            open = true;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(cabinetClose);
            cabinetAnimator.Play("cabinet_close", 0 , 0.0f);
            open = false;
        }
    }
}
