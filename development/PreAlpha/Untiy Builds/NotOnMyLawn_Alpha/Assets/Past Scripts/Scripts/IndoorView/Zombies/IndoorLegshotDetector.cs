using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorLegshotDetector : MonoBehaviour
{
    public TEMP_IndoorZombieHealth damageManager;
    public PlayerBulletCount bulletCount;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            damageManager.Legshot();
        }

    }
}
