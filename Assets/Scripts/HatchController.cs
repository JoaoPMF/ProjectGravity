using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchController : MonoBehaviour
{
    [SerializeField] private GameObject piece;
    [SerializeField] private GameObject real_piece;

    private bool locked = true;
    private Animator hatchAnimator;
    private Collider _collider;

    private void Awake()
    {
        hatchAnimator = gameObject.GetComponent<Animator>();
        _collider = gameObject.GetComponent<Collider>();
    }

    public void Unlock()
    {
        locked = false;
        hatchAnimator.Play("open_hatch", 0 , 0.0f);
        _collider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (locked)
        {
            if (collider.gameObject == piece)
            {
                Unlock();
                Destroy(piece);
                real_piece.SetActive(true);
            }
        }
    }
}
