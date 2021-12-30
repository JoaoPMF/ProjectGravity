using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLockController : Lock
{
    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private float combination = 10;
    [SerializeField] private bool lightTrigger = false;
    [SerializeField] private bool isHint = false;
    [SerializeField] private int hintIndex = -1;
    [SerializeField] private LightingController LightingController;

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
        
        if (lightTrigger)
            LightingController.ToggleLights();

        if (isHint)
        {
            FindObjectOfType<ProgressManager>().RemoveHighlightHintObject(hintIndex);
            FindObjectOfType<ProgressManager>().DisableHint(hintIndex);
        }

         gameObject.GetComponent<AudioSource>().Play();
    }

    public override void _Lock()
    {
        locked = true;
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_EmissionColor", Color.red);
        if (lightTrigger)
            LightingController.ToggleLights();
    }

    public override void Check(float value, PodController controller){

    }

    public override void Check(float value, KeyController controller)
    {
        
    }
}
