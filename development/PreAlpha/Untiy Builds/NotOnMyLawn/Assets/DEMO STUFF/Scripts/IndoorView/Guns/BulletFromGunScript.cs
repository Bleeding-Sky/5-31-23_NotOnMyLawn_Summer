using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFromGunScript : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePos;
    private Rigidbody2D bulletRb;
    public float speed;

    public PointTracker points;
    public Vector3 normalizedDirection;

    public GameObject blood;
    public Vector3 bloodTransform;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bulletRb = GetComponent<Rigidbody2D>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        normalizedDirection = direction.normalized;
        Vector3 rotation = transform.position - mousePos;
        bulletRb.velocity = new Vector3(direction.x, direction.y).normalized * speed;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bloodTransform = transform.position;
        if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Zombie"))
        {
            points.points = points.points + 10;
            Destroy(gameObject);
            
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), GetComponent<Collider2D>());
        }
    }
}