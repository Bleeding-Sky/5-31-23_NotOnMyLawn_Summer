using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_Barricade : MonoBehaviour
{
    public GameObject Board1;
    public GameObject Board2;
    public GameObject Board3;

    public List<GameObject> boards;

    public int boardsOnWindow;
    public int boardElementInList;
    public float boardHealth;
    public float timer;

    public bool boardsInInventory;
    public Player_States boardCounter;
    public bool windowIsRepairable;

    public int zombiesOnWindow;
    public bool zombiesBreakingWindow;
    public float zombieTimer;
    // Start is called before the first frame update
    void Start()
    {
        //Intializes the boards and how many are allowed in it 
        boards.Add(Board1);
        boards.Add(Board2);
        boards.Add(Board3);

        //timer based repair and board amount initialization
        timer = 0;
        boardCounter.boardsOnPlayer = 6;
        boardHealth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Window_Interact window = GetComponent<Window_Interact>();
        CheckForBoardsOnPlayer();
        CheckForZombiesOnWindow();
    }

    /// <summary>
    /// Checks if there are any zombies currently tryring to break the window
    /// </summary>
    private void CheckForZombiesOnWindow()
    {
        //If they arent then the board health and timer remain the same
        if(!zombiesBreakingWindow)
        {
            boardHealth = boardsOnWindow * 40;
            zombieTimer = 0;
        }
        //otherwise they begin to break the window
        else if(zombiesBreakingWindow)
        {
            RemoveBoard();
        }
    }

    /// <summary>
    /// Adds the board on to the window
    /// </summary>
    public void AddBoard()
    {
        //If the timer is held down to 2.5 seconds then the window builds a board
        //and updates everything according to that and resets the timers
        if (timer < 2.5f)
        {
            Debug.Log("COUNTING UP");
            timer += Time.deltaTime;
        }
        else if (timer >= 2.5)
        {
            timer = 0;
            zombieTimer = 0;
            boardsOnWindow += 1;
            boardCounter.boardsOnPlayer -= 1;
            boardHealth += 40;
            boardElementInList = boardsOnWindow - 1;
            boards[boardElementInList].SetActive(true);
        }
    }

    /// <summary>
    /// Removes the board according to zombie and enemies on the wall
    /// </summary>
    private void RemoveBoard()
    {
        //The timer is based on if the zombies stay on the window long enough
        //If the timer is less than 25 then it will continue to go up
        if(zombieTimer < 25 && boardsOnWindow != 0)
        {
            zombieTimer = zombieTimer + (zombiesOnWindow * Time.deltaTime);
        }
        //If the timer reaches 25 then the zombies break a board on the window and 
        //resets the timer and all of the windows statistics
        else if(zombieTimer >= 25)
        {
            boards[boardElementInList].SetActive(false);
            zombieTimer = 0;
            boardsOnWindow -= 1;
            boardHealth -= 40;
            boardElementInList = boardsOnWindow - 1;

        }
    }

    /// <summary>
    /// Checks if the player has boards in their inventory
    /// </summary>
    private void CheckForBoardsOnPlayer()
    {
        if(boardCounter.boardsOnPlayer <= 0)
        {
            boardsInInventory = false;
        }
        else if(boardCounter.boardsOnPlayer > 0)
        {
            boardsInInventory = true;
        }
    }
}
