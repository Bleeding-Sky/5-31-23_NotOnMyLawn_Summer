using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPenetration_Parent : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float critPenetrationPenalty = 1;
    [SerializeField] float armoredPenetrationPenalty = 4;
    [SerializeField] float weakPenetrationPenalty = 2;

    [Header("DEBUG")]
    [SerializeField] BulletInfo myBulletInfo;


    private void Awake()
    {
        myBulletInfo = GetComponent<BulletInfo>();
    }

    protected virtual void ProcessPenetration(DmgRegionEnum damageRegion)
    {
        switch (damageRegion)
        {

            case DmgRegionEnum.Crit:
                myBulletInfo.bulletPenetration -= critPenetrationPenalty; 
                break;

            case DmgRegionEnum.Armored:
                myBulletInfo.bulletPenetration -= armoredPenetrationPenalty;
                break;

            case DmgRegionEnum.Weak:
                myBulletInfo.bulletPenetration -= weakPenetrationPenalty;
                break;

        }

        if (myBulletInfo.bulletPenetration <= 0)
        {
            Destroy(gameObject);
        }

    }

}
