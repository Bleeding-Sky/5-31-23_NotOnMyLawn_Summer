using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorZombie_Movement : MonoBehaviour
{
    public Player_PositionTracker playerPosition;
    public IndoorZombie_States zombieStates;
    public Transform attackPoint;
    public Vector3 zombiePosition;
    public float normalZombieSpeed;
    public float attackArea;
    public LayerMask Player;
    public Collider2D[] players;
    public int strength;
    public int coolDown;
    public float windUp;
    public bool recharging;
    public bool playerInRange;
    // Update is called once per frame
    void Update()
    {
        IndoorZombie_States state = GetComponent<IndoorZombie_States>();
        zombiePosition = transform.position;
        DetectPlayer();
        DetermineState();
        
        if(state.isChasing && !recharging)
        {
            ChasingPlayer();
            FacingDirection();
        }
        else if(state.isAttacking && !recharging)
        {
            InitiateAttack();       
        }
    }

    private void FacingDirection()
    {
        float playerDirection = playerPosition.playerPosition.x - zombiePosition.x;
        if(playerDirection > 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if(playerDirection <= 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void ChasingPlayer()
    {
        zombieStates.isChasing = true;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.playerPosition, normalZombieSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.transform.position, attackArea);
    }
    public void InitiateAttack()
    {
        foreach(Collider2D player in players)
        {
            StartCoroutine(WindUp(player));    
        }
    }

    public IEnumerator Attack(Collider2D player)
    {
        if (playerInRange == true)
        {
            Player_Health playerHealth = player.GetComponent<Player_Health>();
            playerHealth.DamagePlayer(strength);
            Debug.Log("attacking");

        }
        yield return new WaitForSeconds(coolDown);
        recharging = false;  
    }

    public IEnumerator WindUp(Collider2D player)
    {
        recharging = true;
        Debug.Log("winding up");
        yield return new WaitForSeconds(windUp);
        StartCoroutine(Attack(player));
        Debug.Log("wound up");

    }

    public void DetermineState()
    {
        IndoorZombie_States state = GetComponent<IndoorZombie_States>();
        if (!playerInRange && !recharging)
        {
            state.isChasing = true;
            state.isAttacking = false;
        }
        else if (playerInRange)
        {
            state.isAttacking = true;
            state.isChasing = false;
        }
    }

    private void FindPlayer()
    {

    }

    public void DetectPlayer()
    {
        players = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackArea, Player);
        int count = 0; 
        foreach (Collider2D player in players)
        {
            count++;
        }
        if(count > 0)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    
}
