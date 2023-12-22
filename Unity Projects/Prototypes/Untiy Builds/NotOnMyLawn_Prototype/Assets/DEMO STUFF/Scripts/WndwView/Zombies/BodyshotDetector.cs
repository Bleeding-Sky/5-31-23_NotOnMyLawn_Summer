using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyshotDetector : MonoBehaviour
{
    public ZmbDamageManager damageManager;
    public PlayerBulletCount bulletCount;


    private void OnMouseDown()
    {
        if (bulletCount.bulletCount != 0)
        {
            damageManager.Bodyshot();
        }

    }
}
