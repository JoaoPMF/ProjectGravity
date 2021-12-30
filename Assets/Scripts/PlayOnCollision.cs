using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnCollision : MonoBehaviour
{
    static bool settled = true;

    private void OnCollisionEnter(Collision other) {
        if(settled && !other.gameObject.CompareTag("Player")/*other.gameObject.GetComponent<Rigidbody>() == null*/)
        {
            gameObject.GetComponent<AudioSource>().Play();
            settled = false;
            StartCoroutine(toggleSettled());
        }
    }

    IEnumerator toggleSettled()
    {
        yield return new WaitForSecondsRealtime(1f);
        settled = true;
    }
}
