using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Room Position")]
public class RoomTracking_Player : ScriptableObject
{
    public List<GameObject> Rooms;
    public GameObject CurrentRoom;
}
