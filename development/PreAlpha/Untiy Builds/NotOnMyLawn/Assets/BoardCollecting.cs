using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCollecting : MonoBehaviour
{
    public BoardTracker boardCounter;
    public bool canCollectBoard;
    public bool inBoardArea;
    // Start is called before the first frame update
    void Start()
    {
        boardCounter.boardCounter = 0;
        canCollectBoard = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canCollectBoard && inBoardArea && Input.GetKeyDown(KeyCode.E) && boardCounter.boardCounter <= 6)
        {
            boardCounter.boardCounter += 1;
            StartCoroutine(BoardCollectingDelay());
        }
        
    }

    IEnumerator BoardCollectingDelay()
    {
        canCollectBoard = false;
        yield return new WaitForSeconds(.5f);
        canCollectBoard = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inBoardArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inBoardArea = false;
        }
    }
}
