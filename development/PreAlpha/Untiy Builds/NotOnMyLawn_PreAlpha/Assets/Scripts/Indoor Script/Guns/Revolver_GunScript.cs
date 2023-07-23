using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver_GunScript : MonoBehaviour
{
    [Header("CONFIG")]
    public Transform firingPoint;
    public GameObject bullet;
    public Transform GunRotation;
    public Transform handPosition;
    public GameObject player;
    public GameObject aimingPoint;

    [Header("DEBUG")]
    private int bulletAmount;
    private float firingRate;
    private float recoil;
    public bool canFire;
    public Vector3 armPosition;
    public bool pickedUp;

    // Start is called before the first frame update
    void Start()
    {
        Gun_Information gunSpecs = GetComponent<Gun_Information>();
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

            if (Input.GetKeyDown(KeyCode.Mouse0) && canFire)
            {
                Shoot();
                StartCoroutine(DetermineFireRate());
            }
        }
    }

    public void Shoot()
    {
        canFire = false;
        Bullet_Script bulletDirection = bullet.GetComponent<Bullet_Script>();
        Gun_Aiming aiming = aimingPoint.GetComponent<Gun_Aiming>();
        bulletDirection.firingPos = aimingPoint.transform.position;
        bulletDirection.bulletDirectionPosition = armPosition;
        Instantiate(bullet, firingPoint.transform.position, Quaternion.identity);
        bulletAmount -= 1;
    }

    public void FaceMouse()
    {
        Arm_Rotation zRotation = GunRotation.GetComponent<Arm_Rotation>();
        float rotZ = zRotation.itemRotation; 
        transform.localRotation = Quaternion.Euler(0,0,rotZ-90);
    }

    public void CalculateDirection()
    {
        transform.position = handPosition.position;
        armPosition = GunRotation.transform.position;
        FaceMouse();
    }

    IEnumerator DetermineFireRate()
    {
        yield return new WaitForSeconds(firingRate);
        canFire = true;
    }

    public void Recoil()
    {
        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
        playerRB.AddForce(transform.right * recoil);
    }
    
    public void SetGunObjects()
    {
        Gun_Information gunSpecs = GetComponent<Gun_Information>();
        GunRotation = gunSpecs.rotationAndAimingPoint;
        handPosition = gunSpecs.handPos;
        player = gunSpecs.player;
        pickedUp = gunSpecs.isPickedUp;
        aimingPoint = gunSpecs.AimingField;
    }
}
