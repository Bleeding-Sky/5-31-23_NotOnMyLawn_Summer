using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacZmb_PathfindingBASIC : MonoBehaviour
{

    [Header("CONFIG")]
    public Vector2 targetPos;
    public float moveSpeed;
    public bool canMove = true;

    [Header("DEBUG")]
    public Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        //fetch rigidbody
        myRigidbody2D= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //multiplies magnitude 1 vewwctor by movespeed, so magnitude = movespeed
            Vector2 movementVector = CalculateMovementVector();
            myRigidbody2D.velocity = movementVector * moveSpeed;
        }
        else if (!canMove)
        {
            myRigidbody2D.velocity = new Vector2(0, 0);
        }
    }

    /// <summary>
    /// calculates normalized vector (magnitude of 1) that points towards the window
    /// </summary>
    /// <returns></returns>
    Vector2 CalculateMovementVector()
    {
        float x = targetPos.x - transform.position.x;
        float y = targetPos.y - transform.position.y;
        Vector2 movementVect = new Vector2(x, y);
        movementVect.Normalize();
        return movementVect;
    }

}
