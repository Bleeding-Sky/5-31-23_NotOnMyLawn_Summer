using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToTactical : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;

    public bool tacticalView;
    public bool isInteracting;

    public Rigidbody2D Player;

    public GameObject TrapMenu;
    // Start is called before the first frame update
    void Start()
    {
        tacticalView = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.E) && tacticalView == true && isInteracting == false)
        {
            SwitchToMap();
            TrapMenu.SetActive(true);
            isInteracting = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && isInteracting == true)
        {
            SwitchToHouse();
            TrapMenu.SetActive(false);
            isInteracting = false;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tacticalView = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tacticalView = false;
        }
    }
    private void canInteract()
    {
        if (Input.GetKey(KeyCode.E))
        {
            isInteracting = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            isInteracting = false;
        }
    }
    void SwitchToMap()
    {
        Camera1.SetActive(false);
        Camera2.SetActive(false);
        Camera3.SetActive(true);
        GameObject.Find("Circle").GetComponent<TEMP_HorizontalMovement>().enabled = false;
        GameObject.Find("Gun").GetComponent<GunScript>().enabled = false;
        Player.velocity = new Vector2(0, 0);
    }
    void SwitchToHouse()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
        Camera3.SetActive(false);
        GameObject.Find("Circle").GetComponent<TEMP_HorizontalMovement>().enabled = true;
        GameObject.Find("Gun").GetComponent<GunScript>().enabled = true;
    }
}
