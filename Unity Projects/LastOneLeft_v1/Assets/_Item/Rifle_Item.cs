using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle_Item : MonoBehaviour
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
        canFire = true;
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetGunObjects();
        if (pickedUp)
        {
            CalculateDirection();
            CalculateAngles();
            CheckIfFireable();
            if (Input.GetKey(KeyCode.Mouse0) && canFire)
            {
                Shoot();
                StartCoroutine(DetermineFireRate());
            }
        }
    }
    /// <summary>
    /// Calculates the angle that the bullet will take and that the cone will move in
    /// </summary>
    public void CalculateAngles()
    {
        //Determines the direction the arm will follow the mouse 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - armPosition;
        rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        coneDirection = -(rotZ - 90);
    }


    public void Shoot()
    {
        canFire = false;
        rand = new System.Random();
        float angleDifference = shotSpread / 2;
        float angle1 = rotZ - angleDifference;
        float angle2 = rotZ + angleDifference;
        float decidedAngle;

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

    /// <summary>
    /// Transforms the gun object's rotation to face the mouse and arm
    /// </summary>
    public void FaceMouse()
    {
        ArmRotation_Player zRotation = GunRotation.GetComponent<ArmRotation_Player>();
        float rotZ = zRotation.itemRotation;
        transform.localRotation = Quaternion.Euler(0, 0, rotZ - 90);
    }

    /// <summary>
    /// Calculates the direection and poition of the the hand object
    /// </summary>
    public void CalculateDirection()
    {
        transform.position = handPosition.position;
        armPosition = GunRotation.transform.position;
        FaceMouse();
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
    }

    /// <summary>
    /// Sets the Fire rate of the object
    /// </summary>
    /// <returns></returns>
    IEnumerator DetermineFireRate()
    {
        GunInformation_Item gunSpecs = GetComponent<GunInformation_Item>();
        gunSpecs.coolingDown = true;
        yield return new WaitForSeconds(firingRate);
        gunSpecs.coolingDown = false;
        canFire = true;
    }

    public void CheckIfFireable()
    {
        GunInformation_Item gunSpecs = GetComponent<GunInformation_Item>();
        if (gunSpecs.coolingDown != true && bulletAmount > 0)
        {
            canFire = true;
        }
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
