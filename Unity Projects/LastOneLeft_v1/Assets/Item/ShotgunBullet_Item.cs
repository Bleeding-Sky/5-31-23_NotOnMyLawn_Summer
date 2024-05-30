using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet_Item : MonoBehaviour
{
    public Rigidbody2D bulletRB;

    public Vector3 bulletStartPosition;
    public float xDirection;
    public float yDirection;

    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //Initiates the bullet direction and speed
        bulletStartPosition = transform.position;
        bulletRB.velocity = new Vector3(xDirection, yDirection, 0).normalized * bulletSpeed;

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
