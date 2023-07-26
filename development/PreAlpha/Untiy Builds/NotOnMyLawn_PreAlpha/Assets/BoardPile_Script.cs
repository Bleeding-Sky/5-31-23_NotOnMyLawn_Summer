using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPile_Script : MonoBehaviour
{
    public Player_States boardCount;
    public float boardPickUpTimer;
    public float pickUpSpeed;
    public float timerMaxTime;

    public void PickUpBoard()
    {
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

    public void ResetBoardPickUp()
    {
        boardPickUpTimer = 0;
    }
}
