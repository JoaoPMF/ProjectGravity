using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLockController : Lock
{
    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private float combination = 10;

    private List<ButtonController> buttonControllers;

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

    public override void Check(float value)
    {
        float combinationValue = 0;

        foreach (ButtonController buttonController in buttonControllers)
        {
            combinationValue += buttonController.GetValue();
        }

        if (combinationValue == combination) 
            Unlock();
        else
            _Lock();
    }

    public override void Unlock()
    {
        locked = false;
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_EmissionColor", Color.green);
    }

    public override void _Lock()
    {
        locked = true;
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_EmissionColor", Color.red);
    }
}
