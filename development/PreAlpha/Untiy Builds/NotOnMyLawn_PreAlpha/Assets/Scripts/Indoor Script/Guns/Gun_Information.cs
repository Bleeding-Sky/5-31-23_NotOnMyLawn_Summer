using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Information : MonoBehaviour
{
    //Gun objects will take the information from this script and apply it to the gun that its attached to
    [Header("Gun Stats")]
    public int bulletCount;
    public float fireRate;
    public float recoil;
    public bool coolingDown;
    public GameObject bulletType;

    [Header("Gun Configurations")]
    public Transform rotationAndAimingPoint;
    public Transform handPos;
    public GameObject player;
    public GameObject AimingField;

    [Header("Gun State")]
    public bool isPickedUp;
}
