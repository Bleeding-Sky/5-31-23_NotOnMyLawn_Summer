using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float horizontalDir;
    public float speed;

    public Rigidbody2D playerRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDir = HorizontalMovement();

        if(horizontalDir != 0)
        {
            playerRigidBody.velocity = new Vector2(horizontalDir * speed, 0);
        }
        else if (horizontalDir == 0)
        {
            playerRigidBody.velocity = new Vector2(0, 0);
        }
    }

    float HorizontalMovement()
    {
        float hMove = Input.GetAxisRaw("Horizontal");

        return hMove;
    }
}
