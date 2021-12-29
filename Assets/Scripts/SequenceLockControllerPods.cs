using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceLockControllerPods : Lock
{
    [SerializeField] private List<GameObject> pods;
    [SerializeField] private float[] sequence;

    private List<PodController> podControllers;
    private int sequenceIndex = 0;

    void Awake()
    {
        podControllers = new List<PodController>();
    }

    public override void Unlock()
    {
        locked = false;
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_EmissionColor", Color.green);
        //Reset();
    }

    public override void _Lock()
    {
        locked = true;
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_EmissionColor", Color.red);
    }

    public override void Check(float value, PodController controller)
    {
        if (value > 0)
        {
            if (value == sequence[sequenceIndex++])
            {
                if (sequenceIndex == sequence.Length) 
                    Unlock();
                controller.Confirm(1);
                podControllers.Add(controller);
                return;
            }
            else 
            {
                controller.Confirm(0);
                if (podControllers.Contains(controller))
                    podControllers.Remove(controller);
                Reset();
                _Lock();
                return;
            }  
        }
        else
        {
            controller.Confirm(-1);
            if (podControllers.Contains(controller))
                podControllers.Remove(controller);
            Reset();
            _Lock();
        }
    }

    public override void Check(float value)
    {
    
    }

    private void Reset()
    {
        sequenceIndex = 0;
        foreach (PodController podController in podControllers)
        {
            podController.Reset();
        }
    }
}
