using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatertankLockController : Lock
{
    [SerializeField] private WatertankController[] WatertankControllers;
    [SerializeField] private Dialogue dialogue;

    private bool completed = false;

    public override void Unlock()
    {
        locked = false;
        var renderer = gameObject.GetComponent<Renderer>();
        Material green = renderer.materials[1];
        green.SetColor("_EmissionColor", Color.green);
        renderer.materials[1] = green;

        gameObject.GetComponent<AudioSource>().Play();

        if (!completed)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            FindObjectOfType<ProgressManager>().Progress();
            completed = true;
        }
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
