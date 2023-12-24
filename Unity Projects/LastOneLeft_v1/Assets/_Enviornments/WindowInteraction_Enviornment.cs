using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowInteraction_Enviornment : MonoBehaviour
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

    /// <summary>
    /// Switches the view to the inside of the house
    /// </summary>
    public void switchToInside()
    {
        OutsideCam.SetActive(false);
        InsideCam.SetActive(true);
    }

    /// <summary>
    /// Switches the view to the outside of the house
    /// </summary>
    public void switchToOutside()
    {
        OutsideCam.SetActive(true);
        InsideCam.SetActive(false);
    }

    /// <summary>
    /// Ignore the collision of anythng other than the interaction field
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("InteractableField"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
