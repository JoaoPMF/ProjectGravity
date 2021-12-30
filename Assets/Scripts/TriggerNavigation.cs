using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNavigation : MonoBehaviour
{
    [SerializeField] private LeverController LeverController;
    [SerializeField] private bool useLever = false;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private bool isHint = false;
    [SerializeField] private int hintIndex = -1;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            if (useLever)
            {
                LeverController.StopTimer();
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
