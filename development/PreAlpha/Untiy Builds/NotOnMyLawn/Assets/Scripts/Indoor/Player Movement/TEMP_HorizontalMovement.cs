using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_HorizontalMovement : MonoBehaviour
{
    [Header("CONFIG")]
    public float drawnMoveSpeed = 3;
    public float holsteredMoveSpeed = 5;


    [Header("DEBUG")]
    public float currentMoveSpeed;
    public bool gunIsDrawn = false;

    public Rigidbody2D myRigidbody2D;
    public TEMP_PlayerStates PlayerStates;

    void Start()
    {
        currentMoveSpeed = holsteredMoveSpeed;
        HolsterGun();
    }

    private void Update()
    {
        //toggles gunIsDrawn boolean, updates speed var, and updates states
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (gunIsDrawn)
            {
                HolsterGun();

            }
            else if  (!gunIsDrawn)
            {
                DrawGun();
            }
        }
    }

    void FixedUpdate()
    {
        float velocityToBeAdded = 0;
        velocityToBeAdded = ProcessMovementInput(velocityToBeAdded);

        UpdatePlayerStates(velocityToBeAdded);

        //apply movement to rigidbody
        myRigidbody2D.velocity = new Vector2(velocityToBeAdded, 0);
    }

    private float ProcessMovementInput(float velocityToBeAdded)
    {
        //right movement
        if (Input.GetKey(KeyCode.A))
        {
            velocityToBeAdded -= currentMoveSpeed;
        }
        //left movement
        if (Input.GetKey(KeyCode.D))
        {
            velocityToBeAdded += currentMoveSpeed;
        }

        return velocityToBeAdded;
    }

    /// <summary>
    /// Updates isWalking and movementDirection states
    /// </summary>
    /// <param name="velocityToBeAdded"></param>
    private void UpdatePlayerStates(float velocityToBeAdded)
    {
        if (velocityToBeAdded == 0)
        {
            PlayerStates.isWalking = false;
        }
        else
        {
            PlayerStates.isWalking = true;
            if (velocityToBeAdded > 0)
            {
                PlayerStates.faceDirection = 1;
            }
            else if (velocityToBeAdded < 0)
            {
                PlayerStates.faceDirection = -1;
            }
        }
    }

    private void DrawGun()
    {
        gunIsDrawn = true;
        currentMoveSpeed = drawnMoveSpeed;

        PlayerStates.gunIsDrawn = true;
    }

    private void HolsterGun()
    {
        gunIsDrawn = false;
        currentMoveSpeed = holsteredMoveSpeed;

        PlayerStates.gunIsDrawn = false;
    }

}
