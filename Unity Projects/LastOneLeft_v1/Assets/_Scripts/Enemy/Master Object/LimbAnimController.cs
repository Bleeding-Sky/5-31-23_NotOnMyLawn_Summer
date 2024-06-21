using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status_Zombie))]

public class LimbAnimController : MonoBehaviour
{
    [SerializeField] List<Animator> limbAnimators;
    [SerializeField] Status_Zombie statusScript;

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
    #endregion

    string[] stateNames = { idleState,
                            stumblingState, 
                            stunnedState,
                            fallingForwardState, fallenFaceDownState, pushUpRecoverState,
                            fallingBackwardState, fallenFaceUpState, sitUpRecoverState,
                            enragingState, enragedState,
                            legsBreakingState, crawlingState};

    [SerializeField] FodderStatus currentStatus;

    private void Awake()
    {
        statusScript = GetComponent<Status_Zombie>();
    }

    // Start is called before the first frame update
    void Start()
    {
        FetchLimbAnimators();
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// plays an animation on all child limb animators.
    /// if the state is a transition, invokes a delayed method to move to the next state.
    /// </summary>
    /// <param name="status"></param>
    public void PlayAnimation(FodderStatus status)
    {

        FetchLimbAnimators();
        string animStateName = stateNames[((int)status)];

        foreach (Animator animator in limbAnimators)
        {
            animator.CrossFade(animStateName, 0, 0);
        }

        switch(status)
        {
            case FodderStatus.FallingForward:
                statusScript.Invoke(nameof(statusScript.StartFallenFaceDownStatus), limbAnimators[0].GetCurrentAnimatorStateInfo(0).length);
                break;

            case FodderStatus.PushUpRecover:
                statusScript.Invoke(nameof(statusScript.StartPushUpRecover), limbAnimators[0].GetCurrentAnimatorStateInfo(0).length);
                break;

            case FodderStatus.FallingBackward:
                statusScript.Invoke(nameof(statusScript.StartFallenFaceUpStatus), limbAnimators[0].GetCurrentAnimatorStateInfo(0).length);
                break;

            case FodderStatus.SitUpRecover:
                statusScript.Invoke(nameof(statusScript.StartPushUpRecover), limbAnimators[0].GetCurrentAnimatorStateInfo(0).length);
                break;

            case FodderStatus.Enraging:
                statusScript.Invoke(nameof(statusScript.StartEnraging), limbAnimators[0].GetCurrentAnimatorStateInfo(0).length);
                break;

            case FodderStatus.LegsBreaking:
                statusScript.Invoke(nameof(statusScript.BreakLegs), limbAnimators[0].GetCurrentAnimatorStateInfo(0).length);
                break;
        }

    }

    /// <summary>
    /// gets all animator components in children and updates local animator list with them
    /// </summary>
    void FetchLimbAnimators()
    {
        GetComponentsInChildren<Animator>(limbAnimators);
    }



}
