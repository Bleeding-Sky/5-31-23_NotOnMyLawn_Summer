using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver_Item : MonoBehaviour
{
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
        if (pickedUp)
        {
            CalculateDirection();
            CheckIfFireable();

            if (Input.GetKeyDown(KeyCode.Mouse0) && canFire)
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
        bulletDirection.firingPos = circlePos;
        bulletDirection.bulletDirectionPosition = armPosition;

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

    /// <summary>
    /// NOT currently being used but kept in case we wanna incorperate this in any way.
    /// </summary>
    public void Recoil()
    {
        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
        playerRB.AddForce(transform.right * recoil);
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

    /// <summary>
    /// Checks if the gun can be fired through the cooldown and the bullet amount
    /// </summary>
    public void CheckIfFireable()
    {
        GunInformation_Item gunSpecs = GetComponent<GunInformation_Item>();
        if (gunSpecs.coolingDown != true && bulletAmount > 0)
        {
            canFire = true;
        }
    }
}
