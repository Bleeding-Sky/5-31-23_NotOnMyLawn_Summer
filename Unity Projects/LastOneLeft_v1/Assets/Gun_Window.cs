using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && canFire && gunInfo.windowMode)
            {
                CalculateShootingAngle();
                Shoot();
                StartCoroutine(DetermineFireRate());
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

            Debug.Log($"Phi: {phiDecidedAngle} \n Theta: {thetaDecidedAngle}");
            double zDirection = Math.Cos((Math.PI / 180) * phiDecidedAngle) *
                                Math.Sin((Math.PI / 180) * thetaDecidedAngle);
            double yDirection = Math.Sin((Math.PI / 180) * phiDecidedAngle);
            double xDirection = Math.Cos((Math.PI / 180) * thetaDecidedAngle);

            Bullet_Window bulletDirection = bullet.GetComponent<Bullet_Window>();
            bulletDirection.startingPosition = gunInfo.currentCamera.transform;

            bulletDirection.bulletDirection = new Vector3((float)xDirection, (float)yDirection, (float)zDirection);

            //Creates bullet and updates the amount
            Instantiate(bullet, worldPos, Quaternion.identity);
        }
    }


    IEnumerator DetermineFireRate()
    {
        GunInformation_Item gunSpecs = GetComponent<GunInformation_Item>();
        gunSpecs.coolingDown = true;
        yield return new WaitForSeconds(gunSpecs.fireRate);
        gunSpecs.coolingDown = false;
        canFire = true;
    }
    public void CalculateShootingAngle()
    {
        MousePosition();
        startingPosition = gunInfo.currentCamera.transform.position;
        Vector3 direction = worldPos - startingPosition;

        Debug.Log(direction.normalized);
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
}