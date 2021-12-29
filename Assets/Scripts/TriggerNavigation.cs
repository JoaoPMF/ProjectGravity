using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNavigation : MonoBehaviour
{
    [SerializeField] private LeverController LeverController;
    [SerializeField] private bool useLever = false;
    [SerializeField] private Dialogue dialogue;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            if (useLever)
                LeverController.StopTimer();
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            Destroy(this);
        }
    }
}
