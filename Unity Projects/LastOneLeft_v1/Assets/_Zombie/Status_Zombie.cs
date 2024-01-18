using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    //calculations for these are roll-under, no meets beats
    [SerializeField] float headshotStunChance = 40;
    [SerializeField] float bodyshotStunChance = 20;
    [SerializeField] float stumbleChance = 30;
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

    #region Attempt to apply status

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

    #region apply status
    /// <summary>
    /// applies the stumble status
    /// </summary>
    public void DoStumble()
    {
        isStumbling = true;
    }

    /// <summary>
    /// removes the stumble status
    /// </summary>
    public void StopStumble()
    {
        isStumbling = false;
    }

    /// <summary>
    /// applies the stun status
    /// </summary>
    public void DoStun()
    {
        isStunned = true;
    }

    /// <summary>
    /// removes the stun status
    /// </summary>
    public void StopStun()
    {
        isStunned = false;
    }

    public void DoFallForward()
    {
        isFallenForward = true;
    }

    public void StopFallForward()
    {
        isFallenForward = false;
    }

    public void DoFallBackward()
    {
        isFallenBackward = true;
    }

    public void StopFallBackward()
    {
        isFallenBackward = false;
    }

    /// <summary>
    /// applies the crawling status
    /// </summary>
    public void DoCrawl()
    {
        isCrawling = true;
    }

    /// <summary>
    /// removes the crawling status
    /// </summary>
    public void StopCrawl()
    {
        isCrawling = false;
    }

    /// <summary>
    /// applies the attack status
    /// </summary>
    public void DoAttack()
    {
        isAttacking = true;
    }

    /// <summary>
    /// removes the attack status
    /// </summary>
    public void StopAttack()
    {
        isAttacking = false;
    }

    /// <summary>
    /// applies the chase status
    /// </summary>
    public void DoChase()
    {
        isChasing = true;
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
