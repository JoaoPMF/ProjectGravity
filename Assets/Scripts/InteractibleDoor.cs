using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleDoor : MonoBehaviour, IInteractible
{
    private DoorController doorController;

    private void Awake()
    {
        doorController = gameObject.transform.parent.transform.gameObject.GetComponent<DoorController>();
    }

    public void Interact()
    {
        doorController.PlayAnimation();
    }
}
