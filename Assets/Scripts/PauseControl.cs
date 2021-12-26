using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused;
    public Dialogue dialogue;
    
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject hints;
    [SerializeField] private GameObject progress;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Animator hintAnimator;
    [SerializeField] private Animator progressAnimator;
    [SerializeField] private Animator pauseMenuAnimator;
    [SerializeField] private Animator settingsMenuAnimator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            FindObjectOfType<ProgressManager>().Progress();
        }
    }

    public void PauseGame ()
    {
        gameIsPaused = !gameIsPaused;

        if(gameIsPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            crosshair.SetActive(false);
            pauseMenuAnimator.SetBool("isVisible", true);
            hintAnimator.SetBool("isVisible", true);
            progressAnimator.SetBool("isVisible", true);
            //AudioListener.pause = true;
        }
        else 
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            crosshair.SetActive(true);
            settingsMenuAnimator.SetBool("isVisible", false);
            pauseMenuAnimator.SetBool("isVisible", false);
            hintAnimator.SetBool("isVisible", false);
            progressAnimator.SetBool("isVisible", false);
            //AudioListener.pause = false;
            StartCoroutine(disablePauseMenu());
            StartCoroutine(disableSettingsMenu());
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSettingsMenu()
    {
        hintAnimator.SetBool("isVisible", false);
        progressAnimator.SetBool("isVisible", false);
        pauseMenuAnimator.SetBool("isVisible", false);
        StartCoroutine(disablePauseMenu());
        settingsMenu.SetActive(true);
        settingsMenuAnimator.SetBool("isVisible", true);
    }

    public void ExitSettingsMenu()
    {
        pauseMenu.SetActive(true);
        pauseMenuAnimator.SetBool("isVisible", true);
        hintAnimator.SetBool("isVisible", true);
        progressAnimator.SetBool("isVisible", true);
        settingsMenuAnimator.SetBool("isVisible", false);
        StartCoroutine(disableSettingsMenu());
    }

    IEnumerator disablePauseMenu()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        pauseMenu.SetActive(false);
    }

    IEnumerator disableSettingsMenu()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        settingsMenu.SetActive(false);
    }
}
