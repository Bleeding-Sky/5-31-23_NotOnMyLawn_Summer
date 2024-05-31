using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public bool canReload;
    public Vector3 armPosition;
    public bool shooting;
    

    // Start is called before the first frame update
    void Start()
    {
        gunInfo = GetComponent<GunInformation_Item>();
        gunInfo.isPickedUp = false;
        bullet = gunInfo.indoorbullet;
        gunInfo.currentMagAmount = gunInfo.magSize;
        canFire = true;
        canReload = true;
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
            if(shooting && canFire && !gunInfo.windowMode)
            {
                Shoot();
                StartCoroutine(DetermineFireRate());

                if(gunInfo.semiAutomatic)
                {
                    shooting = false;
                }
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
            BulletData(bullet);

            bulletDirection.xDirection = (float)xDirection;
            bulletDirection.yDirection = (float)yDirection;

            //Creates bullet and updates the amount
            Instantiate(bullet, firingPoint.transform.position, Quaternion.identity);
            gunInfo.currentMagAmount -= 1;
        }

    }

    private void BulletData(GameObject bullet)
    {
        BulletInfo bulletData = bullet.GetComponent<BulletInfo>();

        bulletData.damage = gunInfo.damage;
        bulletData.bulletPenetration = gunInfo.bulletPenetration;
        bulletData.statusMultiplier = gunInfo.statusMultiplier;
        bulletData.critDamageMultiplier = gunInfo.critDamageMultiplier;
        bulletData.armoredDamageMultiplier = gunInfo.armoredDamageMultiplier;
        bulletData.weakDamageMultiplier = gunInfo.weakDamageMultiplier;
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
        canReload = false;
        yield return new WaitForSeconds(gunInfo.fireRate);
        canReload = true;
        gunInfo.coolingDown = false;
        canFire = true;
    }

    public IEnumerator Reload()
    {
        canFire = false;
        canReload = false;
        yield return new WaitForSeconds(gunInfo.reload);
        subtractInventoryAmmo();
        canFire = true;
        canReload = true;
    }

    public void subtractInventoryAmmo()
    {

        int reloadAmount = gunInfo.magSize - gunInfo.currentMagAmount;
        int totalBullets = 0;
        switch (gunInfo.bulletType)
        {
            case AmmoDrop_Item.BulletTypes.Large:
                totalBullets = gunInfo.inventory.largeAmmo;
                break;
            case AmmoDrop_Item.BulletTypes.Medium:
                totalBullets = gunInfo.inventory.mediumAmmo;
                break;
            case AmmoDrop_Item.BulletTypes.Small:
                totalBullets = gunInfo.inventory.smallAmmo;
                break;
        }

        Debug.Log(reloadAmount);
        if(totalBullets > gunInfo.magSize)
        {
            switch (gunInfo.bulletType)
            {
                case AmmoDrop_Item.BulletTypes.Large:
                    gunInfo.inventory.largeAmmo -= reloadAmount;
                    break;
                case AmmoDrop_Item.BulletTypes.Medium:
                    gunInfo.inventory.mediumAmmo -= reloadAmount;
                    break;
                case AmmoDrop_Item.BulletTypes.Small:
                    gunInfo.inventory.smallAmmo -= reloadAmount;
                    break;
            }
            gunInfo.currentMagAmount = gunInfo.magSize;
        }
        else if(totalBullets <= gunInfo.magSize && totalBullets > 0)
        {
            switch (gunInfo.bulletType)
            {
                case AmmoDrop_Item.BulletTypes.Large:
                    gunInfo.inventory.largeAmmo -= totalBullets;
                    break;
                case AmmoDrop_Item.BulletTypes.Medium:
                    gunInfo.inventory.mediumAmmo -= totalBullets;
                    break;
                case AmmoDrop_Item.BulletTypes.Small:
                    gunInfo.inventory.smallAmmo -= totalBullets;
                    break;
            }
            gunInfo.currentMagAmount = totalBullets;
        }
        
    }

    /// <summary>
    /// Checks if the gun can be fired through the cooldown and the bullet amount
    /// </summary>
    public void CheckIfFireable()
    {
        if (gunInfo.coolingDown != true && gunInfo.currentMagAmount > 0)
        {
            canFire = true;
        }
        else if(gunInfo.currentMagAmount <= 0)
        {
            canFire = false;
        }

        if(gunInfo.currentMagAmount == gunInfo.magSize)
        {
            canReload = false;
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

    public void ShootingAction(InputAction.CallbackContext actionContext)
    {
        if (gunInfo.semiAutomatic && !gunInfo.windowMode && canFire)
        {
            if (actionContext.started)
            {
                shooting = true;
            }
        }
        else if (gunInfo.automatic && !gunInfo.windowMode && canFire)
        {
            if (actionContext.performed)
            {
                shooting = true;
                Debug.Log("shooting");
            }
            else if(actionContext.canceled)
            {
                shooting = false;
            }
        }
    }

    public void ReloadAction(InputAction.CallbackContext actionContext)
    {
        if (actionContext.started && !gunInfo.windowMode && canReload)
        {
            StartCoroutine(Reload());
            Debug.Log("Reload");
        }
    }





}
