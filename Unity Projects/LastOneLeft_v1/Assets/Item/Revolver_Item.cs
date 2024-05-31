using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver_Item : MonoBehaviour
{
    [Header("CONFIG")]
    public Transform firingPoint;
    public GameObject bullet;
    
    public GameObject aimingPoint;
    GunInformation_Item gunInfo;

    [Header("DEBUG")]
    public bool canFire;
    public Vector3 armPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        gunInfo = GetComponent<GunInformation_Item>();
        gunInfo.isPickedUp = false;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunInfo.isPickedUp)
        {
            gunInfo.windowMode = gunInfo.playerStates.lookingThroughWindow;
            CalculateDirection();
            CheckIfFireable();

            if (Input.GetKeyDown(KeyCode.Mouse0) && canFire && !gunInfo.windowMode)
            {
                Shoot();
                StartCoroutine(DetermineFireRate());
            }
        }
    }

    /// <summary>
    /// Shoots a bullet from the gun
    /// </summary>
    public void Shoot()
    {
        //Disables the firing for the gun
        canFire = false;
        Bullet_Item bulletDirection = bullet.GetComponent<Bullet_Item>();
        //Gun_Aiming aiming = aimingPoint.GetComponent<Gun_Aiming>();

        //Calculates the direction in which the mouse cursor is facing
        Vector3 circlePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        circlePos.z = 0;

        //Sets the bullets trajectory with the direction  

        Vector2 bulletDir = (circlePos - armPosition).normalized;
        bulletDirection.xDirection = bulletDir.x;
        bulletDirection.yDirection = bulletDir.y;
        Debug.Log(armPosition);
        //Creates bullet and updates the amount
        Instantiate(bullet, firingPoint.transform.position, Quaternion.identity);
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
    /// NOT currently being used but kept in case we wanna incorperate this in any way.
    /// </summary>
    public void Recoil()
    {
        Rigidbody2D playerRB = gunInfo.player.GetComponent<Rigidbody2D>();
        //playerRB.AddForce(transform.right * gunInfo.recoil);
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
    }
}
