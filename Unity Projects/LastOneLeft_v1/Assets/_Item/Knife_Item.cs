using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife_Item : MonoBehaviour
{
    private EquippedMelee_Item knife;
    private Rigidbody2D enemyRB;
   
    [Header("CONFIG")]
    public float attackArea;
    public float knockback;
    public float damage;
    public float weaponCooldown;

    [Header("DEBUG")]
    public Transform attackPoint;
    public Collider2D[] enemies;
    public LayerMask Enemy;
    public bool enemiesInRange;
    public PositionTracker_Player tracker;
    public Collider2D enemyCollider;
    public float maxComboTimer;
    public float comboTimer = 0;
    public int comboNumber;
    public bool timerStarted;
    public bool recharging;

    // Start is called before the first frame update
    void Start()
    {
        knife = GetComponent<EquippedMelee_Item>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesInRange();
        AttackTimer();
       
        //Attacks if conditions are met
        if (knife.pickedUp && enemiesInRange && Input.GetKeyDown(KeyCode.Mouse0) && recharging == false)
        {
            ComboAttack();
        }
    }

    public void AttackTimer()
    {
        if (timerStarted == true)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer >= maxComboTimer || comboNumber == 3)
            {
                Debug.Log("Combo Reset");
                timerStarted = false;
                comboNumber = 0;
                comboTimer = 0;
                StartCoroutine(WeaponCoolDown());
            }
        }
    }
    public void ComboAttack()
    {
        Vector2 knockbackDirection = knife.KnockbackDirection();

        if (comboTimer < maxComboTimer && comboNumber == 0)
        {
            timerStarted = true;
            Attack(knockbackDirection);
            Debug.Log("Attack 1");
            comboNumber += 1;
            comboTimer = 0;
        }
        else if(comboTimer < maxComboTimer && comboNumber == 1)
        {
            Attack(knockbackDirection);
            Debug.Log("Attack 2");
            comboNumber += 1;
            comboTimer = 0;
        }
        else if(comboTimer < maxComboTimer && comboNumber == 2)
        {
            Attack(knockbackDirection);
            Debug.Log("Attack 3");
            comboNumber += 1;
            comboTimer = 0;
        }

        
    }
    public IEnumerator WeaponCoolDown()
    {
        recharging = true;
        yield return new WaitForSeconds(weaponCooldown);
        recharging = false;
    }


    /// <summary>
    /// The player attacks and applies the knockback to the enemy
    /// </summary>
    public void Attack(Vector2 direction)
    {
        Debug.Log("Attack");
        enemyRB.velocity = Vector2.zero;
        enemyRB.velocity = direction * knockback;
        Health_Zombie enemyHealth = enemyCollider.GetComponent<Health_Zombie>();
        enemyHealth.DamageHealth(damage);

    }

    /// <summary>
    /// Draws the attack area so that it is visible
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.transform.position, attackArea);
    }

    /// <summary>
    /// Detects the enemies in the range of the players attack and if they can attack
    /// </summary>
    public void EnemiesInRange()
    {
        enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackArea, Enemy);
        int count = 0;

        //Goes through each of the players in the field and atacks the first enemy in the array
        foreach (Collider2D enemy in enemies)
        {
            //only gets the first item in the array
            if (count < 1)
            {
                Debug.Log("Enemy in Range");
                enemyRB = enemy.GetComponent<Rigidbody2D>();
                enemiesInRange = true;
                enemyCollider = enemy;
            }
            count++;
        }

        //If there is no enemy in the field then the enemies in range boolean and the rigidbody is reset
        if (count == 0)
        {
            enemiesInRange = false;
            enemyRB = null;
            enemyCollider = null;
        }
    }
}
