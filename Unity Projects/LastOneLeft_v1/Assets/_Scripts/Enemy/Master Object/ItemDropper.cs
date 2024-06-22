using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{

    [Header("CONFIG")]

    //enable / disable drops
    [SerializeField] bool dropBandage = false;
    [SerializeField] bool dropAmmoPack = false;

    //drop chances
    [SerializeField] float bandageDropChance = 15;
    [SerializeField] float ammoDropChance = 10;
    
    //prefabs
    [SerializeField] GameObject bandagePrefab;
    [SerializeField] GameObject ammoPackPrefab;

    /// <summary>
    /// rolls chances for all items to drop, calls DropItem() on all drops that succeed the roll
    /// </summary>
    public void AttemptDrop()
    {
        if (    dropBandage &&
                RNGRolls_System.RollUnder(bandageDropChance))
        {
            DropItem(bandagePrefab);
        }

        if (    dropAmmoPack &&
                RNGRolls_System.RollUnder(ammoDropChance))
        {
            DropItem(ammoPackPrefab);
        }
    }


    void DropItem(GameObject item)
    {
        //looks for rigidbody 2d so that items can only drop inside
        Vector3 dropPosition = GetComponentInChildren<Rigidbody2D>().gameObject.transform.position;
        if (dropPosition != Vector3.zero)
        {
            Instantiate(item, dropPosition, Quaternion.identity);
        }
    }

}
