using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceLockControllerScreen : Lock
{
    [SerializeField] private List<GameObject> keys;
    [SerializeField] private float[] sequence;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private bool isHint = false;
    [SerializeField] private int hintIndex = -1;

    private List<KeyController> keyControllers;
    private int sequenceIndex = 0;
    private bool completed = false;

    void Awake()
    {
        keyControllers = new List<KeyController>();
    }

    public override void Unlock()
    {
        sequenceIndex = 0;
        locked = false;
        foreach (KeyController keyController in keyControllers)
        {
            keyController.Confirm(2);
        }

        if (!completed)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            FindObjectOfType<ProgressManager>().Progress();
            completed = true;
        }
        //Reset();

        if (isHint)
        {
            FindObjectOfType<ProgressManager>().RemoveHighlightHintObject(hintIndex);
            FindObjectOfType<ProgressManager>().DisableHint(hintIndex);
        }
    }

    public override void _Lock()
    {
        locked = true;
    }

    public override void Check(float value, KeyController controller)
    {
        if (value > 0)
        {
            if (value == sequence[sequenceIndex++])
            {
                keyControllers.Add(controller);
                if (sequenceIndex == sequence.Length)
                {
                    Unlock();
                    return;
                }
                controller.Confirm(1);
                return;
            }
            else 
            {
                controller.Confirm(0);
                if (keyControllers.Contains(controller))
                    keyControllers.Remove(controller);
                Reset();
                _Lock();
                return;
            }  
        }
        else
        {
            controller.Confirm(-1);
            if (keyControllers.Contains(controller))
                keyControllers.Remove(controller);
            Reset();
            _Lock();
        }
    }

    public override void Check(float value)
    {
    
    }

    public override void Check(float value, PodController controller)
    {
        
    }

    private void Reset()
    {
        sequenceIndex = 0;
        foreach (KeyController keyController in keyControllers)
        {
            keyController.Reset();
        }
    }
}
