using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shove_Player : MonoBehaviour
{
    [SerializeField] private float attackArea;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private Collider2D[] enemiesInRange;
    [SerializeField] private Collider2D closestEnemy;
    [SerializeField] private bool shoving;
    [SerializeField] private bool coolingDown;


    // Update is called once per frame
    void Update()
    {
        DetectEnemies();
    }

    public void DetectEnemies()
    {
        enemiesInRange = Physics2D.OverlapCircleAll(attackPosition.position, attackArea, EnemyLayer);
    }

    /// <summary>
    /// Draws the attack area so that it is visible
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if(attackPosition == null)
            return;

        Gizmos.DrawWireSphere(attackPosition.transform.position, attackArea);
    }

    public void ShoveEnemies()
    {
        enemiesInRange = Physics2D.OverlapCircleAll(attackPosition.position, attackArea, EnemyLayer);
        closestEnemy = null;
        float lastClosestDistance = Mathf.Infinity;
        foreach(var enemy in enemiesInRange)
        {
            float distanceFromEnemy = Mathf.Abs(transform.position.x - enemy.transform.position.x);
            Shove_Enemy enemyShove = enemy.GetComponent<Shove_Enemy>();

            if(distanceFromEnemy < lastClosestDistance && enemyShove != null)
            {
                closestEnemy = enemy;
                lastClosestDistance = distanceFromEnemy;
            }
        }

        if(closestEnemy != null)
        {
            Shove(closestEnemy);
            StartCoroutine(ShoveCoolDown());
        }
    }

    public void Shove(Collider2D enemy)
    {
        Shove_Enemy enemyShove = enemy.GetComponent<Shove_Enemy>();
        enemyShove.shoved = true;
    }

    public void ShoveAction(InputAction.CallbackContext actionContext)
    {
        if(actionContext.started && !coolingDown)
        {
            ShoveEnemies();
            
        }
    }
    public IEnumerator ShoveCoolDown()
    {
        coolingDown = true;
        Debug.Log("Shoving");
        yield return new WaitForSeconds(2f);
        coolingDown = false;
    }
}
