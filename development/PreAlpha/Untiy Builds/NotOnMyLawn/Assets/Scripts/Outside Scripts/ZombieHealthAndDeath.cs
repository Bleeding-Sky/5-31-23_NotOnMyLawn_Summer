using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthAndDeath : MonoBehaviour
{
    public PlayerBulletCount bulletCount;
    public PointTracker points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (bulletCount.bulletCount != 0)
        {
            points.points += 20;
            Destroy(gameObject);
        }

    }
}
