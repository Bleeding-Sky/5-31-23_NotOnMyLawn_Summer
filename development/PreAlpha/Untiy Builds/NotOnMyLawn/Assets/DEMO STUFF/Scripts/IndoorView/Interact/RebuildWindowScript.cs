using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebuildWindowScript : MonoBehaviour
{
    public GameObject board1;
    public GameObject board2;
    public GameObject board3;

    public WindowBoardsDamageScript boardHealth;

    public bool canRepair;
    public float rebuildSpeed;
    public bool isRepairing;

    public float boardOpacity;

    public BoardTracker boardCounter;
    public int boardsOnWindow;
    public float timer;

    public float boardCounterTracker;

    // Start is called before the first frame update
    void Start()
    {
        boardHealth.windowBoardDamage = 0;
        isRepairing = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        Rebuild();

        if(canRepair == true && Input.GetKey(KeyCode.C) && boardHealth.windowBoardDamage <= 120)
        {
            Debug.Log("repairing");
            boardHealth.windowBoardDamage += 15*Time.deltaTime;
            isRepairing = true;      
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canRepair = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canRepair = false;
        }
    }
    
    void Rebuild()
    {
        if(boardHealth.windowBoardDamage >= 120)
        {
            board1.SetActive(true);
            board2.SetActive(true);
            board3.SetActive(true);
            boardsOnWindow = 3;
        }
        else if(boardHealth.windowBoardDamage >= 80)
        {
            board1.SetActive(true);
            board2.SetActive(true);
            board3.SetActive(false);
            boardsOnWindow = 2;
        }
        else if (boardHealth.windowBoardDamage >= 40)
        {
            board1.SetActive(true);
            board2.SetActive(false);
            board3.SetActive(false);
            boardsOnWindow = 1;

        }
        else if (boardHealth.windowBoardDamage >= 0)
        {
            board1.SetActive(false);
            board2.SetActive(false);
            board3.SetActive(false);
            boardsOnWindow = 0;
        }
    }

    
    
}
