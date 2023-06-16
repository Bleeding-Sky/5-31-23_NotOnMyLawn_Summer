using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public bool isHolstered;
    public bool canShoot;

    public Transform directonalAiming;
    public float bulletDir;

    public GameObject bullet;
    public Transform bulletTransform;

    private Camera mainCamera;
    private Vector3 mousePos;

    public PlayerBulletCount bulletAmount;
    public float bulletCount;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bulletAmount.bulletCount = bulletCount;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isHolstered)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            bulletDir = calculateDirection();
            transform.rotation = Quaternion.Euler(0, 0, bulletDir - 90);
            
        }

        transform.position = directonalAiming.position;
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isHolstered)
        {
            isHolstered = true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)&&isHolstered)
        {
            isHolstered = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }



        if (Input.GetKeyDown(KeyCode.Mouse0)  && isHolstered && canShoot)
        {
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            bulletAmount.bulletCount = bulletAmount.bulletCount - 1;
        }

        if(bulletAmount.bulletCount != 0)
        {
            canShoot = true;
        }
        else if(bulletAmount.bulletCount == 0)
        {
            canShoot = false;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            bulletAmount.ResetBulletCount();
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }

        }
    }
    float calculateDirection()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        return rotZ;
    }
}
