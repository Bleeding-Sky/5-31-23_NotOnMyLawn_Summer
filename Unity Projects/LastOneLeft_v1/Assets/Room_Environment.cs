using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Environment : MonoBehaviour
{
    public Transform RoomPoint;

    //Upper left bound of room
    public Transform RoomPointA;

    //Lower right bound of room
    public Transform RoomPointB;

    //Room width and length
    public Vector2 RoomSize;

    //Door list
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
            //Seaches the player layer and if it is in the room then it updates the current room
            if (player.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                RoomTracking_Environment tracking = gameObject.transform.parent.GetComponent<RoomTracking_Environment>();
                tracking.CurrentRoom = gameObject;     
            }
            //Finds the number of doors in each room and adds it to a list of doors in the room
            //**Only runs once at the start of the game since rooms and doors dont change**
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

    /// <summary>
    /// Called on the enemy Room tracking script when it enters a new room to give xombies their current room location
    /// </summary>
    /// <param name="enemy"></param>
    public void EnemyTracking(GameObject enemy)
    {
        Collider2D[] EnemiesInRoom = Physics2D.OverlapAreaAll(RoomPointA.position, RoomPointB.position);

        //Finds the enemy in the layer and sets the current room as the one it is in
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
