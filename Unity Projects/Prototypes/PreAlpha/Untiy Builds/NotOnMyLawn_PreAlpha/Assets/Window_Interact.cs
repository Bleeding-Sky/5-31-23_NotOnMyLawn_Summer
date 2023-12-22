using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Interact : MonoBehaviour
{
    public bool Interacting;
    public bool lookingThroughWindow;

    public GameObject OutsideCam;
    public GameObject InsideCam;

    // Start is called before the first frame update
    void Start()
    {
        lookingThroughWindow = false;
        Interacting = false;
    }

    public void switchToInside()
    {
        OutsideCam.SetActive(false);
        InsideCam.SetActive(true);
    }

    public void switchToOutside()
    {
        OutsideCam.SetActive(true);
        InsideCam.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("InteractableField"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
