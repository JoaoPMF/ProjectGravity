using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    private int progress = 0;
    
    [SerializeField] private int hints = 3;
    [SerializeField] private int puzzleCount = 10;
    [SerializeField] private GameObject[] hintObjects;
    [SerializeField] private Text hintValue;
    [SerializeField] private GameObject progressValue;
    [SerializeField] private GameObject progressBackground;
    [SerializeField] private Animator hintAnimator;
    [SerializeField] private Animator progressAnimator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !PauseControl.gameIsPaused)
        {
            hintAnimator.SetBool("isVisible", true);
            StartCoroutine(hideUI(5,hintAnimator));
            StartCoroutine(changeHintValue());
            HighlightHintObject();
        }
    }

    void HighlightHintObject()
    {
        if (hints > 0 && hintObjects.Length >= progress)
        {
            Outline outline = hintObjects[progress].GetComponent<Outline>();
            outline.enabled = true;
            outline.OutlineMode = Outline.Mode.OutlineAll;
        }
    }

    public void Progress()
    {
        progressAnimator.SetBool("isVisible", true);
        StartCoroutine(hideUI(5,progressAnimator));
        StartCoroutine(IncreaseProgressBar());
    }

    IEnumerator IncreaseProgressBar()
    {
        RectTransform rtBg = progressBackground.GetComponent<RectTransform>();
        RectTransform rtVal = progressValue.GetComponent<RectTransform>();
        float currentWidth = ((rtBg.sizeDelta.x / puzzleCount ) * (progress));
        float finalWidth = ((rtBg.sizeDelta.x / puzzleCount ) * (++progress));
        for (float w = currentWidth; w <= finalWidth; w++)
        {
            rtVal.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
            yield return new WaitForSeconds(0.01f);
        }
    }
 
    IEnumerator hideUI(int secs, Animator animator)
    {
        yield return new WaitForSeconds(secs);
        animator.SetBool("isVisible", false);
    }

    IEnumerator changeHintValue()
    {
        yield return new WaitForSeconds(0.5f);
        hintAnimator.Play("hints_flash", 0 , 0.0f);
        yield return new WaitForSeconds(0.25f);
        hintValue.text = (--hints).ToString();
    }
}
