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
    [SerializeField] float crawlMoveSpeed = 0.5f;

    [Header("DEBUG")]
    public PositionTracker_Player playerPosition;
    public States_Player playerStates;
    public Status_Zombie statusScript;
    public IndoorStates_Enemy stateScript;
    public RoomTracking_Zombie roomTrackingScript;
    public Transform attackPoint;
    public Vector3 zombiePosition;
    public bool recharging;

    private void Start()
    {
        statusScript = GetComponentInParent<Status_Zombie>();
        stateScript = GetComponent<IndoorStates_Enemy>();
        roomTrackingScript = GetComponent<RoomTracking_Zombie>();
    }

    // Update is called once per frame
    void Update()
    {
        //set speed before moving
        UpdateSpeedBasedOnStatus();
        zombiePosition = transform.position;
        DetermineAction();
    }

    #region Enemy Actions
    /// <summary>
    /// Chases the player through zombie speed and player position
    /// </summary>
    private void ChasingPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPosition.playerPosition.x,0,0), moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Chases the player through zombie speed and player position
    /// </summary>
    public void PreAttackChasingPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPosition.playerPosition.x, 0, 0), (moveSpeed * 2f) * Time.deltaTime);
    }

    /// <summary>
    /// Enabled when the player is not in the same room as the enemy
    /// it will take the closest room from the tracking script and 
    /// lead the zombie to the player through the door system
    /// </summary>
    private void TrackingPlayer()
    {
        if (roomTrackingScript.doorGoal != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(roomTrackingScript.doorGoal.transform.position.x, 0, 0), moveSpeed * Time.deltaTime);
        }
    }

    #region Attack Actions
    /// <summary>
    /// Starts the Attack 
    /// </summary>
    public void InitiateAttack()
    {
        //Starts an attack for all of the players in range of the attack
        //Aka just the single player but you never know amirite
        foreach (Collider2D player in stateScript.players)
        {
            StartCoroutine(WindUp(player));
        }
    }

    /// <summary>
    /// Winds up for an attack based on a timer
    /// gives the playter a slight chance to dodge an attack if the player is near
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public IEnumerator WindUp(Collider2D player)
    {
        stateScript.preAttackChasing = true;
        recharging = true;
        yield return new WaitForSeconds(windUp);
        stateScript.preAttackChasing = false;
        //Checks to make sure that the player is not already grappling the player in case
        //the player runs into the zombie before the wind up is done
        if (!stateScript.grapplingPlayer)
        {
            //Used to determine the distance from the player to zombie at the time of the attack
            float distanceFromPlayer = Mathf.Abs(transform.position.x - player.transform.position.x);
            float grappleDistance = attackArea / 1.5f;
            //Detertmine if the enemy will commit to the attack or switch to a grapple
            if (distanceFromPlayer <= grappleDistance)
            {
                stateScript.grapplingPlayer = true;
                recharging = false;
            }
            else
            {
                StartCoroutine(Attack(player));
            }
        }
        else
        {
            //Resets the recharging as the player is not longer
            //looking to attack or grapple as he is already in 
            recharging = false;
        }
            
    }

    /// <summary>
    /// Starts the grapple procees by taking the players collider and processing it to the Grapple method
    /// </summary>
    public void InitiateGrapple()
    {
        //Get the players collider and pass to the grapple method
        foreach (Collider2D player in stateScript.players)
        {
            StartCoroutine(Grapple(player));
        }

        //Checks for if the player is still in grapple range if not stops grappling
        if (stateScript.playerInRange != true)
        {
            stateScript.grapplingPlayer = false;
            playerStates.isGrappled = false;
        }
    }

    /// <summary>
    /// Grapple the player and decreases health with a slight delay 
    /// in order to make sure damage isnt chipped away to quickly
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public IEnumerator Grapple(Collider2D player)
    {
        recharging = true;
        if(stateScript.playerInRange == true)
        {
            //Gets the health script of the nearest player and decreases it
            HealthManager_Player playerHealth = player.GetComponent<HealthManager_Player>();
            playerHealth.DecreaseHealth(2);
            playerStates.isGrappled = true; 
        }
       
        yield return new WaitForSeconds(.2f);
        recharging = false;
    }
    
    /// <summary>
    /// Attacks once the wind up is over
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public IEnumerator Attack(Collider2D player)
    {
        Debug.Log("Slash Attack");
        if (stateScript.playerInRange == true)
        {
            //Gets the health script of the nearest player and decreases it
            HealthManager_Player playerHealth = player.GetComponent<HealthManager_Player>();
            playerHealth.DecreaseHealth(10);

        }
        yield return new WaitForSeconds(coolDown);
        recharging = false;
        
    }


    #endregion
    #endregion
    #region Gizmos
    /// <summary>
    /// Draws the attack area so that it is visible
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.transform.position, attackArea);
    }
    #endregion
    #region State Handlers
    public void DetermineAction()
    {
        if(!recharging)
        {
            switch(stateScript.enemyState)
            {
                case IndoorStates_Enemy.EnemyStates.Chase:
                    ChasingPlayer();
                    break;
                case IndoorStates_Enemy.EnemyStates.Tracking:
                    TrackingPlayer();
                    break;
                case IndoorStates_Enemy.EnemyStates.Attack:
                    InitiateAttack();
                    break;
                case IndoorStates_Enemy.EnemyStates.Grappling:
                    InitiateGrapple();
                    break;
                case IndoorStates_Enemy.EnemyStates.Idle:
                    break;
            }
        }
    }
    
    /// <summary>
    /// reads the current state of the zombie and changes speed to appropriate variable
    /// </summary>
    private void UpdateSpeedBasedOnStatus()
    {
        float currentSpeed = 0;

        //crawling overrides all other states
        if (statusScript.currentStatus == FodderStatus.Crawling)
        {
            currentSpeed = crawlMoveSpeed;
        }
        else
        {
            //change move speed based on current state
            switch (statusScript.currentStatus)
            {
                case FodderStatus.Idle:
                    currentSpeed = normalSpeed;
                    break;

                case FodderStatus.Stunned:
                    currentSpeed = stunSpeed;
                    break;

                case FodderStatus.Stumbling:
                    currentSpeed = stumbleSpeed;
                    break;

                case FodderStatus.FallenFaceDown:
                    currentSpeed = fallenMoveSpeed;
                    break;

                case FodderStatus.FallenFaceUp:
                    currentSpeed = fallenMoveSpeed;
                    break;

                case FodderStatus.Enraged:
                    currentSpeed = enragedSpeed;
                    break;
            }
        }
        moveSpeed = currentSpeed;
    }
    #endregion

}
