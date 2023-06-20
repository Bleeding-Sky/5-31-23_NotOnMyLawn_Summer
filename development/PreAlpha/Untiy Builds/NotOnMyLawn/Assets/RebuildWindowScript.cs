using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebuildWindowScript : MonoBehaviour
{
    public GameObject board1;
    public GameObject board2;
    public GameObject board3;

    public SpriteRenderer board1Color;
    public SpriteRenderer board2Color;
    public SpriteRenderer board3Color;

    public WindowBoardsDamageScript boardHealth;

    public bool canRepair;
    public float rebuildSpeed;

    public float boardOpacity;
    // Start is called before the first frame update
    void Start()
    {
        boardHealth.windowBoardDamage = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        Rebuild();

        if(canRepair == true && Input.GetKey(KeyCode.C) && boardHealth.windowBoardDamage <= 120)
        {
            Debug.Log("repairing");
            boardHealth.windowBoardDamage = boardHealth.windowBoardDamage + (rebuildSpeed * Time.deltaTime);
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
        }
        else if(boardHealth.windowBoardDamage >= 80)
        {
            board1.SetActive(true);
            board2.SetActive(true);
            board3.SetActive(false);
        }
        else if (boardHealth.windowBoardDamage >= 40)
        {
            board1.SetActive(true);
            board2.SetActive(false);
            board3.SetActive(false);
        }
        else if (boardHealth.windowBoardDamage >= 0)
        {
            board1.SetActive(false);
            board2.SetActive(false);
            board3.SetActive(false);
        }
    }
}
