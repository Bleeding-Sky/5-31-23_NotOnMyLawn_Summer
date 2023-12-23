using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDoor_Script : MonoBehaviour
{
    public Transform ExitPoint;
    public GameObject Player;
    public bool onDoor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Uses the doors position to teleport the player to the door
        if(onDoor == true && Input.GetKeyDown(KeyCode.W))
        {
            Player.transform.position = ExitPoint.transform.position;
        }
    }

    /// <summary>
    /// Once the door is in range the door will be able to be used
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject;
            onDoor = true;
        }
    }

    /// <summary>
    /// When the player is not on the door then it is not usable
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onDoor = false;
        }
    }
}
