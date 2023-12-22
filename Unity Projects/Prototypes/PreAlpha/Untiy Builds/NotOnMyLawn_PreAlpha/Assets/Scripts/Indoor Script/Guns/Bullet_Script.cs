using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    public Rigidbody2D bulletRB;

    public Vector3 firingPos;
    public Vector3 bulletStartPosition;
    public Vector3 bulletDirectionPosition;

    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {       
        bulletStartPosition = transform.position;
        Vector3 direction = firingPos - bulletDirectionPosition;
        Debug.Log(direction);
        bulletRB.velocity = new Vector3(direction.x, direction.y, 0).normalized * bulletSpeed;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Zombie"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
