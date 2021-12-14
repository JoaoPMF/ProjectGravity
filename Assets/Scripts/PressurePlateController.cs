using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private DoorController doorController;
    private Animator pressurePlateAnimator;
    private bool pressurePlateDown = false;

    private void Awake() 
    {
        doorController = door.GetComponent<DoorController>();
        pressurePlateAnimator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collider) 
    {
        doorController.PlayAnimation();
        PlayAnimation();
    }

    private void OnTriggerExit(Collider collider) {
        doorController.PlayAnimation();
        PlayAnimation();
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
