using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    private int progress = 0;
    private Dictionary<GameObject,bool> hintList;
    
    [SerializeField] private int hints = 3;
    [SerializeField] private int puzzleCount = 10;
    [SerializeField] private List<GameObject> hintObjects;
    [SerializeField] private Text hintValue;
    [SerializeField] private GameObject progressValue;
    [SerializeField] private GameObject progressBackground;
    [SerializeField] private Animator hintAnimator;
    [SerializeField] private Animator progressAnimator;
    [SerializeField] private Animator spaceshipAnimator;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private PlayerMovement playerMovement;

    private void Awake() {
        hintList = new Dictionary<GameObject,bool>();

        foreach(GameObject hintObject in hintObjects)
        {
            hintList.Add(hintObject,true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !PauseControl.gameIsPaused)
        {
            hintAnimator.SetBool("isVisible", true);
            StartCoroutine(hideUI(5,hintAnimator));
            if (hints > 0 && hintList.ContainsValue(true))
            {
                StartCoroutine(changeHintValue());
                HighlightHintObject();
            }
        }
    }

    GameObject GetNextHint()
    {
        foreach(GameObject gameObject in hintList.Keys)
        {
            if (hintList[gameObject])
            {
                return gameObject;
            }
        }
        return null;
    }

    public void RemoveHighlightHintObject(int index)
    {
        GameObject hintObject = hintList.Keys.ElementAt(index);
        Outline outline = hintObject.GetComponent<Outline>();
        outline.enabled = false;
    }

    public void DisableHint(int index)
    {
        hintList[hintList.Keys.ElementAt(index)] = false;
    }

    void HighlightHintObject()
    {
        GameObject nextHint = GetNextHint();
        if (nextHint != null)
        {
            hintList[nextHint] = false;
            Outline outline = nextHint.GetComponent<Outline>();
            outline.enabled = true;
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
        if(progress==puzzleCount)
        {
            spaceshipAnimator.SetBool("finish",true);
            StartCoroutine(goToMainMenu());
            StartCoroutine(fadeOut());
            playerMovement.enabled = false;
        }
    }

    IEnumerator fadeOut()
    {   
        yield return new WaitForSeconds(10f);
        fadeAnimator.SetBool("finish",true);
    }

    IEnumerator goToMainMenu()
    {   
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene(1);
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
