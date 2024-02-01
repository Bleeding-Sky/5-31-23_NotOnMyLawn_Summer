using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum zombieStatusEnum { Idle, Stumbling, Stunned, FallenForward, FallenBackward, Crawling }

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

    [Header("DEBUG")]
    //statuses
    //DO NOT EDIT THESE DIRECTLY- PLEASE USE METHODS BELOW
    public bool isStumbling = false;
    public bool isStunned = false;
    public bool isFallenForward = false;
    public bool isFallenBackward = false;
    public bool isCrawling = false;
    public bool isAttacking = false;
    public bool isChasing = false;

    [SerializeField] float stumbleTimeRemaining;
    [SerializeField] float stunTimeRemaining;
    [SerializeField] float fallenTimeRemaining;

    zombieStatusEnum statusEnum = zombieStatusEnum.Idle;

    private void Update()
    {
        //checks if status is active, then decrements its timer and checks if it == 0 and should end
        #region decrement status timers

        if (isStumbling)
        {
            stumbleTimeRemaining -= Time.deltaTime;
            if (stumbleTimeRemaining <= 0)
            {
                StopStumble();
            }
        }

        if (isStunned)
        {
            stunTimeRemaining -= Time.deltaTime;
            if (stunTimeRemaining <= 0)
            {
                StopStun();
            }
        }

        if (isFallenForward || isFallenBackward)
        {
            fallenTimeRemaining -= Time.deltaTime;
            if (fallenTimeRemaining <= 0)
            {
                if (isFallenForward) { StopFallForward(); }
                else if (isFallenBackward) { StopFallBackward(); }
            }
        }

        #endregion

    }

    #region "attempt status" methods

    /// <summary>
    /// determines if a stumble occurs
    /// </summary>
    public void AttemptStumble()
    {
        if (isStumbling)
        {
            AttemptFallForward();
            return;
        }

        if (RNGRolls_System.RollUnder(stumbleChance)) { DoStumble(); }
    }

    /// <summary>
    /// determines if a stun occurs
    /// </summary>
    /// <param name="damagedRegion"></param>
    public void AttemptStun(DmgRegionEnum damagedRegion)
    {
        if (isStunned)
        {
            AttemptFallBackward(damagedRegion);
            return;
        }

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

        if (RNGRolls_System.RollUnder(successCutoff)) { DoStun(); }
    }

    /// <summary>
    /// determines if the zombie falls forward
    /// </summary>
    public void AttemptFallForward()
    {
        if (RNGRolls_System.RollUnder(fallForwardChance)) { DoFallForward(); }
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

        if (RNGRolls_System.RollUnder(successCutoff)) { DoFallBackward(); }
    }
    #endregion

    #region "do status" methods
    /// <summary>
    /// applies the stumble status
    /// </summary>
    public void DoStumble()
    {
        isStumbling = true;
        stumbleTimeRemaining = stumbleDuration;
    }

    /// <summary>
    /// applies the stun status
    /// </summary>
    public void DoStun()
    {
        isStunned = true;
        stunTimeRemaining = stunDuration;
    }

    public void DoFallForward()
    {
        isFallenForward = true;
        fallenTimeRemaining = fallenDuration;
    }

    public void DoFallBackward()
    {
        isFallenBackward = true;
        fallenTimeRemaining = fallenDuration;
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
        isStumbling = false;
    }

    /// <summary>
    /// removes the stun status
    /// </summary>
    public void StopStun()
    {
        isStunned = false;
    }

    public void StopFallForward()
    {
        isFallenForward = false;
    }

    public void StopFallBackward()
    {
        isFallenBackward = false;
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
