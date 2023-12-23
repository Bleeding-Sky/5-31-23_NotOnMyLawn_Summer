using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_UI : MonoBehaviour
{
    public GameObject RoomTrackingSystem;
    public GameObject InRoom;
    public GameObject LastRoomOccupied;
    // Start is called before the first frame update
    void Start()
    { 
        Room_Tracking tracking = RoomTrackingSystem.GetComponent<Room_Tracking>();
        InRoom = tracking.OccupiedRoom;
        LastRoomOccupied = InRoom;
    }
    // Update is called once per frame
    void Update()
    {
        Room_Tracking tracking = RoomTrackingSystem.GetComponent<Room_Tracking>();
        //Each of the objects with this script have a room UI and in game counter part
        //They are connected and will light up depending on 
        //which room is being used by the player
        if (tracking.OccupiedRoom == null)
        {
            Debug.Log("room is not declared");
        }
        else if (tracking.OccupiedRoom != null)
        {
            InRoom = tracking.OccupiedRoom;
            CheckIfRoomChanged();
            RoomBeingUsed();
        }
    }

    /// <summary>
    /// If the room is occupied then the players location is updated 
    /// </summary>
    private void RoomBeingUsed()
    {
        Room_Script usedRoom = InRoom.GetComponent<Room_Script>();
        Room_MapCounterPart mapRoom = usedRoom.mapCounterPart.GetComponent<Room_MapCounterPart>();

        mapRoom.emptyRoom.SetActive(false);
        mapRoom.occupiedRoom.SetActive(true);
    }

    /// <summary>
    /// Checks if the room's used location is changed
    /// </summary>
    private void CheckIfRoomChanged()
    {
        if(InRoom != LastRoomOccupied && LastRoomOccupied != null)
        {
            Room_Script usedRoom = LastRoomOccupied.GetComponent<Room_Script>();
            Room_MapCounterPart mapRoom = usedRoom.mapCounterPart.GetComponent<Room_MapCounterPart>();

            //Keeps track of the last occupied and updates based on if it is the same of the currently used room
            mapRoom.emptyRoom.SetActive(true);
            mapRoom.occupiedRoom.SetActive(false);
            LastRoomOccupied = InRoom;
        }
        //If there is no change then things will remain the same
        else if(LastRoomOccupied == null)
        {
            LastRoomOccupied = InRoom;
        }
    }
}
