using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceLockController : Lock
{
    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private float[] sequence = {};

    private List<ButtonController> buttonControllers;
    private int sequenceIndex = 0;

    void Awake()
    {
        buttonControllers = new List<ButtonController>();

        foreach (GameObject button in buttons)
        {
            ButtonController buttonController = button.GetComponent<ButtonController>();
            if (buttonController != null) 
                buttonControllers.Add(buttonController);
        }
    }

    public override void Unlock()
    {
        locked = false;
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_EmissionColor", Color.green);
        Reset();
    }

    public override void _Lock()
    {
        locked = true;
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_EmissionColor", Color.red);
    }

    public override void Check(float value)
    {
        if (value > 0)
        {
            if (value == sequence[sequenceIndex])
            {
                if (++sequenceIndex == sequence.Length) 
                    Unlock();
                return;
            }
            else 
            {
                Reset();
                _Lock();
                return;
            }  
        }
    }

    private void Reset()
    {
        sequenceIndex = 0;
        foreach (ButtonController buttonController in buttonControllers)
        {
            buttonController.Reset();
        }
    }

    public override void Check(float value, PodController controller){

    }
}
