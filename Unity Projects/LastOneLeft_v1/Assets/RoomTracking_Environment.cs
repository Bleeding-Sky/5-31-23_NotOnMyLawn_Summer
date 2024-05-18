using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracking_Environment : MonoBehaviour
{
    public List<GameObject> Rooms;
    public GameObject CurrentRoom;
    public RoomTracking_Player player;

    private void Update()
    {
        player.Rooms = Rooms;
        player.CurrentRoom = CurrentRoom;
    }
}
