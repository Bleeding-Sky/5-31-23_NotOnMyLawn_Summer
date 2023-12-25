using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Item : MonoBehaviour
{
    public Rigidbody2D bulletRB;

    public Vector3 firingPos;
    public Vector3 bulletStartPosition;
    public Vector3 bulletDirectionPosition;

    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //Initiates the bullet direction and speed
        bulletStartPosition = transform.position;
        Vector3 direction = firingPos - bulletDirectionPosition;
        Debug.Log(direction);
        bulletRB.velocity = new Vector3(direction.x, direction.y, 0).normalized * bulletSpeed;

    }

    /// <summary>
    /// Destroys the bullet when hitting the enviornment or zombie
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Zombie"))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Destroys the bullet when hitting the enviornment
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
