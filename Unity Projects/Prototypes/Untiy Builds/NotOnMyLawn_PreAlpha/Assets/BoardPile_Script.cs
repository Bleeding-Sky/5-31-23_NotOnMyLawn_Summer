using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPile_Script : MonoBehaviour
{
    [Header("CONFIG")]
    public float boardPickUpTimer;
    public float pickUpSpeed;
    public float timerMaxTime;
    [Header("DEBUG")]
    public Player_States boardCount;
    
    /// <summary>
    /// Board pick up is based on a timer
    /// </summary>
    public void PickUpBoard()
    {
        //If timer reaches the max time then the player gets a board
        if (boardPickUpTimer < timerMaxTime)
        {
            boardPickUpTimer += pickUpSpeed * Time.deltaTime;
        }
        else if(boardPickUpTimer >= timerMaxTime)
        {
            boardCount.boardsOnPlayer += 1;
            boardPickUpTimer = 0;
        }
    }

    /// <summary>
    /// Used to reset the timer whenever the player is interpupted or lets go of the pick up button
    /// </summary>
    public void ResetBoardPickUp()
    {
        boardPickUpTimer = 0;
    }
}
