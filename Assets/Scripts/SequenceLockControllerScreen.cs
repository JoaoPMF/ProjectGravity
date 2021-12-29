using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceLockControllerScreen : Lock
{
    [SerializeField] private List<GameObject> keys;
    [SerializeField] private float[] sequence;

    private List<KeyController> keyControllers;
    private int sequenceIndex = 0;

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
        //Reset();
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
                    Unlock();
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
