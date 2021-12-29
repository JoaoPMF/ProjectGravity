using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour, IInteractible
{
    [SerializeField] private DoorController doorController;
    [SerializeField] private float timerDuration = 10f;

    private Animator leverAnimator;
    private float timer = 0f;

    private void Awake()
    {
        leverAnimator = gameObject.GetComponent<Animator>();
    }

    public void Interact()
    {
        if (timer <= 0f){
            leverAnimator.Play("lever_down", 0 , 0.0f);
            doorController.PlayAnimation();
            timer = timerDuration;
        }
    }

    public void StopTimer()
    {
        timer=0;
        GetComponent<Collider>().enabled = false;
    }

    private void Update() {
        if(timer>0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f){
                doorController.PlayAnimation();
                leverAnimator.Play("lever_up", 0 , 0.0f);
            }
        }
    }
}
