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
        boards.Add(Board1);
        boards.Add(Board2);
        boards.Add(Board3);
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

    private void CheckForZombiesOnWindow()
    {
        if(!zombiesBreakingWindow)
        {
            boardHealth = boardsOnWindow * 40;
            zombieTimer = 0;
        }
        else if(zombiesBreakingWindow)
        {
            RemoveBoard();
        }
    }

    public void AddBoard()
    {
        if(timer < 2.5f)
        {
            Debug.Log("COUNTING UP");
            timer += Time.deltaTime;
        }
        else if(timer >= 2.5)
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

    private void RemoveBoard()
    {
        if(zombieTimer < 25 && boardsOnWindow != 0)
        {
            zombieTimer = zombieTimer + (zombiesOnWindow * Time.deltaTime);
        }
        else if(zombieTimer >= 25)
        {
            boards[boardElementInList].SetActive(false);
            zombieTimer = 0;
            boardsOnWindow -= 1;
            boardHealth -= 40;
            boardElementInList = boardsOnWindow - 1;

        }
    }

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
