using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LimbAnimController))]

public class Status_Zombie : MonoBehaviour
{

    [Header("CONFIG")]

    //calculations for these are roll-under, no meets beats
    [SerializeField] float stumbleDuration = 1;
    [SerializeField] float stumbleChance = 30;

    [SerializeField] float stunDuration = 3;
    [SerializeField] float headshotStunChance = 40;
    [SerializeField] float bodyshotStunChance = 20;

    [SerializeField] float fallenDuration = 5;
    [SerializeField] float fallForwardChance = 50;
    [SerializeField] float headshotFallBackwardChance = 60;
    [SerializeField] float bodyshotFallBackwardChance = 30;

    [SerializeField] float enrageDuration = 2;

    [SerializeField] bool printDebugMessages = false;

    [Header("DEBUG")]

    [SerializeField] LimbAnimController myLimbAnimController;

    //statuses
    //DO NOT EDIT THESE DIRECTLY- PLEASE USE METHODS BELOW
    public FodderStatus status = FodderStatus.Idle;

    [SerializeField] LimbCondition headCondition = LimbCondition.Intact;
    public LimbCondition LArmCondition = LimbCondition.Intact;
    public LimbCondition RArmCondition = LimbCondition.Intact;
    [SerializeField] LimbCondition legsCondition = LimbCondition.Intact;

    public bool isCrawling = false;
    public bool isAttacking = false;
    public bool isChasing = false;
    public bool isTracking = false;

    [SerializeField] float stumbleTimeRemaining;
    [SerializeField] float stunTimeRemaining;
    [SerializeField] float fallenTimeRemaining;
    [SerializeField] float enrageTimeRemaining;

    private void Awake()
    {
        myLimbAnimController = GetComponent<LimbAnimController>();
    }

    private void Update()
    {
        //checks if status is active, then decrements its timer and checks if it == 0 and should end
        //doesnt update if zombie is crawling because crawling takes priority over all statuses
        if (!isCrawling) { UpdateStandingStatusTimers(); }


    }

    /// <summary>
    /// decrements the currently active status effect & removes it when the timer hits 0
    /// </summary>
    private void UpdateStandingStatusTimers()
    {
        if (status == FodderStatus.Stumbling)
        {
            stumbleTimeRemaining -= Time.deltaTime;
            if (stumbleTimeRemaining <= 0)
            {
                StopStumble();
            }
        }

        if (status == FodderStatus.Stunned)
        {
            stunTimeRemaining -= Time.deltaTime;
            if (stunTimeRemaining <= 0)
            {
                StopStun();
            }
        }

        if (status == FodderStatus.FallenFaceDown ||
            status == FodderStatus.FallenFaceUp)
        {
            fallenTimeRemaining -= Time.deltaTime;
            if (fallenTimeRemaining <= 0)
            {
                if (status == FodderStatus.FallenFaceDown) { GetUpFromFallForward(); }
                else if (status == FodderStatus.FallenFaceUp) { GetUpFromFallBackward(); }
            }
        }

        if (status == FodderStatus.Enraged)
        {
            enrageTimeRemaining -= Time.deltaTime;
            if (enrageTimeRemaining <= 0)
            {
                StopEnrage();
            }
        }
    }

    /// <summary>
    /// updates a limb's condition
    /// </summary>
    /// <param name="limb"></param>
    /// <param name="newCondition"></param>
    public void ChangeLimbCondition(FodderLimb limb, LimbCondition newCondition)
    {
        switch (limb)
        {
            case FodderLimb.Head:
                headCondition = newCondition;
            break;

            case FodderLimb.LArm:
                LArmCondition = newCondition;
            break;

            case FodderLimb.RArm:
                RArmCondition = newCondition;
            break;

            case FodderLimb.Legs:
                legsCondition = newCondition;
            break;
        }

    }

    //attempts to apply appropriate statuses based on current zombie state and incoming damage region
    #region Incoming Damage Processors

    public void ProcessCritHit(float statusModifier)
    {
        //ensure prerequisite state is active, then attempt state change
        if (status == FodderStatus.Idle)
        {
            AttemptStun(DmgRegionEnum.Crit, statusModifier);
        }
        else if (status == FodderStatus.Stunned)
        {
            AttemptFallBackward(DmgRegionEnum.Crit, statusModifier);
        }
    }

    public void ProcessArmoredHit(float statusModifier)
    {
        //ensure prerequisite state is active
        if (status == FodderStatus.Idle)
        {
            //attempt proper state change
            AttemptStun(DmgRegionEnum.Armored, statusModifier);

        }
        else if (status == FodderStatus.Stunned)
        {
            AttemptFallBackward(DmgRegionEnum.Armored, statusModifier);
        }
    }

    public void ProcessWeakHit(float statusModifier)
    {
        //if zombie has no status, attempt a stumble
        //if zombie is stumbling, attempt a fall forward
        //if zombie is stunned and recieves leg damage, attempt a stumble

        //ensure prerequisite state is active
        if (status == FodderStatus.Idle)
        {
            //attempt proper state change
            AttemptStumble(statusModifier);
        }
        else if (status == FodderStatus.Stumbling)
        {
            AttemptFallForward(statusModifier);
        }
        else if (status == FodderStatus.Stunned)
        {
            AttemptStumble(statusModifier);
        }
    }

    #endregion

