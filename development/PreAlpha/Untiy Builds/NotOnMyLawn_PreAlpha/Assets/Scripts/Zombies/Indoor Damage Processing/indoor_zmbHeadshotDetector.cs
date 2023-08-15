using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class indoor_zmbHeadshotDetector : MonoBehaviour
{
    public Zmb_Master zmbMasterScript;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //call headshot method on master w/ bullet headshot dmg value
            float bulletHeadDmg = collision.gameObject.GetComponent<bulletData>().headDmg;
            zmbMasterScript.headshot(bulletHeadDmg);
        }
    }
}
