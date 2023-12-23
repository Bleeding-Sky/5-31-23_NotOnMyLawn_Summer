using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PostionTracking : MonoBehaviour
{
    public Player_PositionTracker playerPosition;
    /// <summary>
    /// Updates the players position so it can be used by other scripts and enemies
    /// </summary>
    void Update()
    {
        playerPosition.playerPosition = transform.position;
    }
}
