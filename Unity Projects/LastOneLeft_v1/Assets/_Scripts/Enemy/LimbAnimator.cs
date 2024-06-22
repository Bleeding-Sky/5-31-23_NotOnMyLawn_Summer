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

    const string idleState_Broken = "Idle (Broken)";
    const string stumblingState_Broken = "Stumbling (Broken)";
    const string stunnedState_Broken = "Stunned (Broken)";
    const string fallingForwardState_Broken = "Falling Forward (Broken)";
    const string fallenFaceDownState_Broken = "Fallen Face Down (Broken)";
    const string pushUpRecoverState_Broken = "Push Up Recover (Broken)";
    const string fallingBackwardState_Broken = "Falling Backward (Broken)";
    const string fallenFaceUpState_Broken = "Fallen Face Up (Broken)";
    const string sitUpRecoverState_Broken = "Sit Up Recover (Broken)";
    const string enragingState_Broken = "Enraging (Broken)";
    const string enragedState_Broken = "Enraged (Broken)";
    const string legsBreakingState_Broken = "Legs Breaking (Broken)";
    const string crawlingState_Broken = "Crawling (Broken)";
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
    [SerializeField] FodderLimb myLimbType;
    [SerializeField] bool showDebugMessages = false;

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
                        LimbCondition bodyCondition,
                        LimbCondition legsCondition)
    {
        if (showDebugMessages) { Debug.Log("Playing animation"); }

        //check if limb is broken
        bool isBroken = CheckBrokenStatus(headCondition, LArmCondition, RArmCondition, bodyCondition, legsCondition);

        //use correct animation state array depending on if the limb is broken
        string animStateName;
        if (isBroken)
        {
            animStateName = brokenStateNames[((int)status)];
        }
        else
        {
            animStateName = intactStateNames[((int)status)];
        }

        myAnimator.CrossFade(animStateName, 0, 0);
    }

    /// <summary>
    /// checks all limb statuses against its own limb type to see if it is broken
    /// </summary>
    /// <param name="headCondition"></param>
    /// <param name="LArmCondition"></param>
    /// <param name="RArmCondition"></param>
    /// <param name="legsCondition"></param>
    /// <returns></returns>
    private bool CheckBrokenStatus( LimbCondition headCondition, 
                                    LimbCondition LArmCondition, 
                                    LimbCondition RArmCondition, 
                                    LimbCondition bodyCondition, 
                                    LimbCondition legsCondition)
    {
        bool isBroken = false;

        switch (myLimbType)
        {
            case FodderLimb.Head:
                if (headCondition == LimbCondition.Broken) 
                { 
                    isBroken = true;
                    if (showDebugMessages) { Debug.Log("Head detected as broken"); }
                }
                
                
                break;

            case FodderLimb.LArm:
                if (LArmCondition == LimbCondition.Broken) 
                { 
                    isBroken = true;
                    if (showDebugMessages) { Debug.Log("Left Arm detected as broken"); }
                }
                

                break;

            case FodderLimb.RArm:
                if (RArmCondition == LimbCondition.Broken)
                { 
                    isBroken = true;
                    if (showDebugMessages) { Debug.Log("Right Arm detected as broken"); }
                }
                
                break;

            case FodderLimb.Body:
                if (bodyCondition == LimbCondition.Broken)
                {
                    isBroken = true;
                    if (showDebugMessages) { Debug.Log("Body detected as broken"); }
                }

                break;

            case FodderLimb.Legs:
                if (legsCondition == LimbCondition.Broken)
                { 
                    isBroken = true;
                    if (showDebugMessages) { Debug.Log("Legs detected as broken"); }
                }
                
                break;
        }

        return isBroken;
    }
}
