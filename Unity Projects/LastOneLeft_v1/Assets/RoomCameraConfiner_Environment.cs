using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomCameraConfiner_Environment : MonoBehaviour
{
    RoomTracking_Environment roomTracker;
    public GameObject currentRoom;
    public CinemachineVirtualCamera indoor;
    public PolygonCollider2D roomConfinerCollider;
    public GameObject roomConfiner;
    // Start is called before the first frame update
    void Start()
    {
        roomTracker = GetComponent<RoomTracking_Environment>();
        roomConfinerCollider = roomConfiner.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks to see if the player changes rooms
        if(currentRoom != roomTracker.CurrentRoom)
        {
           //If the player changes rooms then the confiner is updated and the cache is invalidated 
           CinemachineConfiner2D indoorConfiner = indoor.GetComponent<CinemachineConfiner2D>();
           indoorConfiner.InvalidateCache();
        }

        currentRoom = roomTracker.CurrentRoom;

        if(currentRoom != null)
        { 
            DefineRoomPosition();
        }
    }

    /// <summary>
    /// Uses the Point A and Point B positions from the room system to determine
    /// The length and height of the room in order to confine the camera with the sizes
    /// </summary>
    public void DefineRoomPosition()
    {
        //Point A and B information is being held on the current room that the player is in so
        //that information is used to define the size of the polygon collider
        Room_Environment currentRoomInfo = currentRoom.GetComponent<Room_Environment>();
        float height = currentRoomInfo.RoomPointA.position.y - currentRoomInfo.RoomPointB.position.y;
        float length = currentRoomInfo.RoomPointB.position.x - currentRoomInfo.RoomPointA.position.x;
        
        //Room Center is defined and the collider is set to it as to center it
        Vector2 roomPos = currentRoomInfo.RoomPointA.position - new Vector3(-(length / 2), height / 2, 0);
        roomConfiner.transform.position = roomPos; 

        SizeCollider(height/2, length/2, roomPos);
    }

    /// <summary>
    /// Defines the size of the collider by offsetting each of the corners and making the rooms dimensions
    /// </summary>
    /// <param name="heightOffset"></param>
    /// <param name="lengthOffset"></param>
    /// <param name="roomPos"></param>
    public void SizeCollider(float heightOffset, float lengthOffset, Vector2 roomPos)
    {
        roomConfinerCollider.points = new[] { new Vector2(lengthOffset, -heightOffset), 
                                              new Vector2(lengthOffset, heightOffset), 
                                              new Vector2(-lengthOffset, heightOffset), 
                                              new Vector2(-lengthOffset, -heightOffset)}; 
   
    }


}
