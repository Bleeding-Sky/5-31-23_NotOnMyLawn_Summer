using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDebugger : MonoBehaviour
{

    [SerializeField] Status_Zombie statusScript;

    #region Limb Conditions

    [ContextMenu("Change Head Condition: Intact")]
    void ChangeHeadToIntact()
    {
        statusScript.ChangeLimbCondition(FodderLimb.Head, LimbCondition.Intact);
    }

    [ContextMenu("Change Head Condition: Broken")]
    void ChangeHeadToBroken()
    {
        statusScript.ChangeLimbCondition(FodderLimb.Head, LimbCondition.Broken);
    }

    [ContextMenu("Change Left Arm Condition: Intact")]
    void ChangeLArmToIntact()
    {
        statusScript.ChangeLimbCondition(FodderLimb.LArm, LimbCondition.Intact);
    }

    [ContextMenu("Change Left Arm Condition: Broken")]
    void ChangeLArmToBroken()
    {
        statusScript.ChangeLimbCondition(FodderLimb.LArm, LimbCondition.Broken);
    }

    [ContextMenu("Change Right Arm Condition: Intact")]
    void ChangeRArmToIntact()
    {
        statusScript.ChangeLimbCondition(FodderLimb.RArm, LimbCondition.Intact);
    }

    [ContextMenu("Change Right Arm Condition: Broken")]
    void ChangeRArmToBroken()
    {
        statusScript.ChangeLimbCondition(FodderLimb.RArm, LimbCondition.Broken);
    }

    [ContextMenu("Change Body Condition: Intact")]
    void ChangeBodyToIntact()
    {
        statusScript.ChangeLimbCondition(FodderLimb.Body, LimbCondition.Intact);
    }

    [ContextMenu("Change Body Condition: Broken")]
    void ChangeBodyToBroken()
    {
        statusScript.ChangeLimbCondition(FodderLimb.Body, LimbCondition.Broken);
    }

    [ContextMenu("Change Legs Condition: Intact")]
    void ChangeLegsToIntact()
    {
        statusScript.ChangeLimbCondition(FodderLimb.Legs, LimbCondition.Intact);
    }

    [ContextMenu("Change Legs Condition: Broken")]
    void ChangeLegsToBroken()
    {
        statusScript.ChangeLimbCondition(FodderLimb.Legs, LimbCondition.Broken);
    }

    #endregion

    #region Statuses
    [ContextMenu("Become Idle")]
    void Debug_BecomeIdle()
    {
        statusScript.BecomeIdle();
    }

    [ContextMenu("Do Stumble")]
    void Debug_DoStumble()
    {
        statusScript.DoStumble();
    }

    [ContextMenu("Do Stun")]
    void Debug_DoStun()
    {
        statusScript.DoStun();
    }

    [ContextMenu("Fall Forward")]
    void Debug_FallForward()
    {
        statusScript.FallForward();
    }

    [ContextMenu("Fallen Face Down")]
    void Debug_FallenFaceDown()
    {
        statusScript.StartFallenFaceDownStatus();
    }

    [ContextMenu("Push Up Recover")]
    void Debug_PushUpRecover()
    {
        statusScript.StartPushUpRecover();
    }

    [ContextMenu("Fall Backward")]
    void Debug_FallBackward()
    {
        statusScript.FallBackward();
    }

    [ContextMenu("Fallen Face Up")]
    void Debug_FallenFaceUp()
    {
        statusScript.StartFallenFaceUpStatus();
    }

    [ContextMenu("Sit Up Recover")]
    void Debug_SitUpRecover()
    {
        statusScript.SitUpRecover();
    }

    [ContextMenu("Enraging")]
    void Debug_Enraging()
    {
        statusScript.StartEnraging();
    }

    [ContextMenu("Enraged")]
    void Debug_Enraged()
    {
        statusScript.BecomeEnraged();
    }

    [ContextMenu("Crawl (Doesnt break legs)")]
    void Debug_BreakLegs()
    {
        statusScript.BreakLegs();
    }

    #endregion
}
