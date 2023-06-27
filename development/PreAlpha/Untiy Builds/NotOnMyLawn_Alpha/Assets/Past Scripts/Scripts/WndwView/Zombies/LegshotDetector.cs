using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegshotDetector : MonoBehaviour
{
    // Start is called before the first frame update
    public ZmbDamageManager damageManager;
    public PlayerBulletCount bulletCount;


    private void OnMouseDown()
    {
        if (bulletCount.bulletCount != 0)
        {
            damageManager.Legshot();
        }

    }
}
