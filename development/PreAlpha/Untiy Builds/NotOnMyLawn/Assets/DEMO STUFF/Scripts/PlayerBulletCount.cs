using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenuAttribute(menuName = "Player Bullet Count")]
public class PlayerBulletCount : ScriptableObject
{
    public float bulletCount = 10;
   
    // Start is called before the first frame update

    public void ResetBulletCount()
    {
        bulletCount = 10;
    }
}



