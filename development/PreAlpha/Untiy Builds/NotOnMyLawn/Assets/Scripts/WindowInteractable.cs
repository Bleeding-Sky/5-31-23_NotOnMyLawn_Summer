using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowInteractable : MonoBehaviour
{
    public bool isInteracting;
    public bool windowView;

    public GameObject Camera1;
    public GameObject Camera2;

    public Rigidbody2D Player;
    public bool zombieThroughTheWindow;

    public GameObject outsideGun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        canInteract();
        if (windowView == true && isInteracting && !zombieThroughTheWindow)
        {
            switchToOutside();
        }
        else if (Input.GetKeyUp(KeyCode.E) || zombieThroughTheWindow)
        {
            switchToInside();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            windowView = true;
        }

        if(collision.CompareTag("Zombie"))
        {
            zombieThroughTheWindow = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            windowView = false;
        }

        if (collision.CompareTag("Zombie"))
        {
            zombieThroughTheWindow = false;
        }
    }

    private void canInteract()
    {
        if(Input.GetKey(KeyCode.E))
        {
            isInteracting = true;
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            isInteracting = false;
        }
    }

    private void switchToOutside()
    {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
        outsideGun.SetActive(true);

        GameObject.Find("Circle").GetComponent<BasicMovement>().enabled = false;
        GameObject.Find("Gun").GetComponent<GunScript>().enabled = false;
        Player.velocity = new Vector2(0, 0);

    }
    private void switchToInside()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
        outsideGun.SetActive(false);

        GameObject.Find("Circle").GetComponent<BasicMovement>().enabled = true;
        GameObject.Find("Gun").GetComponent<GunScript>().enabled = true;
    }

}
