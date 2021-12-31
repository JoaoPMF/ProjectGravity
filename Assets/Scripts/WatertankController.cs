using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatertankController : Lock
{
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject water;
    [SerializeField] private bool isHint = false;
    [SerializeField] private int hintIndex = -1;
    [SerializeField] private Dialogue dialogue;

    public override void Unlock()
    {
        locked = false;

        gameObject.GetComponent<AudioSource>().Play();

        water.GetComponent<Renderer>().material.color = Color.cyan;

        if (isHint)
        {
            FindObjectOfType<ProgressManager>().RemoveHighlightHintObject(hintIndex);
            FindObjectOfType<ProgressManager>().DisableHint(hintIndex);
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

    private void OnTriggerEnter(Collider collider) 
    {
        if (locked)
        {
            if (collider.gameObject == key)
            {
                Unlock();
                Destroy(key);
            }
            else if (!collider.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
        }
    }
}
