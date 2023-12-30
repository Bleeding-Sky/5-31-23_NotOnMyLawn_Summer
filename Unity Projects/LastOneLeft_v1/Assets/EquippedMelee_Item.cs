using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedMelee_Item : MonoBehaviour
{
    [Header("Melee Weapon Configurations")]
    public Transform rotationAndAimingPoint;
    public Transform handPos;
    public GameObject player;

    public bool pickedUp;

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            ItemInHand();
        }
    }

    public void ItemInHand()
    {
        transform.position = handPos.position;
        ArmRotation_Player zRotation = rotationAndAimingPoint.GetComponent<ArmRotation_Player>();
        float rotZ = zRotation.itemRotation;
        transform.localRotation = Quaternion.Euler(0, 0, rotZ);
    }
}
