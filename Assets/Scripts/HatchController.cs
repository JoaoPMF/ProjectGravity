using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchController : MonoBehaviour
{
    [SerializeField] private GameObject piece;
    [SerializeField] private GameObject hatch;
    [SerializeField] private GameObject real_piece;

    private bool locked = true;
    private Animator hatchAnimator;
    private Collider _collider;

    private void Awake()
    {
        hatchAnimator = hatch.GetComponent<Animator>();
        _collider = gameObject.GetComponent<Collider>();
    }

    public void Unlock()
    {
        locked = false;
        hatchAnimator.Play("open_hatch", 0 , 0.0f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (locked)
        {
            if (collider.gameObject == piece)
            {
                Destroy(piece);
                real_piece.SetActive(true);
                Unlock();
            }
        }
    }
}
