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
        if(onDoor == true && Input.GetKeyDown(KeyCode.W))
        {
            Player.transform.position = ExitPoint.transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject;
            onDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onDoor = false;
        }
    }
}
