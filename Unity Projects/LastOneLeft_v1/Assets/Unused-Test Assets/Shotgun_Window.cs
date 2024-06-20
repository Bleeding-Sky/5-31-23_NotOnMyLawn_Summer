using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Window : MonoBehaviour
{
    public GunInformation_Item gunSpecs;
    public Vector3 worldPos;
    public Vector3 startingPosition;
    public float anglePhi;
    public float angleTheta;
    public float spread;
    public GameObject bullet;
    public bool canFire;
    // Start is called before the first frame update
    void Start()
    {
        gunSpecs = GetComponent<GunInformation_Item>();
        canFire = true;
        bullet = gunSpecs.windowBullet;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunSpecs.isPickedUp)
        {
            gunSpecs.windowMode = gunSpecs.playerStates.lookingThroughWindow;
            gunSpecs.currentCamera = gunSpecs.cameraManager.currentCamera;
            if (Input.GetKeyDown(KeyCode.Mouse0) && canFire && gunSpecs.windowMode)
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
        float phiAngleDifference = spread / 2;
        float phiAngle1 = anglePhi - phiAngleDifference;
        float phiAngle2 = anglePhi + phiAngleDifference;
        float phiDecidedAngle;

        float thetaAngleDifference = spread / 2;
        float thetaAngle1 = angleTheta - thetaAngleDifference;
        float thetaAngle2 = angleTheta + thetaAngleDifference;
        float thetaDecidedAngle;
        for (int i = 0; i < 10; i++)
        {
            phiDecidedAngle = UnityEngine.Random.Range(phiAngle1, phiAngle2);
            thetaDecidedAngle = UnityEngine.Random.Range(thetaAngle1, thetaAngle2);

            Debug.Log($"Phi: {phiDecidedAngle} \n Theta: {thetaDecidedAngle}");
            double zDirection = Math.Cos((Math.PI / 180) * phiDecidedAngle) * 
                Math.Sin((Math.PI / 180) * thetaDecidedAngle);
            double yDirection = Math.Sin((Math.PI / 180) * phiDecidedAngle);
            double xDirection = Math.Cos((Math.PI / 180) * thetaDecidedAngle);

            Bullet_Window bulletDirection = bullet.GetComponent<Bullet_Window>();
            bulletDirection.startingPosition = gunSpecs.currentCamera.transform;

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
        startingPosition = gunSpecs.currentCamera.transform.position;
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