    #region "attempt status" methods

    /// <summary>
    /// determines if a stumble occurs
    /// </summary>
    public void AttemptStumble(float statusModifier)
    {
        if (RNGRolls_System.RollUnder(stumbleChance * statusModifier))
        {
            DoStumble(); 
            if (printDebugMessages) { Debug.Log("Stumble Sucess"); }
        }
        else
        {
            if (printDebugMessages) { Debug.Log("Stumble Failure"); }
        }
    }

    /// <summary>
    /// determines if a stun occurs, factoring in which damage region was hit
    /// </summary>
    /// <param name="damagedRegion"></param>
    public void AttemptStun(DmgRegionEnum damagedRegion, float statusModifier)
    {
        float successCutoff = 0;

        if (damagedRegion == DmgRegionEnum.Crit)
        {
            successCutoff = headshotStunChance;
        }
        else if (damagedRegion == DmgRegionEnum.Armored)
        {
            successCutoff = bodyshotStunChance;
        }
        else
        { Debug.Log("Error detected in AttemptStun damagedRegion argument"); }

        if (RNGRolls_System.RollUnder(successCutoff * statusModifier))
        {
            DoStun(); 
            if (printDebugMessages) { Debug.Log("Stun Success"); }
        }
        else
        {
            if (printDebugMessages) { Debug.Log("Stun Failure"); }
        }
    }

    /// <summary>
    /// determines if the zombie falls forward
    /// </summary>
    public void AttemptFallForward(float statusModifier)
    {
        if (RNGRolls_System.RollUnder(fallForwardChance * statusModifier))
        { 
            StartFallForward();
            if (printDebugMessages) { Debug.Log("Fall Forward Success"); }
        }
        else
        {
            if (printDebugMessages) { Debug.Log("Fall Forward Failure"); }
        }
    }

    /// <summary>
    /// determines if the zombie falls backward, considering which region was hit
    /// </summary>
    /// <param name="damagedRegion"></param>
    public void AttemptFallBackward(DmgRegionEnum damagedRegion, float statusModifier)
    {
        float successCutoff = 0;

        if (damagedRegion == DmgRegionEnum.Crit)
        {
            successCutoff = headshotFallBackwardChance;
        }
        else if (damagedRegion == DmgRegionEnum.Armored)
        {
            successCutoff = bodyshotFallBackwardChance;
        }
        else
        { Debug.Log("Error detected in AttemptFallBackward damagedRegion argument"); }

        if (RNGRolls_System.RollUnder(successCutoff * statusModifier)) 
        {
            StartFallBackward();
            if (printDebugMessages) { Debug.Log("Fall Backward Success"); }
        }
        else
        {
            if (printDebugMessages) { Debug.Log("Fall Backward Failure"); }
        }
    }
    #endregion

    //applies a status by flipping a bool and initializing its duration value
    #region "do status" methods
    /// <summary>
    /// applies the stumble status
    /// </summary>
    public void DoStumble()
    {
        status = FodderStatus.Stumbling;
        stumbleTimeRemaining = stumbleDuration;
    }

    /// <summary>
    /// applies the stun status
    /// </summary>
    public void DoStun()
    {
        status = FodderStatus.Stunned;
        stunTimeRemaining = stunDuration;
    }

    public void StartFallForward()
    {
        status = FodderStatus.FallenFaceDown;
        fallenTimeRemaining = fallenDuration;
    }

    public void StartFallBackward()
    {
        status = FodderStatus.FallenFaceUp;
        fallenTimeRemaining = fallenDuration;
    }

    public void StartEnrage()
    {
        status = FodderStatus.Enraged;
        enrageTimeRemaining = enrageDuration;

    }

    /// <summary>
    /// applies the crawling status
    /// </summary>
    public void StartCrawl()
    {
        isCrawling = true;
    }

    /// <summary>
    /// applies the attack status
    /// </summary>
    public void DoAttack()
    {
        isAttacking = true;
    }

    /// <summary>
    /// applies the chase status
    /// </summary>
    public void DoChase()
    {
        isChasing = true;
    }

    /// <summary>
    /// applies the tracking status
    /// </summary>
    public void DoTrack()
    {
        isTracking = true;
    }

    #endregion

    //stops a status by changing the state
    #region "stop status" methods

    /// <summary>
    /// removes the stumble status
    /// </summary>
    public void StopStumble()
    {
        status = FodderStatus.Idle;
    }

    /// <summary>
    /// removes the stun status
    /// </summary>
    public void StopStun()
    {
        status = FodderStatus.Idle;
    }

    public void GetUpFromFallForward()
    {
        status = FodderStatus.Idle;
    }

    public void GetUpFromFallBackward()
    {
        status = FodderStatus.Idle;
    }

    public void StopEnrage()
    {
        status = FodderStatus.Stumbling;
    }

    /// <summary>
    /// removes the crawling status
    /// </summary>
    public void StopCrawl()
    {
        isCrawling = false;
    }

    /// <summary>
    /// removes the attack status
    /// </summary>
    public void StopAttack()
    {
        isAttacking = false;
    }

    /// <summary>
    /// removes the chase status
    /// </summary>
    public void StopChase()
    {
        isChasing = false;
    }

    /// <summary>
    /// removes the track status
    /// </summary>
    public void StopTrack()
    {
        isTracking = false;
    }

    #endregion

}
