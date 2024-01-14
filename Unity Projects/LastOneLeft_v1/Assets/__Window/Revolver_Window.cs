using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver_Window : MonoBehaviour
{
    [Header("CONFIG")]
    public GunInformation_Item gunSpecs;
    public GameObject bullet;
    [Header("DEBUG")]
    public bool canFire;
    public bool pickedUp;
    public Vector3 worldPos;
    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
        pickedUp = false;
        bullet = gunSpecs.windowBullet;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpecs();
        if (pickedUp)
        {
            gunSpecs.windowMode = gunSpecs.playerStates.lookingThroughWindow;
            gunSpecs.currentCamera = gunSpecs.cameraManager.currentCamera;
            if (Input.GetKeyDown(KeyCode.Mouse0) && canFire && gunSpecs.windowMode)
            {
                MousePosition();
                Shoot();
                StartCoroutine(DetermineFireRate());
            }
        }
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
    private void Shoot()
    {
        //Disables the firing for the gun
        canFire = false;
        Bullet_Window bulletDirection = bullet.GetComponent<Bullet_Window>();
        bulletDirection.startingPosition = gunSpecs.currentCamera.transform;
        //Gun_Aiming aiming = aimingPoint.GetComponent<Gun_Aiming>();

        //Sets the bullets trajectory with the direction  
        bulletDirection.mousePos = worldPos;
        //Creates bullet and updates the amount
        Instantiate(bullet, worldPos, Quaternion.identity);
        gunSpecs.bulletCount -= 1;
    }

    IEnumerator DetermineFireRate()
    {
        GunInformation_Item gunSpecs = GetComponent<GunInformation_Item>();
        gunSpecs.coolingDown = true;
        yield return new WaitForSeconds(gunSpecs.fireRate);
        gunSpecs.coolingDown = false;
        canFire = true;
    }

    public void UpdateSpecs()
    {
        pickedUp = gunSpecs.isPickedUp;
    }
}
