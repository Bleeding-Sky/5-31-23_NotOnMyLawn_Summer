using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    public float bulletAmount;
    public float shotDelay;
    public bool shot;

    GunScript Gun;
    // Start is called before the first frame update
    void Start()
    {
        Gun = GetComponent<GunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        if (bulletAmount != 0 && shot && Input.GetKey(KeyCode.Mouse1))
        {
            bulletAmount = bulletAmount - 1;
            Gun.canShoot = true;
        }
        else if(bulletAmount == 0)
        {
            Gun.canShoot = false;
        }
        shot = false;
        ShootDelay();
    }

    private void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            shot = true;
        }
    }
    IEnumerator ShootDelay()
    {

        yield return new WaitForSeconds(shotDelay);
        shot = true;
    }
}
