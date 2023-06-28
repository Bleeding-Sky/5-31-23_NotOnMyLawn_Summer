using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopviewCheckBoardHealth : MonoBehaviour
{
    public WindowBoardsDamageScript windowBoardHealth;
    public Vector2 zombiePosition;
    public Vector2 windowPosition;

    public bool boardsAreUp;
    public bool atWindow;

    public GameObject indoorZombie;
    public Vector3 indoorWindow;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        TopviewZmbPathing zombiePath = GetComponent<TopviewZmbPathing>();
        TEMP_IndoorZombieHealth zombieHealth = indoorZombie.GetComponent<TEMP_IndoorZombieHealth>();
        TopviewZmbHealth tacZombieHealth = GetComponent<TopviewZmbHealth>();
        zombiePosition = transform.position;
        FindWindowPosition();
        checkForBoards();
        checkIfAtWindow();
        if (boardsAreUp && atWindow)
        {
            DestroyWindow();
            zombiePath.canMove = false;
        }
        else if (!boardsAreUp && atWindow)
        {
            zombiePath.canMove = false;
            zombieHealth.currentHealth = tacZombieHealth.currentHealth;
            Instantiate(indoorZombie, indoorWindow, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void DestroyWindow()
    {
        windowBoardHealth.windowBoardDamage -= 2 *Time.deltaTime;
    }
    public void FindWindowPosition()
    {
        TopviewZmbPathing zombiePath = GetComponent<TopviewZmbPathing>();
        windowPosition = zombiePath.windowObject.transform.position;
    }

    public void checkIfAtWindow()
    {
        
        if(zombiePosition.y <= windowPosition.y + .5)
        {
            atWindow = true;
        }
    }
    public void checkForBoards()
    {
        if (windowBoardHealth.windowBoardDamage > 0)
        {
            boardsAreUp = true;
        }
        else if (windowBoardHealth.windowBoardDamage <= 0)
        {
            boardsAreUp = false;
        }
    }

}
