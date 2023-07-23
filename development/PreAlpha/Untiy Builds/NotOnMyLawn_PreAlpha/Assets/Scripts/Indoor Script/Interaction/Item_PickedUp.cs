using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_PickedUp : MonoBehaviour
{
    public GameObject player;
    public GameObject hand;
    public Transform armRotationPos;
    public Transform handPos;
    public GameObject AimingArea;
    
    public void DetermineItemType(GameObject item)
    {
        Interaction_Identification itemType = item.GetComponent<Interaction_Identification>();

        if(itemType.isGun)
        {
            PickedUpGun(item);
        }
    }
    public void PickedUpGun(GameObject gun)
    {
        Gun_Information GunInfo = gun.GetComponent<Gun_Information>();
        BoxCollider2D gunCollider = gun.GetComponent<BoxCollider2D>();
        GunInfo.isPickedUp = true;
        GunInfo.player = player;
        GunInfo.gameObject.transform.parent = hand.transform;
        GunInfo.rotationAndAimingPoint = armRotationPos;
        GunInfo.handPos = handPos;
        GunInfo.AimingField = AimingArea;
        gunCollider.enabled = false;
    }
}
