using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet_Window : MonoBehaviour
{
    public Rigidbody bulletRB;
    public Vector3 bulletDirection;
    public Transform startingPosition;
    public Vector3 mousePos;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPosition.position;
        bulletRB.velocity = bulletDirection * speed;
    }

    /// <summary>
    /// Destroys the bullet when hitting the enviornment or zombie
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Zombie"))
        {
            Destroy(gameObject);
            Debug.Log("bullet hit");
        }
    }

    /// <summary>
    /// Destroys the bullet when hitting the enviornment
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
