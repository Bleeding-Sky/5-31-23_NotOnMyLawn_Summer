using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Environment : MonoBehaviour
{
    public Transform RoomPoint;
    public Transform RoomPointA;
    public Transform RoomPointB;
    public Vector2 RoomSize;
    public List<GameObject> Doors;
    public bool countDoors;
    // Start is called before the first frame update
    void Start()
    {
        countDoors = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerTracking();
    }
    /// <summary>
    /// Tracks the player through the map and sets their location depending on if the plaer is in the room
    /// </summary>
    public void PlayerTracking()
    {
        Collider2D[] PlayersInRoom = Physics2D.OverlapAreaAll(RoomPointA.position, RoomPointB.position);

        foreach (Collider2D player in PlayersInRoom)
        {
            if (player.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                RoomTracking_Environment tracking = gameObject.transform.parent.GetComponent<RoomTracking_Environment>();
                tracking.CurrentRoom = gameObject;
              
            }
            else if(player.gameObject.layer == LayerMask.NameToLayer("Interactable") && countDoors == true)
            {
                if(player.gameObject.tag == "Door")
                {
                    Doors.Add(player.gameObject);
                    BackgroundDoor_Environment doorInfo = player.GetComponent<BackgroundDoor_Environment>();
                    doorInfo.inRoom = gameObject;
                }
            }
        }
        countDoors = false;
    }

    public void EnemyTracking(GameObject enemy)
    {
        Collider2D[] EnemiesInRoom = Physics2D.OverlapAreaAll(RoomPointA.position, RoomPointB.position);

        foreach (Collider2D entity in EnemiesInRoom)
        {
            if(entity.gameObject == enemy)
            {
                RoomTracking_Zombie enemyTracker = enemy.GetComponent<RoomTracking_Zombie>();
                enemyTracker.ZombieCurrentRoom = gameObject;
            }
        }

    }
    /// <summary>
    /// Draws the room depending on where the corners of the room are
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (RoomPoint == null)
            return;
        Vector2 center = CenterOfRectangle();
        Vector2 area = AreaOfRectangle();
        Gizmos.DrawWireCube(center, area);
    }

    /// <summary>
    /// Calculates where the center of the rectangle is
    /// </summary>
    /// <returns></returns>
    private Vector2 CenterOfRectangle()
    {
        float width = RoomPointB.position.x - RoomPointA.position.x;
        float height = RoomPointA.position.y - RoomPointB.position.y;

        Vector2 centerPoint = new Vector2((RoomPointA.position.x + (width / 2)), (RoomPointB.position.y + (height / 2)));

        return centerPoint;
    }

    /// <summary>
    /// Calculates what the size of the room is
    /// </summary>
    /// <returns></returns>
    private Vector2 AreaOfRectangle()
    {
        float width = RoomPointB.position.x - RoomPointA.position.x;
        float height = RoomPointA.position.y - RoomPointB.position.y;

        Vector2 area = new Vector2(width, height);
      
        return area;
    }
}
