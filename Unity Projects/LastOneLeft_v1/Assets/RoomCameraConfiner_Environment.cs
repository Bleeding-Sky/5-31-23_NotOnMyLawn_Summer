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
        if(currentRoom != roomTracker.CurrentRoom)
        {
           CinemachineConfiner2D indoorConfiner = indoor.GetComponent<CinemachineConfiner2D>();
           indoorConfiner.InvalidateCache();
        }
        currentRoom = roomTracker.CurrentRoom;

        if(currentRoom != null)
        { 
            DefineRoomPosition();
        }
    }

    public void DefineRoomPosition()
    {
        Room_Environment currentRoomInfo = currentRoom.GetComponent<Room_Environment>();
        float height = currentRoomInfo.RoomPointA.position.y - currentRoomInfo.RoomPointB.position.y;
        float length = currentRoomInfo.RoomPointB.position.x - currentRoomInfo.RoomPointA.position.x;
        
        Vector2 roomPos = currentRoomInfo.RoomPointA.position - new Vector3(-(length / 2), height / 2, 0);
        roomConfiner.transform.position = roomPos; 

        SizeCollider(height/2, length/2, roomPos);
    }

    public void SizeCollider(float heightOffset, float lengthOffset, Vector2 roomPos)
    {
        roomConfinerCollider.points = new[] { new Vector2(lengthOffset, -heightOffset), 
                                              new Vector2(lengthOffset, heightOffset), 
                                              new Vector2(-lengthOffset, heightOffset), 
                                              new Vector2(-lengthOffset, -heightOffset)}; 
   
    }


}
