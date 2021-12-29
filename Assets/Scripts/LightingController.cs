using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour
{
    [SerializeField] private GameObject[] lights;
    [SerializeField] private GameObject[] fixtures;
    [SerializeField] private GameObject[] side_fixtures;
    [SerializeField] private Lock lockController;
    [SerializeField] private Dialogue dialogue;

    private bool lightsOn = false;
    private bool completed = false;

    public void ToggleLights()
    {
        if(!lightsOn && !lockController.locked)
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(true);
            }

            foreach (GameObject fixture in fixtures)
            {
                Renderer renderer = fixture.GetComponent<Renderer>();
                Material m = renderer.materials[1];
                m.EnableKeyword("_EMISSION");
                renderer.materials[1] = m;
            }

            foreach (GameObject fixture in side_fixtures)
            {
                Renderer renderer = fixture.GetComponent<Renderer>();
                Material m = renderer.materials[0];
                m.EnableKeyword("_EMISSION");
                renderer.materials[0] = m;
            }

            lightsOn = true;
            if (!completed)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                FindObjectOfType<ProgressManager>().Progress();
                completed = true;
            }
        }
        else
        {
            Debug.Log("wrong block");
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
            }

            foreach (GameObject fixture in fixtures)
            {
                Renderer renderer = fixture.GetComponent<Renderer>();
                Material m = renderer.materials[1];
                m.DisableKeyword("_EMISSION");
                renderer.materials[1] = m;
            }

            foreach (GameObject fixture in side_fixtures)
            {
                Renderer renderer = fixture.GetComponent<Renderer>();
                Material m = renderer.materials[0];
                m.DisableKeyword("_EMISSION");
                renderer.materials[0] = m;
            }
            
            lightsOn = false;
        }
    }
}
