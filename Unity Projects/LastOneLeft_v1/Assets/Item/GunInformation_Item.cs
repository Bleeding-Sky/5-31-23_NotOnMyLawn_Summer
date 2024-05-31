using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInformation_Item : MonoBehaviour
{
    [Header("Gun Type")]
    public bool automatic;
    public bool semiAutomatic;
    public AmmoDrop_Item.BulletTypes bulletType;

    //Gun objects will take the information from this script and apply it to the gun that its attached to
    [Header("Gun Stats")]
    public int bulletsPerShot;
    public int magSize;
    public float fireRate;
    public float reload;
    public float mobility;
    public GameObject indoorbullet;
    public GameObject windowBullet;
    [Range(0, 360)]
    public float shotSpread;

    [Header("Bullet Information")]
    public float damage;
    public float bulletPenetration;
    public float statusMultiplier;
    public float critDamageMultiplier;
    public float armoredDamageMultiplier;
    public float weakDamageMultiplier;


    [Header("Gun Configurations")]
    public Transform rotationAndAimingPoint;
    public Transform handPos;
    public GameObject player;
    public GameObject currentCamera;
    public States_Player playerStates;
    public CameraManagement cameraManager;
    public Inventory_Player inventory;

    [Header("Gun State")]
    public bool isPickedUp;
    public bool windowMode;
    public bool coolingDown;
    public int currentMagAmount;
}
