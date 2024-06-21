using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
    public FodderStatus currentStatus = FodderStatus.Idle;

    [SerializeField] LimbCondition headCondition = LimbCondition.Intact;
    public LimbCondition LArmCondition = LimbCondition.Intact;
    public LimbCondition RArmCondition = LimbCondition.Intact;
    [SerializeField] LimbCondition legsCondition = LimbCondition.Intact;


    private void Awake()
    {
        myLimbAnimController = GetComponent<LimbAnimController>();
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

    //attempts to apply appropriate statuses based on current zombie state and incoming damage region.
    //if zombie's legs are broken, no status will be applied
    #region Incoming Damage Processors

    public void ProcessCritHit(float statusModifier)
    {
        if (legsCondition == LimbCondition.Broken) { return; }

        //ensure prerequisite state is active, then attempt state change
        if (currentStatus == FodderStatus.Idle)
        {
            AttemptStun(DmgRegionEnum.Crit, statusModifier);
        }
        else if (currentStatus == FodderStatus.Stunned)
        {
            AttemptFallBackward(DmgRegionEnum.Crit, statusModifier);
        }
    }

    public void ProcessArmoredHit(float statusModifier)
    {
        if (legsCondition == LimbCondition.Broken) { return; }

        //ensure prerequisite state is active
        if (currentStatus == FodderStatus.Idle)
        {
            //attempt proper state change
            AttemptStun(DmgRegionEnum.Armored, statusModifier);

        }
        else if (currentStatus == FodderStatus.Stunned)
        {
            AttemptFallBackward(DmgRegionEnum.Armored, statusModifier);
        }

    }

    public void ProcessWeakHit(float statusModifier)
    {
        if (legsCondition == LimbCondition.Broken) { return; }

        //if zombie has no status, attempt a stumble
        //if zombie is stumbling, attempt a fall forward
        //if zombie is stunned and recieves leg damage, attempt a stumble

        //ensure prerequisite state is active
        if (currentStatus == FodderStatus.Idle)
        {
            //attempt proper state change
            AttemptStumble(statusModifier);
        }
        else if (currentStatus == FodderStatus.Stumbling)
        {
            AttemptFallForward(statusModifier);
        }
        else if (currentStatus == FodderStatus.Stunned)
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
            FallForward();
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
            FallBackward();
            if (printDebugMessages) { Debug.Log("Fall Backward Success"); }
        }
        else
        {
            if (printDebugMessages) { Debug.Log("Fall Backward Failure"); }
        }
    }
    #endregion

    public void PlayAnimationOnLimbs()
    {
        myLimbAnimController.ChangeAnimationState(currentStatus, headCondition, LArmCondition, RArmCondition, legsCondition);
    }

    //applies a status  if its prerequisite status is met.
    //also triggers animations on every status change.
    #region "do status" methods

    public void BecomeIdle()
    {
        if (    currentStatus == FodderStatus.Stunned
            ||  currentStatus == FodderStatus.Stumbling
            ||  currentStatus == FodderStatus.PushUpRecover
            ||  currentStatus == FodderStatus.SitUpRecover)
        {
            currentStatus = FodderStatus.Idle;

            PlayAnimationOnLimbs();
        }
    }

    /// <summary>
    /// applies the stumble status 
    /// </summary>
    public void DoStumble()
    {
        if (currentStatus == FodderStatus.Idle
            || currentStatus == FodderStatus.Enraged)
        {
            currentStatus = FodderStatus.Stumbling;
            Invoke(nameof(BecomeIdle), stumbleDuration);

            PlayAnimationOnLimbs();
        }
    }

    /// <summary>
    /// applies the stun status
    /// </summary>
    public void DoStun()
    {
        if (currentStatus == FodderStatus.Idle)
        {
            currentStatus = FodderStatus.Stunned;
            Invoke(nameof(BecomeIdle), stunDuration);

            PlayAnimationOnLimbs();
        }
    }

    /// <summary>
    /// changes state to falling forward.
    /// relies on limbAnimController to trigger next state when transition anim is done
    /// </summary>
    public void FallForward()
    {
        if (currentStatus == FodderStatus.Stumbling)
        {
            currentStatus = FodderStatus.FallingForward;

            PlayAnimationOnLimbs();
        }
    }

    /// <summary>
    /// makes enemy fall face down and invokes recovery state after a delay
    /// </summary>
    public void StartFallenFaceDownStatus()
    {
        if (currentStatus == FodderStatus.FallingForward)
        {
            currentStatus = FodderStatus.FallenFaceDown;
            Invoke(nameof(StartPushUpRecover), fallenDuration);

            PlayAnimationOnLimbs();
        }
    }

    /// <summary>
    /// changes state to pushUpRecover.
    /// relies on limbAnimController to trigger next state when transition anim is done
    /// </summary>
    public void StartPushUpRecover()
    {
        if (currentStatus == FodderStatus.FallenFaceDown)
        {
            currentStatus = FodderStatus.PushUpRecover;

            PlayAnimationOnLimbs();
        }
    }

    /// <summary>
    /// changes state to fallingBackward.
    /// relies on limbAnimController to trigger next state when transition anim is done
    /// </summary>
    public void FallBackward()
    {
        if (currentStatus == FodderStatus.Stunned)
        {
            currentStatus = FodderStatus.FallingBackward;

            PlayAnimationOnLimbs();
        }
    }

    public void StartFallenFaceUpStatus()
    {
        if (currentStatus == FodderStatus.FallingBackward)
        {
            currentStatus = FodderStatus.FallenFaceUp;
            Invoke(nameof(SitUpRecover), fallenDuration);

            PlayAnimationOnLimbs();
        }
    }

    /// <summary>
    /// changes state to sitUpRecover.
    /// relies on limbAnimController to trigger next state when transition anim is done
    /// </summary>
    public void SitUpRecover()
    {
        if (currentStatus == FodderStatus.FallenFaceUp)
        {
            currentStatus = FodderStatus.SitUpRecover;

            PlayAnimationOnLimbs();
        }
    }


    public void StartEnraging()
    {
        if (currentStatus == FodderStatus.Stunned)
        {
            currentStatus = FodderStatus.Enraging;

            PlayAnimationOnLimbs();
        }
    }

    public void BecomeEnraged()
    {
        if (currentStatus == FodderStatus.Enraging)
        {
            currentStatus = FodderStatus.Enraged;
            Invoke(nameof(DoStumble), enrageDuration);

            PlayAnimationOnLimbs();
        }
    }

    public void BreakLegs()
    {
        currentStatus = FodderStatus.LegsBreaking;

        PlayAnimationOnLimbs();
    }

    /// <summary>
    /// applies the crawling status
    /// </summary>
    public void StartCrawl()
    {
        if (currentStatus == FodderStatus.LegsBreaking)
        {
            currentStatus = FodderStatus.Crawling;

            PlayAnimationOnLimbs();
        }
    }

    #endregion


}
