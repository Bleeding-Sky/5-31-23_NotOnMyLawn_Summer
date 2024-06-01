using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_Player : MonoBehaviour
{
    [Header("CONFIG")]
    public float drawnMoveSpeed = 2;
    public float holsteredMoveSpeed = 5;

    [Header("DEBUG")]
    public float inputDirection;
    public float currentMoveSpeed;

    public Rigidbody2D myRigidbody2D;
    public States_Player playerStates;
    public PositionTracker_Player playerPosition;

    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessGunStatus();

        float newVelocity = inputDirection * currentMoveSpeed * playerStates.playerMobility;

        UpdatePlayerStates(newVelocity);

        //apply movement to rigidbody
        myRigidbody2D.velocity = new Vector2(newVelocity, 0);

        //sends player's position to scriptable object
        playerPosition.playerPosition = transform.position;

    }

    

    public void OnMoveAction(InputAction.CallbackContext actionContext)
    {
        if (actionContext.performed)
        {
            inputDirection = actionContext.ReadValue<float>();
        }
        else if (actionContext.canceled)
        {
            inputDirection = 0;
        }

        playerStates.inputDirection = inputDirection;
    }

    /// <summary>
    /// sets speed based on gun's drawn/holstered status
    /// </summary>
    public void ProcessGunStatus()
    {
        if (playerStates.gunIsDrawn)
        {
            currentMoveSpeed = drawnMoveSpeed;
        }
        //if holstered
        else
        {
            currentMoveSpeed = holsteredMoveSpeed;
        }
    }

    /// <summary>
    /// Updates isWalking and movementDirection states
    /// </summary>
    /// <param name="newVelocity"></param>
    private void UpdatePlayerStates(float newVelocity)
    {
        if (newVelocity == 0)
        {
           playerStates.isWalking = false;
        }
        else
        {
            playerStates.isWalking = true;

            //update face direction when moving
            if (newVelocity > 0)
            {
               playerStates.faceDirection = 1;
            }
            else if (newVelocity < 0)
            {
                playerStates.faceDirection = -1;
            }
        }
    }
}
