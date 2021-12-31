using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNavigation : MonoBehaviour
{
    [SerializeField] private LeverController leverController;
    [SerializeField] private DoorController doorController;
    [SerializeField] private bool useLever = false;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private bool isHint = false;
    [SerializeField] private int hintIndex = -1;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            if (useLever)
            {
                if (!doorController.doorOpen)
                    doorController.PlayAnimation();
                leverController.StopTimer();
                if (isHint)
                {
                    FindObjectOfType<ProgressManager>().RemoveHighlightHintObject(hintIndex);
                    FindObjectOfType<ProgressManager>().DisableHint(hintIndex);
                }
            }
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            Destroy(this);
        }
    }
}
