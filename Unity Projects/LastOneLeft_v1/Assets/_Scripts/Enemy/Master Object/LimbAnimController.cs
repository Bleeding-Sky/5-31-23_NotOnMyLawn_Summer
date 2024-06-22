using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status_Zombie))]

public class LimbAnimController : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] List<LimbAnimator> limbAnimators;
    [SerializeField] Status_Zombie statusScript;

    private void Awake()
    {
        statusScript = GetComponent<Status_Zombie>();
    }

    // Start is called before the first frame update
    void Start()
    {
        FetchLimbAnimators();
    }

    /// <summary>
    /// plays an animation on all child limb animators.
    /// if the state is a transition, invokes a delayed method to move to the next state.
    /// </summary>
    /// <param name="status"></param>
    public void ChangeAnimationState(FodderStatus status,
                                LimbCondition headCondition,
                                LimbCondition LArmCondition,
                                LimbCondition RArmCondition,
                                LimbCondition bodyCondition,
                                LimbCondition legsConsition
                              )
    {

        FetchLimbAnimators();
        
        //if this^ causes performance issues,move this to other scripts
        //and make it ONLY trigger when a new view of the enemy is created.

        //OCCASIONALLY this will cause an error due to missing references in the list.
        //i do not know why this happens. maybe fetch method taking too long? idk.
        foreach (LimbAnimator limbAnimator in limbAnimators)
        {
            limbAnimator.PlayAnimation(status, headCondition, LArmCondition, RArmCondition, bodyCondition, legsConsition);
        }

        //invokes a method that transitions to the next state.
        //the methods (on the status script) check for prerequisite states in order to transition
        switch(status)
        {
            case FodderStatus.FallingForward:
                statusScript.Invoke(nameof(statusScript.StartFallenFaceDownStatus), limbAnimators[0].myAnimator.GetCurrentAnimatorStateInfo(0).length);
                break;

            case FodderStatus.PushUpRecover:
                statusScript.Invoke(nameof(statusScript.BecomeIdle), limbAnimators[0].myAnimator.GetCurrentAnimatorStateInfo(0).length);
                break;

            case FodderStatus.FallingBackward:
                statusScript.Invoke(nameof(statusScript.StartFallenFaceUpStatus), limbAnimators[0].myAnimator.GetCurrentAnimatorStateInfo(0).length);
                break;

            case FodderStatus.SitUpRecover:
                statusScript.Invoke(nameof(statusScript.BecomeIdle), limbAnimators[0].myAnimator.GetCurrentAnimatorStateInfo(0).length);
                break;

            case FodderStatus.Enraging:
                statusScript.Invoke(nameof(statusScript.BecomeEnraged), limbAnimators[0].myAnimator.GetCurrentAnimatorStateInfo(0).length);
                break;

            case FodderStatus.LegsBreaking:
                statusScript.Invoke(nameof(statusScript.StartCrawl), limbAnimators[0].myAnimator.GetCurrentAnimatorStateInfo(0).length);
                break;
        }

    }

    /// <summary>
    /// gets all animator components in children and updates local animator list with them
    /// </summary>
    void FetchLimbAnimators()
    {
        limbAnimators.Clear();
        //Debug.Log("Fetching animators...");
        GetComponentsInChildren<LimbAnimator>(limbAnimators);
    }



}
