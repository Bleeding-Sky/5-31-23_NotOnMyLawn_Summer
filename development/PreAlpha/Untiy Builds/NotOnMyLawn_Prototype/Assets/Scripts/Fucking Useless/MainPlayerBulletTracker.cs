using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayerBulletTracker : MonoBehaviour
{
    public PlayerBulletCount bulletCount;
    public Text bulletText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletText.text = "Bullet Count: " + bulletCount.bulletCount.ToString();
    }
}
