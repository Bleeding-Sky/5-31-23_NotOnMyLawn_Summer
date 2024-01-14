using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WindowInteraction_Enviornment : MonoBehaviour
{
    public bool Interacting;
    public bool lookingThroughWindow;

    public CameraManagement cameraManager;
    public CameraManagement.Cameras IndoorCamera;
    public CameraManagement.Cameras WindowView;
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
        cameraManager.currentEnum = IndoorCamera;
        Debug.Log("Indoor");
    }

    /// <summary>
    /// Switches the view to the outside of the house
    /// </summary>
    public void switchToOutside()
    {
        cameraManager.currentEnum = WindowView;
        Debug.Log("out");
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
