using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeverController : MonoBehaviour, IInteractible
{
    [SerializeField] private DoorController doorController;
    [SerializeField] private float timerDuration = 10f;
    [SerializeField] private AudioClip leverError;
    [SerializeField] private Lock waterlock_lever;
    [SerializeField] private GameObject countdown;

    private Animator leverAnimator;
    private TextMeshPro countdownText;

    private float timer = 0f;

    private void Awake()
    {
        leverAnimator = gameObject.GetComponent<Animator>();
        countdownText = countdown.GetComponent<TextMeshPro>();
    }

    public void Interact()
    {
        if (waterlock_lever.locked){
            leverAnimator.Play("lever_locked", 0 , 0.0f);
            gameObject.GetComponent<AudioSource>().PlayOneShot(leverError);
        }
        else{
            if (timer <= 0f){
                gameObject.GetComponent<AudioSource>().Play();
                leverAnimator.Play("lever_down", 0 , 0.0f);
                doorController.PlayAnimation();
                timer = timerDuration;
                TimeSpan time = TimeSpan.FromSeconds(timer);
                countdownText.SetText(time.ToString("ss':'ff"));
                countdown.SetActive(true);
            }
        }
    }

    public void StopTimer()
    {
        timer=0;
        GetComponent<Collider>().enabled = false;
    }

    private void Update() {
        if(timer>0)
        {
            timer -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(timer);
            countdownText.SetText(time.ToString("ss':'ff"));
            if (timer <= 0f){
                doorController.PlayAnimation();
                leverAnimator.Play("lever_up", 0 , 0.0f);
                gameObject.GetComponent<AudioSource>().Play();
                countdown.SetActive(false);
            }
        }
    }
}
