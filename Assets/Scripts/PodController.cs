using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodController : MonoBehaviour, IInteractible
{
    private bool closed = false;
    private Animator podAnimator;
    private Lock lockController;

    [SerializeField] private float value = 0f;
    [SerializeField] private GameObject _lock;
    [SerializeField] private GameObject _light;
    [SerializeField] private AudioClip podOpen;
    [SerializeField] private AudioClip podError;

    private void Awake()
    {
        podAnimator = gameObject.GetComponent<Animator>();
        lockController = _lock.GetComponent<Lock>();
    }

    public void Interact()
    {
        PlayAnimation(lockController.locked);
        if(lockController.locked)
            lockController.Check(GetValue(), this);
    }

    public float GetValue()
    {
        return !closed ? 0 : value;
    }

    public void Reset()
    {
        if(closed)
        {
            StartCoroutine(toogleLight(false));
            podAnimator.Play("capsule_open", 0 , 0.0f);
            closed = false;
        }
    }

    public void PlayAnimation(bool locked)
    {
        if(!closed)
        {
            closed = true;
            if(!lockController.locked)
            {
                gameObject.GetComponent<AudioSource>().Play();
                podAnimator.Play("capsule_close", 0 , 0.0f);
                StartCoroutine(toogleLight(true));
            }
        }
        else
        {
            closed = false;
            if(!lockController.locked)
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(podOpen);
                podAnimator.Play("capsule_open", 0 , 0.0f);
                closed = false;
                StartCoroutine(toogleLight(false));
            }
        }
    }

    public void Confirm(int success)
    {
        if(success == 1)
        {
            gameObject.GetComponent<AudioSource>().Play();
            podAnimator.Play("capsule_close", 0 , 0.0f);
            StartCoroutine(toogleLight(true));
        }
        else if (success == 0)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(podError);
            podAnimator.Play("capsule_error", 0 , 0.0f);
            closed = false;
        }
        else if (success == -1)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(podOpen);
            podAnimator.Play("capsule_open", 0 , 0.0f);
            closed = false;
            StartCoroutine(toogleLight(false));
        }
    }

    IEnumerator toogleLight(bool toogle)
    {
        yield return new WaitForSecondsRealtime(1f);
        _light.SetActive(toogle);
    }
}

