using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingOutside : MonoBehaviour
{
    public PlayerBulletCount bulletCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && bulletCount.bulletCount != 0)
        {
            bulletCount.bulletCount = bulletCount.bulletCount - 1;
        }
    }
}
