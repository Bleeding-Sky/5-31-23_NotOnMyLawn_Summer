using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop_Item : MonoBehaviour
{

    public enum BulletTypes {Large, Medium, Small};
    public BulletTypes BulletType;
    public int bulletCount;
    public System.Random rand;
    // Start is called before the first frame update
    void Start()
    {
        DetermineBulletType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetermineBulletType()
    {
        rand = new System.Random();
        int randType = rand.Next(0, 3);

        if(randType == 0)
        {
            BulletType = BulletTypes.Large;
        }
        else if(randType == 1)
        {
            BulletType = BulletTypes.Medium;
        }
        else if(randType == 2)
        {
            BulletType = BulletTypes.Small;
        }

        DetermineAmount();

    }

    public void DetermineAmount()
    {
        if(BulletType == BulletTypes.Large)
        {
            rand = new System.Random();
            int randAmount = rand.Next(1, 5);
            bulletCount = randAmount;
        }
        else if(BulletType == BulletTypes.Medium)
        {
            rand = new System.Random();
            int randAmount = rand.Next(3, 8);
            bulletCount = randAmount;
        }
        else if(BulletType == BulletTypes.Small)
        {
            rand = new System.Random();
            int randAmount = rand.Next(6, 12);
            bulletCount = randAmount;
        }
    }

    public void DestroyPack()
    {
        Destroy(gameObject);
    }
}
