using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDoor_Environment : MonoBehaviour
{

    public Transform groundPosition;

    public float length;
    public float height;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerDetection();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundPosition.position, new Vector2(length, height));
    }

    public Vector2 topLeftCalculation()
    {
        Vector2 centerPos = groundPosition.position;

        float heightOffset = height / 2;
        float lengthOffset = length / 2;

        Vector2 topLeftPos = centerPos + new Vector2(-lengthOffset, heightOffset);

        return topLeftPos;
    }

    public Vector2 bottomRightCalculation()
    {
        Vector2 centerPos = groundPosition.position;

        float heightOffset = height / 2;
        float lengthOffset = length / 2;

        Vector2 bottomRightPos = centerPos + new Vector2(lengthOffset, -heightOffset);

        return bottomRightPos;
    }
    public void PlayerDetection()
    {
        Collider2D[] PlayersInArea = Physics2D.OverlapAreaAll(topLeftCalculation(), bottomRightCalculation());

        foreach (Collider2D player in PlayersInArea)
        {
            if (player.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                BackgroundDoor_Environment door = GetComponent<BackgroundDoor_Environment>();

                //Depending on which door the player is at in the set it will teleport the player there
                if (door.currentDoor == door.door1)
                {
                    player.gameObject.transform.position = new Vector3(door.door2.transform.position.x - 2, 0, 0);
                    Debug.Log(door.door2.transform.position);
                    Debug.Log(door.door2.transform.position + new Vector3(2, 0, 0));
                }
                else if (door.currentDoor == door.door2)
                {
                    player.gameObject.transform.transform.position = new Vector3(door.door1.transform.position.x + 2, 0, 0);
                    Debug.Log(door.door1.transform.position);
                    Debug.Log(door.door1.transform.position + new Vector3(2, 0, 0));
                }
            }
        }
    }
}
