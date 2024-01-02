using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Item : MonoBehaviour
{
    private EquippedMelee_Item bat;
    private Rigidbody2D enemyRB;

    [Header("CONFIG")]
    public float attackArea;
    public float knockback;
    public float damage;

    [Header("DEBUG")]
    public Transform attackPoint;
    public Collider2D[] enemies;
    public LayerMask Enemy;
    public bool enemiesInRange;
    public PositionTracker_Player tracker;
    public Collider2D enemy;

    // Start is called before the first frame update
    void Start()
    {
        bat = GetComponent<EquippedMelee_Item>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesInRange();

        //Updates the local scale of the item as the player is holding it
        if (bat.pickedUp)
        {
            transform.localScale = new Vector3(1, 1 * tracker.playerDirection, 1);
        }

        //Attacks if conditions are met
        if (bat.pickedUp && Input.GetKeyDown(KeyCode.Mouse0) && enemiesInRange)
        {
            Attack();
        }
    }

    /// <summary>
    /// The player attacks and applies the knockback to the enemy
    /// </summary>
    public void Attack()
    {
        Debug.Log("Attack");
        enemyRB.velocity = Vector2.zero;
        enemyRB.velocity = new Vector2(1 * tracker.playerDirection, 1) * knockback;
        Health_Zombie enemyHealth = enemy.GetComponent<Health_Zombie>();
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
        foreach (Collider2D player in enemies)
        {
            //only gets the first item in the array
            if(count < 1)
            {
                Debug.Log("Enemy in Range");
                enemyRB = player.GetComponent<Rigidbody2D>();
                enemiesInRange = true;
                enemy = player;
            }
            count++;
        }

        //If there is no enemy in the field then the enemies in range boolean and the rigidbody is reset
        if(count == 0)
        {
            enemiesInRange = false;
            enemyRB = null;
            enemy = null;
        }
    }
}
