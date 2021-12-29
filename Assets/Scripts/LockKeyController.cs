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
        Material green = renderer.materials[2];
        green.SetColor("_EmissionColor", Color.green);
        renderer.materials[2] = green;
    }

    public override void _Lock() 
    {
        //empty
    }

    public override void Check(float value) 
    {
        //empty
    }

    public override void Check(float value, PodController controller){

    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (locked)
        {
            if (collider.gameObject == key)
            {
                Unlock();
                Destroy(key);
            }
        }
    }
}
