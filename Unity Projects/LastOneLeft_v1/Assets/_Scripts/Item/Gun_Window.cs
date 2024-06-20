using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Gun_Window : MonoBehaviour
{
    public GunInformation_Item gunInfo;
    public Vector3 worldPos;
    public Vector3 startingPosition;
    public float anglePhi;
    public float angleTheta;
    public GameObject bullet;
    public bool canFire;
    public bool canReload;
    public bool shooting;
    // Start is called before the first frame update
    void Start()
    {
        gunInfo = GetComponent<GunInformation_Item>();
        canFire = true;
        bullet = gunInfo.windowBullet;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunInfo.isPickedUp)
        {
            gunInfo.windowMode = gunInfo.playerStates.lookingThroughWindow;
            gunInfo.currentCamera = gunInfo.cameraManager.currentCamera;
            CheckIfFireable();

            if (shooting && canFire && gunInfo.windowMode)
            {
                CalculateShootingAngle();
                Shoot();
                StartCoroutine(DetermineFireRate());

                if(gunInfo.semiAutomatic)
                {
                    shooting = false;
                }
            }
        }
    }

    public void Shoot()
    {
        canFire = false;
        float phiAngleDifference = gunInfo.shotSpread / 2;
        float phiAngle1 = anglePhi - phiAngleDifference;
        float phiAngle2 = anglePhi + phiAngleDifference;
        float phiDecidedAngle;

        float thetaAngleDifference = gunInfo.shotSpread / 2;
        float thetaAngle1 = angleTheta - thetaAngleDifference;
        float thetaAngle2 = angleTheta + thetaAngleDifference;
        float thetaDecidedAngle;
        for (int i = 0; i < gunInfo.bulletsPerShot; i++)
        {
            phiDecidedAngle = UnityEngine.Random.Range(phiAngle1, phiAngle2);
            thetaDecidedAngle = UnityEngine.Random.Range(thetaAngle1, thetaAngle2);

            double zDirection = Math.Cos((Math.PI / 180) * phiDecidedAngle) *
                                Math.Sin((Math.PI / 180) * thetaDecidedAngle);
            double yDirection = Math.Sin((Math.PI / 180) * phiDecidedAngle);
            double xDirection = Math.Cos((Math.PI / 180) * thetaDecidedAngle);

            Bullet_Window bulletDirection = bullet.GetComponent<Bullet_Window>();
            BulletData(bullet);
            bulletDirection.startingPosition = gunInfo.currentCamera.transform;

            bulletDirection.bulletDirection = new Vector3((float)xDirection, (float)yDirection, (float)zDirection);

            //Creates bullet and updates the amount
            Instantiate(bullet, worldPos, Quaternion.identity);
        }

        gunInfo.currentMagAmount -= 1;
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


    IEnumerator DetermineFireRate()
    {   
        gunInfo.coolingDown = true;
        canReload = false;
        yield return new WaitForSeconds(gunInfo.fireRate);
        gunInfo.coolingDown = false;
        canFire = true;
        canReload = true;
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
        if (totalBullets > gunInfo.magSize)
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
        else if (totalBullets <= gunInfo.magSize && totalBullets > 0)
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
        else if (gunInfo.currentMagAmount <= 0)
        {
            canFire = false;
        }

        if (gunInfo.currentMagAmount == gunInfo.magSize)
        {
            canReload = false;
        }

    }



    public void CalculateShootingAngle()
    {
        MousePosition();
        startingPosition = gunInfo.currentCamera.transform.position;
        Vector3 direction = worldPos - startingPosition;

        anglePhi = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
        angleTheta = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
    }
    public void MousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitLocation;
        if (Physics.Raycast(ray, out hitLocation, 1000))
        {
            worldPos = hitLocation.point;
        }
    }

    public void ShootingAction(InputAction.CallbackContext actionContext)
    {
        if (gunInfo.semiAutomatic && gunInfo.windowMode && gunInfo.isPickedUp)
        {
            if (actionContext.started && canFire)
            {
                shooting = true;
            }
        }
        else if (gunInfo.automatic && gunInfo.windowMode && gunInfo.isPickedUp)
        {
            if (actionContext.performed && canFire)
            {
                shooting = true;
            }
            else if (actionContext.canceled)
            {
                shooting = false;
                Debug.Log("stopped shooting");
            }
        }
    }

    public void ReloadAction(InputAction.CallbackContext actionContext)
    {
        if (actionContext.started && gunInfo.windowMode && canReload && gunInfo.isPickedUp)
        {
            StartCoroutine(Reload());

            Debug.Log("Reload");
        }
    }
}
