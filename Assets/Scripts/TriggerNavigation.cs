using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNavigation : MonoBehaviour
{
    [SerializeField] private LeverController LeverController;
    [SerializeField] private Dialogue dialogue;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            LeverController.StopTimer();
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            Destroy(this);
        }
    }
}
