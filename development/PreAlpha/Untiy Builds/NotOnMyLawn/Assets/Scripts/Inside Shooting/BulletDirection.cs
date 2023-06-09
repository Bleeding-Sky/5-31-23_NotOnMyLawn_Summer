using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirection : MonoBehaviour
{
    public float bulletDir;

    public GameObject bullet;
    public Transform bulletTransform;

    private Camera mainCamera;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
  
    }

    // Update is called once per frame
    void Update()
    {

        bulletDir = calculateDirection();
        transform.rotation = Quaternion.Euler(0, 0, bulletDir);


    }

    float calculateDirection()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        return rotZ;
    }
}
