using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracking_Environment : MonoBehaviour
{
    //Hold values for the rooms
    public List<GameObject> Rooms;
    public GameObject CurrentRoom;
    public RoomTracking_Player player;

    private void Update()
    {
        //Updates the SO so that zombies can track player movement
        player.Rooms = Rooms;
        player.CurrentRoom = CurrentRoom;
    }
}
