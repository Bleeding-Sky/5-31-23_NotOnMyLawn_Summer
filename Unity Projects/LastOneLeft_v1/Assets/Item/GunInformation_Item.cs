using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInformation_Item : MonoBehaviour
{
    //Gun objects will take the information from this script and apply it to the gun that its attached to
    [Header("Gun Stats")]
    public int bulletsPerShot;
    public int bulletCount;
    public int magSize;
    public float fireRate;
    public float reload;
    public GameObject bulletType;
    public GameObject windowBullet;
    [Range(0, 360)]
    public float shotSpread;

    [Header("Gun Configurations")]
    public Transform rotationAndAimingPoint;
    public Transform handPos;
    public GameObject player;
    public GameObject currentCamera;
    public States_Player playerStates;
    public CameraManagement cameraManager;

    [Header("Gun State")]
    public bool isPickedUp;
    public bool windowMode;
    public bool coolingDown;
    public int currentMagSize;
}
