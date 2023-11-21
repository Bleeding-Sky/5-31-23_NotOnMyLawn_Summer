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

    private void RoomBeingUsed()
    {
        Room_Script usedRoom = InRoom.GetComponent<Room_Script>();
        Room_MapCounterPart mapRoom = usedRoom.mapCounterPart.GetComponent<Room_MapCounterPart>();

        mapRoom.emptyRoom.SetActive(false);
        mapRoom.occupiedRoom.SetActive(true);
    }

    private void CheckIfRoomChanged()
    {
        if(InRoom != LastRoomOccupied && LastRoomOccupied != null)
        {
            Room_Script usedRoom = LastRoomOccupied.GetComponent<Room_Script>();
            Room_MapCounterPart mapRoom = usedRoom.mapCounterPart.GetComponent<Room_MapCounterPart>();

            mapRoom.emptyRoom.SetActive(true);
            mapRoom.occupiedRoom.SetActive(false);
            LastRoomOccupied = InRoom;
        }
        else if(LastRoomOccupied == null)
        {
            LastRoomOccupied = InRoom;
        }
    }
}
