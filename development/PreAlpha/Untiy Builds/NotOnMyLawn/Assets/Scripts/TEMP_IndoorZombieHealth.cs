using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_IndoorZombieHealth : MonoBehaviour
{
    public int health = 5;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health--;
        }
        
    }

}
