using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatertankLockController : Lock
{
    [SerializeField] private WatertankController[] WatertankControllers;

    public override void Unlock()
    {
        locked = false;
        var renderer = gameObject.GetComponent<Renderer>();
        Material green = renderer.materials[1];
        green.SetColor("_EmissionColor", Color.green);
        renderer.materials[1] = green;
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

    public void Update(){
        if (locked)
        {
            foreach (WatertankController watertank in WatertankControllers)
            {
                if (watertank.locked)
                    return;
            }
            Unlock();
        }
    }
}
