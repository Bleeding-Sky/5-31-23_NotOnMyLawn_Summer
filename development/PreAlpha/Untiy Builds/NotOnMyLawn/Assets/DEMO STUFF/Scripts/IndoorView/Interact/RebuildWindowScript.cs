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

        if(canRepair == true && Input.GetKey(KeyCode.C) && boardsOnWindow != 3 && boardCounter.boardCounter !=0)
        {
            Debug.Log("repairing");
            boardHealth.windowBoardDamage = boardHealth.windowBoardDamage + (rebuildSpeed * Time.deltaTime);
            isRepairing = true;
            timer += Time.deltaTime;
        }

        if(timer >= 2.5)
        {
            StartCoroutine(BoardPlacementDelay());
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
        if(boardsOnWindow == 3)
        {
            board1.SetActive(true);
            board2.SetActive(true);
            board3.SetActive(true);
        }
        else if(boardsOnWindow == 2)
        {
            board1.SetActive(true);
            board2.SetActive(true);
            board3.SetActive(false);
        }
        else if (boardsOnWindow == 1)
        {
            board1.SetActive(true);
            board2.SetActive(false);
            board3.SetActive(false);
        }
        else if (boardsOnWindow == 0)
        {
            board1.SetActive(false);
            board2.SetActive(false);
            board3.SetActive(false);
        }
    }

    IEnumerator BoardPlacementDelay()
    {
        boardCounter.boardCounter -= 1;
        boardsOnWindow += 1;
        timer = 0;
        yield return new WaitForSeconds(.5f);
        

    }
}
