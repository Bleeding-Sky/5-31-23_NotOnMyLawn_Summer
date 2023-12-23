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
    
    /// <summary>
    /// Determines what type of item it is and initializes all of the necessary components for it to properly function
    /// </summary>
    /// <param name="item"></param>
    public void DetermineItemType(GameObject item)
    {
        Interaction_Identification itemType = item.GetComponent<Interaction_Identification>();

        if(itemType.isGun)
        {
            PickedUpGun(item);
        }
    }

    /// <summary>
    /// Picks up the gun and then tunes it to the player's information
    /// </summary>
    /// <param name="gun"></param>
    public void PickedUpGun(GameObject gun)
    {
        Gun_Information GunInfo = gun.GetComponent<Gun_Information>();
        BoxCollider2D gunCollider = gun.GetComponent<BoxCollider2D>();
        GunInfo.isPickedUp = true;

        //Accesses the gun information and gives it to the gun
        GunInfo.player = player;
        GunInfo.gameObject.transform.parent = hand.transform;
        GunInfo.rotationAndAimingPoint = armRotationPos;
        GunInfo.handPos = handPos;
        GunInfo.AimingField = AimingArea;
        gunCollider.enabled = false;
    }
}
