using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatertankController : Lock
{
    [SerializeField] private GameObject key;

    public override void Unlock()
    {
        locked = false;
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

    public override void Check(float value, KeyController controller)
    {
        
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
