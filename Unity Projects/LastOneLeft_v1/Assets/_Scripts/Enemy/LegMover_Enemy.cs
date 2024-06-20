using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMover_Enemy : MonoBehaviour
{
    public Transform limbSolverTarget;
    public float moveDistance;
    public LayerMask groundLayer;
    public GameObject body;
    public float legOffset;
    public float hDir;
    public float vDir;

    // Update is called once per frame
    void Update()
    {
        hDir = Input.GetAxis("Horizontal");
        vDir = Input.GetAxis("Vertical");
        if (hDir != 0)
        {
            CheckGround();
        }

        if(Vector2.Distance(limbSolverTarget.position,transform.position) > moveDistance)
        {
            limbSolverTarget.position = transform.position;
        }
    }

    public void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, (float)0.1, groundLayer);
        RaycastHit2D leftHit = Physics2D.Raycast(gameObject.transform.position, Vector3.left, (float)0.1, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(gameObject.transform.position, Vector3.right, (float)0.1, groundLayer);
        if (hit.collider != null && leftHit.collider == null && rightHit.collider == null)
        {
            Vector3 point = hit.point;
            point.y += .1f;
            transform.position = point;
        }

        
    }
}
