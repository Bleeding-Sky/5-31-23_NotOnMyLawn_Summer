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

    //attempts to apply appropriate statuses based on current zombie state and incoming damage region
    #region Incoming Damage Processors

    public void ProcessCritHit(float statusModifier)
    {
        if (legsCondition != LimbCondition.Broken)
        {
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
    }

    public void ProcessArmoredHit(float statusModifier)
    {
        if (legsCondition != LimbCondition.Broken)
        {
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
    }

    public void ProcessWeakHit(float statusModifier)
    {
        //if zombie has no status, attempt a stumble
        //if zombie is stumbling, attempt a fall forward
        //if zombie is stunned and recieves leg damage, attempt a stumble

        if (legsCondition != LimbCondition.Broken)
        {
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

    void PlayAnimationOnLimbs()
    {
        myLimbAnimController.PlayAnimation(currentStatus);
    }

    void ChangeStatus(FodderStatus newStatus)
    {

    }

    //applies a status by flipping a bool and initializing its duration value
    #region "do status" methods
    /// <summary>
    /// applies the stumble status
    /// </summary>
    public void DoStumble()
    {
        currentStatus = FodderStatus.Stumbling;
        Invoke(nameof(StopStumble), stumbleDuration);
        PlayAnimationOnLimbs();
        
    }

    /// <summary>
    /// applies the stun status
    /// </summary>
    public void DoStun()
    {
        currentStatus = FodderStatus.Stunned;
        Invoke(nameof(StopStun), stunDuration);
        PlayAnimationOnLimbs();
    }

    /// <summary>
    /// changes state to falling forward.
    /// relies on limbAnimController to trigger next state when transition anim is done
    /// </summary>
    public void FallForward()
    {
        currentStatus = FodderStatus.FallingForward;
        PlayAnimationOnLimbs();
    }

    /// <summary>
    /// makes enemy fall face down and invokes recovery state after a delay
    /// </summary>
    public void StartFallenFaceDownStatus()
    {
        currentStatus = FodderStatus.FallenFaceDown;
        Invoke(nameof(StartPushUpRecover), fallenDuration);
        PlayAnimationOnLimbs();

    }

    /// <summary>
    /// changes state to pushUpRecover.
    /// relies on limbAnimController to trigger next state when transition anim is done
    /// </summary>
    public void StartPushUpRecover()
    {
        currentStatus = FodderStatus.PushUpRecover;
        PlayAnimationOnLimbs();
    }

    /// <summary>
    /// changes state to fallingBackward.
    /// relies on limbAnimController to trigger next state when transition anim is done
    /// </summary>
    public void FallBackward()
    {
        currentStatus = FodderStatus.FallingBackward;
        PlayAnimationOnLimbs();
    }

    public void StartFallenFaceUpStatus()
    {
        currentStatus = FodderStatus.FallenFaceUp;
        Invoke(nameof(SitUpRecover), fallenDuration);
        PlayAnimationOnLimbs();
    }

    /// <summary>
    /// changes state to sitUpRecover.
    /// relies on limbAnimController to trigger next state when transition anim is done
    /// </summary>
    public void SitUpRecover()
    {
        currentStatus = FodderStatus.SitUpRecover;
        PlayAnimationOnLimbs();
    }


    public void StartEnraging()
    {
        currentStatus = FodderStatus.Enraging;
        PlayAnimationOnLimbs();
    }

    public void BecomeEnraged()
    {
        currentStatus = FodderStatus.Enraged;
        Invoke(nameof(DoStumble), enrageDuration);
        PlayAnimationOnLimbs();
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
        currentStatus = FodderStatus.Crawling;
        PlayAnimationOnLimbs();
    }

    #endregion

    //stops a status by changing the state
    #region "stop status" methods

    /// <summary>
    /// removes the stumble status
    /// </summary>
    public void StopStumble()
    {
        currentStatus = FodderStatus.Idle;
    }

    /// <summary>
    /// removes the stun status
    /// </summary>
    public void StopStun()
    {
        currentStatus = FodderStatus.Idle;
    }

    public void GetUpFromFallForward()
    {
        currentStatus = FodderStatus.Idle;
    }

    public void GetUpFromFallBackward()
    {
        currentStatus = FodderStatus.Idle;
    }


    #endregion

}
