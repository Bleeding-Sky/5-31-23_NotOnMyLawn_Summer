using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PostionTracking : MonoBehaviour
{
    public Player_PositionTracker playerPosition;
    void Update()
    {
        playerPosition.playerPosition = transform.position;
    }
}
