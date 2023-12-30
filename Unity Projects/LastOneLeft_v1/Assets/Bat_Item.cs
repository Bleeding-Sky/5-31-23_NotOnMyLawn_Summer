using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Item : MonoBehaviour
{
    [Header("DEBUG")]
    public float attackArea;
    public Transform attackPoint;
    public Collider2D[] enemies;
    public LayerMask Enemy;
    public EquippedMelee_Item bat;

    // Start is called before the first frame update
    void Start()
    {
        bat = GetComponent<EquippedMelee_Item>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesInRange();
        if (bat.pickedUp && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    public void Attack()
    {
        Debug.Log("Attack");
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

    public void EnemiesInRange()
    {
        enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackArea, Enemy);
        int count = 0;
        foreach (Collider2D player in enemies)
        {
            if(count < 1)
            {
                Debug.Log("Enemy in Range");
            }
            count++;
        }

        
    }
}
