using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamageScript : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    public float attackTimer;
    public bool startTimer;
    // Start is called before the first frame update
    void Start()
    {
        attackTimer = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer == true)
        {
            attackTimer = attackTimer - Time.deltaTime;
            if(attackTimer <= 0)
            {
                startTimer = false;
                attackTimer = 1;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && attackTimer == 1)
        {
            PlayerHealth.health -= 10;
            startTimer = true;

        }
        
    }

    private IEnumerator PlayerDamage()
    {
        PlayerHealth.health -= 10;
        yield return new WaitForSeconds(1);
        
    }
}
