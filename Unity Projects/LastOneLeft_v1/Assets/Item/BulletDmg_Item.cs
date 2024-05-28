using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDmg_Item : MonoBehaviour
{

    public BulletData_Item bulletDataSO;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        //fetch bullet damage from scriptable object
        damage = bulletDataSO.damage;
    }

}
