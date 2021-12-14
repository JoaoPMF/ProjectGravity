using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPressurePlateController : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private float timerDuration = 1f;

    private DoorController doorController;
    private Animator pressurePlateAnimator;
    private bool pressurePlateDown = false;
    private float timer = 0f;

    private void Awake() 
    {
        doorController = door.GetComponent<DoorController>();
        pressurePlateAnimator = gameObject.GetComponent<Animator>();
    }

    private void Update() {
        if(timer>0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f){
                doorController.PlayAnimation();
                PlayAnimation();
            }
        }
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (timer <= 0f){
            doorController.PlayAnimation();
            PlayAnimation();
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        timer = timerDuration;
    }

    public void PlayAnimation()
    {
        if(!pressurePlateDown)
        {
            pressurePlateAnimator.Play("pressure_plate_down", 0 , 0.0f);
            pressurePlateDown = true;
        }
        else
        {
            pressurePlateAnimator.Play("pressure_plate_up", 0 , 0.0f);
            pressurePlateDown = false;
        }
    }
}
