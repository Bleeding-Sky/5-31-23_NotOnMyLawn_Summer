using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Information : MonoBehaviour
{
    [Header("Gun Stats")]
    public int bulletCount;
    public float fireRate;
    public float recoil;
    public GameObject bulletType;

    [Header("Gun Configurations")]
    public GameObject rotationAndAimingPoint;
}
