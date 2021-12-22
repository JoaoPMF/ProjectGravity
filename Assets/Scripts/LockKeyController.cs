using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockKeyController : Lock
{
    [SerializeField] private GameObject key;

    public override void Unlock()
    {
        locked = false;
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_EmissionColor", Color.green);
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (locked)
        {
            if (collider.gameObject == key)
            {
                Unlock();
            }
        }
    }
}
