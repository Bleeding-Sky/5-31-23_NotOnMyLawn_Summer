using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver_GunScript : MonoBehaviour
{
    [Header("CONFIG")]
    public Transform firingPoint;
    public GameObject bullet;
    public Transform GunLocation;
    public Transform GunRotation;
    public Transform handPosition;
    public GameObject player;

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
        bulletAmount = gunSpecs.bulletCount;
        firingRate = gunSpecs.fireRate;
        recoil = gunSpecs.recoil;
        canFire = true;
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pickedUp)
        {
            Rigidbody2D GunRigidBody = GetComponent<Rigidbody2D>();
            CalculateDirection();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && canFire)
        {
            Shoot();
            StartCoroutine(DetermineFireRate());
        }
    }

    public void Shoot()
    {
        canFire = false;
        Bullet_Script bulletDirection = bullet.GetComponent<Bullet_Script>();
        bulletDirection.bulletDirectionPosition = armPosition;
        Instantiate(bullet, firingPoint.transform.position, Quaternion.identity);
        bulletAmount -= 1;
    }

    public void FaceMouse(float RotationZ)
    {
        GunLocation.localRotation = GunRotation.localRotation;
    }

    public void CalculateDirection()
    {
        GunLocation.transform.position = handPosition.position;

        armPosition = GunRotation.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        FaceMouse(rotZ);
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
    
}
