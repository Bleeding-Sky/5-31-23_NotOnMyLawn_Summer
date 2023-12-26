using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Zombies : MonoBehaviour
{
    [Header("CONFIG")]
    public int strength;
    public int coolDown;
    public float windUp;
    public float normalZombieSpeed;
    public float attackArea;
    public LayerMask Player;

    [Header("DEBUG")]
    public PositionTracker_Player playerPosition;
    public Status_Zombie zombieStates;
    public Transform attackPoint;
    public Vector3 zombiePosition;
    public Collider2D[] players;
    public bool recharging;
    public bool playerInRange;

    // Update is called once per frame
    void Update()
    {
        Status_Zombie state = GetComponent<Status_Zombie>();
        zombiePosition = transform.position;
        DetectPlayer();
        DetermineState();

        if (state.isChasing && !recharging)
        {
            ChasingPlayer();
            FacingDirection();
        }
        else if (state.isAttacking && !recharging)
        {
            InitiateAttack();
        }
    }

    /// <summary>
    /// Chooses the direction is facing based on the player's relative position to the zombie
    /// </summary>
    private void FacingDirection()
    {
        float playerDirection = playerPosition.playerPosition.x - zombiePosition.x;
        if (playerDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (playerDirection <= 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    /// <summary>
    /// Chases the player through zombie speed and player position
    /// </summary>
    private void ChasingPlayer()
    {
        zombieStates.DoChase();
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.playerPosition, normalZombieSpeed * Time.deltaTime);
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
    /// Starts the Attack 
    /// </summary>
    public void InitiateAttack()
    {
        foreach (Collider2D player in players)
        {
            StartCoroutine(WindUp(player));
        }
    }

    /// <summary>
    /// Attacks once the wind up is over
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public IEnumerator Attack(Collider2D player)
    {
        if (playerInRange == true)
        {
            Debug.Log("attacking");

        }
        yield return new WaitForSeconds(coolDown);
        recharging = false;
    }

    /// <summary>
    /// Winds up for an attack
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public IEnumerator WindUp(Collider2D player)
    {
        recharging = true;
        Debug.Log("winding up");
        yield return new WaitForSeconds(windUp);
        StartCoroutine(Attack(player));
        Debug.Log("wound up");

    }

    /// <summary>
    /// Determines what the zombie is currently doing
    /// </summary>
    public void DetermineState()
    {
        Status_Zombie state = GetComponent<Status_Zombie>();
        if (!playerInRange && !recharging)
        {
            state.DoChase();
            state.StopAttack();
        }
        else if (playerInRange)
        {
            state.DoAttack();
            state.StopChase();
        }
    }

    /// <summary>
    /// Detects if the player is in the attack range of the player
    /// </summary>
    public void DetectPlayer()
    {
        players = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackArea, Player);
        int count = 0;
        foreach (Collider2D player in players)
        {
            count++;
        }
        if (count > 0)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

}
