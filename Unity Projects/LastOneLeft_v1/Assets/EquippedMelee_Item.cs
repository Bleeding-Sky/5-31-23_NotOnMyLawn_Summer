using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedMelee_Item : MonoBehaviour
{
    [Header("Melee Weapon Configurations")]
    public Transform rotationAndAimingPoint;
    public PositionTracker_Player tracker;
    public Transform handPos;
    public GameObject player;

    public bool pickedUp;
    public float rotationAngle;

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            ItemInHand();
            weaponDirection();
        }
    }

    /// <summary>
    /// Attaches all the player's stats to the item
    /// </summary>
    public void ItemInHand()
    {
        transform.position = handPos.position;
        ArmRotation_Player zRotation = rotationAndAimingPoint.GetComponent<ArmRotation_Player>();
        rotationAngle = zRotation.itemRotation;
        transform.localRotation = Quaternion.Euler(0, 0, rotationAngle);
    }

    /// <summary>
    /// Determines the knockback direction based on the rotation angle of the mouse
    /// </summary>
    /// <returns></returns>
    public Vector2 KnockbackDirection()
    {
        float rotation = rotationAngle;
        float xDirection = (float)Math.Cos((Math.PI / 180) * rotation);
        float yDirection = (float)Math.Sin((Math.PI / 180) * rotation);
        return new Vector2(xDirection, yDirection);
    }

    public void weaponDirection()
    {
        transform.localScale = new Vector3(1, 1 * tracker.playerDirection, 1);
    }
}
