using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //statuses
    //DO NOT EDIT THESE DIRECTLY- PLEASE USE METHODS BELOW
    public ZmbStandingStateEnum standingState = ZmbStandingStateEnum.NoStatus;

    public bool isCrawling = false;
    public bool isAttacking = false;
    public bool isChasing = false;

    [SerializeField] float stumbleTimeRemaining;
    [SerializeField] float stunTimeRemaining;
    [SerializeField] float fallenTimeRemaining;
    [SerializeField] float enrageTimeRemaining;


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
        if (standingState == ZmbStandingStateEnum.Stumbling)
        {
            stumbleTimeRemaining -= Time.deltaTime;
            if (stumbleTimeRemaining <= 0)
            {
                StopStumble();
            }
        }

        if (standingState == ZmbStandingStateEnum.Stunned)
        {
            stunTimeRemaining -= Time.deltaTime;
            if (stunTimeRemaining <= 0)
            {
                StopStun();
            }
        }

        if (standingState == ZmbStandingStateEnum.FallenForward ||
            standingState == ZmbStandingStateEnum.FallenBackward)
        {
            fallenTimeRemaining -= Time.deltaTime;
            if (fallenTimeRemaining <= 0)
            {
                if (standingState == ZmbStandingStateEnum.FallenForward) { StopFallForward(); }
                else if (standingState == ZmbStandingStateEnum.FallenBackward) { StopFallBackward(); }
            }
        }

        if (standingState == ZmbStandingStateEnum.Enraged)
        {
            enrageTimeRemaining -= Time.deltaTime;
            if (enrageTimeRemaining <= 0)
            {
                StopEnrage();
            }
        }
    }

    //attempts to apply appropriate statuses based on current zombie state and incoming damage region
    #region Incoming Damage Processors

    public void ProcessHeadshotStatus()
    {
        //ensure prerequisite state is active, then attempt state change
        if (standingState == ZmbStandingStateEnum.NoStatus)
        {
            AttemptStun(DmgRegionEnum.Head);
        }
        else if (standingState == ZmbStandingStateEnum.Stunned)
        {
            AttemptFallBackward(DmgRegionEnum.Head);
        }
    }

    public void ProcessBodyshotStatus()
    {
        //ensure prerequisite state is active
        if (standingState == ZmbStandingStateEnum.NoStatus)
        {
            //attempt proper state change
            AttemptStun(DmgRegionEnum.Body);

        }
        else if (standingState == ZmbStandingStateEnum.Stunned)
        {
            AttemptFallBackward(DmgRegionEnum.Body);
        }
    }

    public void ProcessLegshotStatus()
    {
        //if zombie has no status, attempt a stumble
        //if zombie is stumbling, attempt a fall forward
        //if zombie is stunned and recieves leg damage, attempt a stumble

        //ensure prerequisite state is active
        if (standingState == ZmbStandingStateEnum.NoStatus)
        {
            //attempt proper state change
            AttemptStumble();
        }
        else if (standingState == ZmbStandingStateEnum.Stumbling)
        {
            AttemptFallForward();
        }
        else if (standingState == ZmbStandingStateEnum.Stunned)
        {
            AttemptStumble();
        }
    }

    #endregion

    #region "attempt status" methods

    /// <summary>
    /// determines if a stumble occurs
    /// </summary>
    public void AttemptStumble()
    {
        if (RNGRolls_System.RollUnder(stumbleChance))
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
    /// determines if a stun occurs
    /// </summary>
    /// <param name="damagedRegion"></param>
    public void AttemptStun(DmgRegionEnum damagedRegion)
    {
        float successCutoff = 0;

        if (damagedRegion == DmgRegionEnum.Head)
        {
            successCutoff = headshotStunChance;
        }
        else if (damagedRegion == DmgRegionEnum.Body)
        {
            successCutoff = bodyshotStunChance;
        }
        else
        { Debug.Log("Error detected in AttemptStun damagedRegion argument"); }

        if (RNGRolls_System.RollUnder(successCutoff))
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
    public void AttemptFallForward()
    {
        if (RNGRolls_System.RollUnder(fallForwardChance))
        { 
            DoFallForward();
            if (printDebugMessages) { Debug.Log("Fall Forward Success"); }
        }
        else
        {
            if (printDebugMessages) { Debug.Log("Fall Forward Failure"); }
        }
    }

    /// <summary>
    /// determines if the zombie falls backward
    /// </summary>
    /// <param name="damagedRegion"></param>
    public void AttemptFallBackward(DmgRegionEnum damagedRegion)
    {
        float successCutoff = 0;

        if (damagedRegion == DmgRegionEnum.Head)
        {
            successCutoff = headshotFallBackwardChance;
        }
        else if (damagedRegion == DmgRegionEnum.Body)
        {
            successCutoff = bodyshotFallBackwardChance;
        }
        else
        { Debug.Log("Error detected in AttemptFallBackward damagedRegion argument"); }

        if (RNGRolls_System.RollUnder(successCutoff)) 
        {
            DoFallBackward();
            if (printDebugMessages) { Debug.Log("Fall Backward Success"); }
        }
        else
        {
            if (printDebugMessages) { Debug.Log("Fall Backward Failure"); }
        }
    }
    #endregion

    #region "do status" methods
    /// <summary>
    /// applies the stumble status
    /// </summary>
    public void DoStumble()
    {
        standingState = ZmbStandingStateEnum.Stumbling;
        stumbleTimeRemaining = stumbleDuration;
    }

    /// <summary>
    /// applies the stun status
    /// </summary>
    public void DoStun()
    {
        standingState = ZmbStandingStateEnum.Stunned;
        stunTimeRemaining = stunDuration;
    }

    public void DoFallForward()
    {
        standingState = ZmbStandingStateEnum.FallenForward;
        fallenTimeRemaining = fallenDuration;
    }

    public void DoFallBackward()
    {
        standingState = ZmbStandingStateEnum.FallenBackward;
        fallenTimeRemaining = fallenDuration;
    }

    public void DoEnrage()
    {
        standingState = ZmbStandingStateEnum.Enraged;
        enrageTimeRemaining = enrageDuration;

    }

    /// <summary>
    /// applies the crawling status
    /// </summary>
    public void DoCrawl()
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

    #endregion

    #region "stop status" methods

    /// <summary>
    /// removes the stumble status
    /// </summary>
    public void StopStumble()
    {
        standingState = ZmbStandingStateEnum.NoStatus;
        stumbleTimeRemaining = 0;
    }

    /// <summary>
    /// removes the stun status
    /// </summary>
    public void StopStun()
    {
        standingState = ZmbStandingStateEnum.NoStatus;
        stunTimeRemaining = 0;
    }


    public void StopFallForward()
    {
        standingState = ZmbStandingStateEnum.NoStatus;
        fallenTimeRemaining = 0;
    }

    public void StopFallBackward()
    {
        standingState = ZmbStandingStateEnum.NoStatus;
        fallenTimeRemaining = 0;
    }

    public void StopEnrage()
    {
        standingState = ZmbStandingStateEnum.Stumbling;
        enrageTimeRemaining = 0;
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

    #endregion

}
