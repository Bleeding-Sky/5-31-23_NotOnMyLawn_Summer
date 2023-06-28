using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadshotDetector : MonoBehaviour
{
    public ZmbDamageManager damageManager;
    public PlayerBulletCount bulletCount;
    

    private void OnMouseDown()
    {
        if (bulletCount.bulletCount != 0)
        {
            damageManager.Headshot();
        }

    }

}
