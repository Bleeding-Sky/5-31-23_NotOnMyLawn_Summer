using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Item : MonoBehaviour
{
    public System.Random rand;
    public float rotZ;
    public float shotRadius;
    public float coneDirection;
    public double xDirection;
    public double yDirection;

    [Header("CONFIG")]
    public Transform firingPoint;
    public GameObject bullet;
    public Transform GunRotation;
    public Transform handPosition;
    public GameObject player;
    public GameObject aimingPoint;

    [Header("DEBUG")]
    public int bulletAmount;
    private float firingRate;
    private float recoil;
    public bool canFire;
    public Vector3 armPosition;
    public bool pickedUp;
    [Range(0, 360)]
    public float shotSpread;


    // Start is called before the first frame update
    void Start()
    {
        GunInformation_Item gunSpecs = GetComponent<GunInformation_Item>();
        gunSpecs.isPickedUp = false;
        bulletAmount = gunSpecs.bulletCount;
        firingRate = gunSpecs.fireRate;
        recoil = gunSpecs.recoil;
        canFire = true;
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {

        SetGunObjects();
        //Determines the direction the arm will follow the mouse 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        coneDirection = -(rotZ - 90);
        Shoot();

    }

    public void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            rand = new System.Random();
            float angleDifference = shotSpread / 2;
            float angle1 = rotZ - angleDifference;
            float angle2 = rotZ + angleDifference;
            float decidedAngle;

            for (int i = 0; i < 5; i++)
            {
                decidedAngle = rand.Next((int)angle1, (int)angle2);
                xDirection = Math.Cos((Math.PI / 180) * decidedAngle);
                yDirection = Math.Sin((Math.PI / 180) * decidedAngle);
                ShotgunBullet_Item bulletDirection = bullet.GetComponent<ShotgunBullet_Item>();
                
                bulletDirection.xDirection = (float)xDirection;
                bulletDirection.yDirection = (float)yDirection;
       
                //Creates bullet and updates the amount
                Instantiate(bullet, firingPoint.transform.position, Quaternion.identity);
                bulletAmount -= 1;
            }
            
            
        }
    }

    /// <summary>
    /// Updates the gun objects with it's gun information objects
    /// so that the gun is always oriented correctly
    /// </summary>
    public void SetGunObjects()
    {
        GunInformation_Item gunSpecs = GetComponent<GunInformation_Item>();
        GunRotation = gunSpecs.rotationAndAimingPoint;
        handPosition = gunSpecs.handPos;
        player = gunSpecs.player;
        pickedUp = gunSpecs.isPickedUp;
        aimingPoint = gunSpecs.AimingField;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += firingPoint.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
    
}
