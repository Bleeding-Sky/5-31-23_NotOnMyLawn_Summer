using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gun_Item : MonoBehaviour
{
    public System.Random rand;
    public float rotZ;
    public float coneDirection;
    public double xDirection;
    public double yDirection;

    [Header("CONFIG")]
    public Transform firingPoint;
    GunInformation_Item gunInfo;
    public GameObject bullet;

    [Header("DEBUG")]
    public bool canFire;
    public Vector3 armPosition;
    
    

    // Start is called before the first frame update
    void Start()
    {
        gunInfo = GetComponent<GunInformation_Item>();
        gunInfo.isPickedUp = false;
        bullet = gunInfo.bulletType;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gunInfo.isPickedUp)
        {
            gunInfo.windowMode = gunInfo.playerStates.lookingThroughWindow;
            gunInfo.currentCamera = gunInfo.cameraManager.currentCamera;
            CalculateDirection();
            CalculateAngles();
            CheckIfFireable();
            if (Input.GetKeyDown(KeyCode.Mouse0) && canFire && !gunInfo.windowMode)
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
        float angleDifference = gunInfo.shotSpread / 2;
        float angle1 = rotZ - angleDifference;
        float angle2 = rotZ + angleDifference;
        float decidedAngle;

        for (int i = 0; i < gunInfo.bulletsPerShot; i++)
        {
            decidedAngle = rand.Next((int)angle1, (int)angle2);
            xDirection = Math.Cos((Math.PI / 180) * decidedAngle);
            yDirection = Math.Sin((Math.PI / 180) * decidedAngle);
            Bullet_Item bulletDirection = bullet.GetComponent<Bullet_Item>();

            bulletDirection.xDirection = (float)xDirection;
            bulletDirection.yDirection = (float)yDirection;

            //Creates bullet and updates the amount
            Instantiate(bullet, firingPoint.transform.position, Quaternion.identity);
            gunInfo.bulletCount -= 1;
        }

    }

    /// <summary>
    /// Transforms the gun object's rotation to face the mouse and arm
    /// </summary>
    public void FaceMouse()
    {
        ArmRotation_Player zRotation = gunInfo.rotationAndAimingPoint.GetComponent<ArmRotation_Player>();
        float rotZ = zRotation.itemRotation;
        transform.localRotation = Quaternion.Euler(0, 0, rotZ - 90);
    }

    /// <summary>
    /// Calculates the direection and poition of the the hand object
    /// </summary>
    public void CalculateDirection()
    {
        transform.position = gunInfo.handPos.position;
        armPosition = gunInfo.rotationAndAimingPoint.transform.position;
        FaceMouse();
    }



    /// <summary>
    /// Sets the Fire rate of the object
    /// </summary>
    /// <returns></returns>
    IEnumerator DetermineFireRate()
    {
        gunInfo.coolingDown = true;
        yield return new WaitForSeconds(gunInfo.fireRate);
        gunInfo.coolingDown = false;
        canFire = true;
    }

    /// <summary>
    /// Checks if the gun can be fired through the cooldown and the bullet amount
    /// </summary>
    public void CheckIfFireable()
    {
        if (gunInfo.coolingDown != true && gunInfo.bulletCount > 0)
        {
            canFire = true;
        }
    }

    /// <summary>
    /// gets the angles of the cone from the original angle
    /// </summary>
    /// <param name="angleInDegrees"></param>
    /// <param name="angleIsGlobal"></param>
    /// <returns></returns>
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += firingPoint.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
