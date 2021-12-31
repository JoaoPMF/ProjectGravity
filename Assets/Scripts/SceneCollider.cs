using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCollider : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private PlayerMovement playerMovement;
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player"))
        {
            fadeAnimator.SetBool("start",true);
            playerMovement.enabled = false;
            StartCoroutine(goToGame());
        }
    }

    IEnumerator goToGame()
    {   
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
}
