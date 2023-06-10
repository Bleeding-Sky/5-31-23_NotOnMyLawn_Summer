using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_IndoorZmbMovement : MonoBehaviour
{

    public GameObject playerObject;
    public float moveSpeed;
    public int moveDirection;
    public Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerXPos = playerObject.transform.position.x;
        float myXPos = transform.position.x;
        
        if (playerXPos > myXPos)
        {
            moveDirection = 1;
        }
        else if (playerXPos < myXPos)
        {
            moveDirection = -1;
        }

        myRigidbody2D.velocity = new Vector2(moveSpeed * moveDirection, 0);

        //flips x scale to make zombie face movement direction
        transform.localScale = new Vector3(moveDirection, 1, 1);

    }
}
