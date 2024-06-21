using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbAnimator : MonoBehaviour
{

    //edit these if animation state names are ever changed
    #region animation state name constants
    const string idleState = "Idle";
    const string stumblingState = "Stumbling";
    const string stunnedState = "Stunned";
    const string fallingForwardState = "Falling Forward";
    const string fallenFaceDownState = "Fallen Face Down";
    const string pushUpRecoverState = "Push Up Recover";
    const string fallingBackwardState = "Falling Backward";
    const string fallenFaceUpState = "Fallen Face Up";
    const string sitUpRecoverState = "Sit Up Recover";
    const string enragingState = "Enraging";
    const string enragedState = "Enraged";
    const string legsBreakingState = "Legs Breaking";
    const string crawlingState = "Crawling";

    const string idleState_Broken = "Idle";
    const string stumblingState_Broken = "Stumbling";
    const string stunnedState_Broken = "Stunned";
    const string fallingForwardState_Broken = "Falling Forward";
    const string fallenFaceDownState_Broken = "Fallen Face Down";
    const string pushUpRecoverState_Broken = "Push Up Recover";
    const string fallingBackwardState_Broken = "Falling Backward";
    const string fallenFaceUpState_Broken = "Fallen Face Up";
    const string sitUpRecoverState_Broken = "Sit Up Recover";
    const string enragingState_Broken = "Enraging";
    const string enragedState_Broken = "Enraged";
    const string legsBreakingState_Broken = "Legs Breaking";
    const string crawlingState_Broken = "Crawling";
    #endregion

    string[] intactStateNames = { idleState,
                            stumblingState,
                            stunnedState,
                            fallingForwardState, fallenFaceDownState, pushUpRecoverState,
                            fallingBackwardState, fallenFaceUpState, sitUpRecoverState,
                            enragingState, enragedState,
                            legsBreakingState, crawlingState};

    string[] brokenStateNames = { idleState_Broken,
                            stumblingState_Broken,
                            stunnedState_Broken,
                            fallingForwardState_Broken, fallenFaceDownState_Broken, pushUpRecoverState_Broken,
                            fallingBackwardState_Broken, fallenFaceUpState_Broken, sitUpRecoverState_Broken,
                            enragingState_Broken, enragedState_Broken,
                            legsBreakingState_Broken, crawlingState_Broken};

    [Header("CONFIG")]
    [SerializeField] FodderLimb limbType;

    [Header("DEBUG")]
    public Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        //play correct animation on startup
        GetComponentInParent<Status_Zombie>().PlayAnimationOnLimbs();
    }

    public void PlayAnimation(FodderStatus status,
                        LimbCondition headCondition,
                        LimbCondition LArmCondition,
                        LimbCondition RArmCondition,
                        LimbCondition legsCondition)
    {

        bool isBroken = false;

        isBroken = CheckBrokenStatus(headCondition, LArmCondition, RArmCondition, legsCondition, isBroken);


        string animStateName;
        if (isBroken)
        {
            animStateName = intactStateNames[((int)status)];
        }
        else
        {
            animStateName = brokenStateNames[((int)status)];
        }

        myAnimator.CrossFade(animStateName, 0, 0);
    }

    private bool CheckBrokenStatus(LimbCondition headCondition, LimbCondition LArmCondition, LimbCondition RArmCondition, LimbCondition legsCondition, bool isBroken)
    {
        switch (limbType)
        {
            case FodderLimb.Head:
                if (headCondition == LimbCondition.Broken) { isBroken = true; }
                break;

            case FodderLimb.LArm:
                if (LArmCondition == LimbCondition.Broken) { isBroken = true; }
                break;

            case FodderLimb.RArm:
                if (RArmCondition == LimbCondition.Broken) { isBroken = true; }
                break;

            case FodderLimb.Legs:
                if (legsCondition == LimbCondition.Broken) { isBroken = true; }
                break;
        }

        return isBroken;
    }
}
