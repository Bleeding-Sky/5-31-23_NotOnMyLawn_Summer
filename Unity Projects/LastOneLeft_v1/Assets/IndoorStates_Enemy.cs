using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorStates_Enemy : MonoBehaviour
{
    [Header("STATE HANDLER")]
    public Collider2D[] players;
    public enum EnemyStates { Idle, Attack, Chase, Tracking, Grappling};
    public EnemyStates enemyState;

    [Header("SCRIPTS")]
    public Behavior_Zombie behaviorScript;
    public RoomTracking_Zombie tracker;

    [Header("PLAYER STATES")]
    public LayerMask Player;
    public bool playerInRange;
    public bool playerInRoom;
    public bool grapplingPlayer;
    public bool preAttackChasing;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyStates.Idle;
        behaviorScript = GetComponent<Behavior_Zombie>();
        tracker = GetComponent<RoomTracking_Zombie>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRoom = !tracker.findPlayer;
        DetectPlayer();
        DetermineState();

        if(preAttackChasing)
        {
            behaviorScript.PreAttackChasingPlayer();
        }
    }

    #region State Handler
    /// <summary>
    /// Determines what the zombie is currently doing
    /// </summary>
    public void DetermineState()
    {

        if (!playerInRange && !behaviorScript.recharging && playerInRoom)
        {
            enemyState = EnemyStates.Chase;
        }
        else if (!playerInRange && !behaviorScript.recharging && !playerInRoom)
        {
            enemyState = EnemyStates.Tracking;
        }
        else if (playerInRange && playerInRoom && !grapplingPlayer)
        {
            enemyState = EnemyStates.Attack;
        }
        else if (playerInRange && playerInRoom && grapplingPlayer)
        {
            enemyState = EnemyStates.Grappling;
        }
    }

    /// <summary>
    /// Detects if the player is in the attack range of the player
    /// </summary>
    public void DetectPlayer()
    {
        players = Physics2D.OverlapCircleAll(behaviorScript.attackPoint.transform.position, behaviorScript.attackArea, Player);
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
    #endregion


}