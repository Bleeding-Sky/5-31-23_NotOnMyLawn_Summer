using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction_Player : MonoBehaviour
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
        InteractionIdentification_Item itemType = item.GetComponent<InteractionIdentification_Item>();

        if (itemType.isGun)
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
        GunInformation_Item GunInfo = gun.GetComponent<GunInformation_Item>();
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
