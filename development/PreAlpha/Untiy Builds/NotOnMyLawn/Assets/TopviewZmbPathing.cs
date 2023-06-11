using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopviewZmbPathing : MonoBehaviour
{

    public GameObject windowObject;
    public float moveSpeed;
    public Rigidbody2D myRigidbody2D;

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = CalculateMovementVector();
        myRigidbody2D.velocity = movementVector * moveSpeed;
    }

    Vector2 CalculateMovementVector()
    {
        //calculate normalized vector that points towards the window
        float x = windowObject.transform.position.x - transform.position.x;
        float y = windowObject.transform.position.y - transform.position.y;
        Vector2 movementVect= new Vector2(x, y);
        movementVect.Normalize();
        return movementVect;
    }

}
