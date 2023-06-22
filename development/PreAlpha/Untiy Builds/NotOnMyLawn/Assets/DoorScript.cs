using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorScript : MonoBehaviour
{

    public TEMP_InteractableStates interactableStates;
    public GameObject openDoorSprite;
    public GameObject closedDoorSprite;
    public GameObject player;

    //used to track if a door is open
    public bool doorIsOpen;

    // Start is called before the first frame update
    void Start()
    {
        //fetch state component to read from 
        interactableStates = GetComponent<TEMP_InteractableStates>();

        //set doorIsOpen var
        doorIsOpen = interactableStates.isActivated;
    }

    // Update is called once per frame
    void Update()
    {
        //if activated, open door sprite turns on
        if (interactableStates.isActivated)
        {
            if (!doorIsOpen)
            {
                OpenDoor();
            }
            
        }
        //if not activated, door is closed
        else
        {
            closedDoorSprite.SetActive(true);
            openDoorSprite.SetActive(false);
            doorIsOpen= false;
        }
    }

    private void OpenDoor()
    {
        closedDoorSprite.SetActive(false);
        openDoorSprite.SetActive(true);

        //make door open away from player
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        doorIsOpen = true;
    }
}
