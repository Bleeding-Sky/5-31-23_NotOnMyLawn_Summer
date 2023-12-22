using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ZmbIndoor_BodyshotDetector : MonoBehaviour
{
    public Zmb_StatusManager zmbStatusScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //call headshot method on master w/ bullet bodyshot dmg value
            float bulletBodyDmg = collision.gameObject.GetComponent<bulletData>().bodyDmg;
            zmbStatusScript.Bodyshot(bulletBodyDmg);
        }
    }
}
