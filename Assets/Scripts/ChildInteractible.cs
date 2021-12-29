using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildInteractible : MonoBehaviour, IInteractible
{
    public void Interact()
    {
        IInteractible interactible = transform.parent.gameObject.GetComponent<IInteractible>();
        if (interactible != null) 
        {
            interactible.Interact();
        }
    }
}
