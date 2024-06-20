using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBrain_Enemy : MonoBehaviour
{
    public LayerMask groundLayer;
    public float speed;
    public bool canGo;
    public float hDir;
    public float vDir;

    // Update is called once per frame
    void Update()
    {
        hDir = Input.GetAxis("Horizontal");
        vDir = Input.GetAxis("Vertical");
        if (canGo)
        {
            transform.position = new Vector3(transform.position.x + (hDir * speed) * Time.deltaTime, transform.position.y + (vDir * speed) * Time.deltaTime, transform.position.z);
        }
        else
        {
            RaycastHit2D leftHit = Physics2D.Raycast(gameObject.transform.position, Vector3.left, 1, groundLayer);
            RaycastHit2D rightHit = Physics2D.Raycast(gameObject.transform.position, Vector3.right, 1, groundLayer);
            if (hDir == 1 && leftHit)
            {
                transform.position = new Vector3(transform.position.x + (1 * speed) * Time.deltaTime, transform.position.y + (vDir * speed) * Time.deltaTime, transform.position.z);
            }
            else if(hDir == -1 && rightHit)
            {
                transform.position = new Vector3(transform.position.x + (-1 * speed) * Time.deltaTime, transform.position.y + (vDir * speed) * Time.deltaTime, transform.position.z);
            }


        }
        
        CalculateGround();
    }

    public void CalculateGround()
    {
        RaycastHit2D upHit = Physics2D.Raycast(gameObject.transform.position, Vector3.up, (float)0.1, groundLayer);
        RaycastHit2D downHit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, (float)0.1, groundLayer);
        RaycastHit2D leftHit = Physics2D.Raycast(gameObject.transform.position, Vector3.left, (float)0.1, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(gameObject.transform.position, Vector3.right, (float)0.1, groundLayer);

        if(leftHit || rightHit || downHit || upHit)
        {
            canGo = false;
        }
        else
        {
            canGo = true;
        }
    }
}