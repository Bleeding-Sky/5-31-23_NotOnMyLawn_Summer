using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    public int strength = 1;
    public int coolDown = 1;
    public float windUp = .25f;
    public float moveSpeed = 1;
    public float attackArea = .5f;
    public LayerMask Player;

    //vars for speed switching (due to zombie state changes)
    [SerializeField] float normalSpeed = 1;
    [SerializeField] float stunSpeed = 0;
    [SerializeField] float stumbleSpeed = .5f;
    [SerializeField] float fallenMoveSpeed = 0;
    [SerializeField] float enragedSpeed = 2.5f;

    [Header("DEBUG")]
    public PositionTracker_Player playerPosition;
    public Status_Zombie zombieStates;
    public Transform attackPoint;
    public Vector3 zombiePosition;
    public Collider2D[] players;
    public bool recharging;
    public bool playerInRange;

    public float scaleFloat;

    public Status_Zombie statusScript;

    private void Start()
    {
        scaleFloat = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //set speed before moving
        ChangeSpeedBasedOnStatus();

        zombiePosition = transform.position;
        DetectPlayer();
        DetermineState();

        if (zombieStates.isChasing && !recharging)
        {
            ChasingPlayer();
            FacingDirection();
        }
        else if (zombieStates.isAttacking && !recharging)
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
            transform.localScale = new Vector3(scaleFloat, transform.localScale.y, transform.localScale.z);
        }
        else if (playerDirection <= 0)
        {
            transform.localScale = new Vector3(-scaleFloat, transform.localScale.y, transform.localScale.z);
        }
    }

    /// <summary>
    /// Chases the player through zombie speed and player position
    /// </summary>
    private void ChasingPlayer()
    {
        zombieStates.DoChase();
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.playerPosition, moveSpeed * Time.deltaTime);
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
        yield return new WaitForSeconds(windUp);
        StartCoroutine(Attack(player));
    }

    /// <summary>
    /// Determines what the zombie is currently doing
    /// </summary>
    public void DetermineState()
    {
        
        if (!playerInRange && !recharging)
        {
            zombieStates.DoChase();
            zombieStates.StopAttack();
        }
        else if (playerInRange)
        {
            zombieStates.DoAttack();
            zombieStates.StopChase();
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

    /// <summary>
    /// reads the current state of the zombie and changes speed to appropriate variable
    /// </summary>
    private void ChangeSpeedBasedOnStatus()
    {
        float currentSpeed = 0;
        switch (statusScript.standingState)
        {
            case ZmbStandingStateEnum.NoStatus:
                currentSpeed = normalSpeed;
                break;

            case ZmbStandingStateEnum.Stunned:
                currentSpeed = stunSpeed;
                break;

            case ZmbStandingStateEnum.Stumbling:
                currentSpeed = stumbleSpeed;
                break;

            case ZmbStandingStateEnum.FallenForward:
                currentSpeed = fallenMoveSpeed;
                break;

            case ZmbStandingStateEnum.FallenBackward:
                currentSpeed = fallenMoveSpeed;
                break;

            case ZmbStandingStateEnum.Enraged:
                currentSpeed = enragedSpeed;
                break;
        }

        moveSpeed = currentSpeed;
    }

}
