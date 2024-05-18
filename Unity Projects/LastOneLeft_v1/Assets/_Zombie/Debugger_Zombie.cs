using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class Debugger_Zombie : MonoBehaviour
{
    [SerializeField] Health_Zombie health;
    [SerializeField] Status_Zombie status;
    [SerializeField] LimbLoss_Zombie limbloss;
    

    [SerializeField] float damage;

    #region Damage
    [ContextMenu("True Damage")]
    void TakeDamage()
    {
        health.DamageHealth(damage);
    }

    [ContextMenu("Headshot")]
    void Headshot()
    {
        health.Headshot(damage);
    }

    [ContextMenu("Bodyshot")]
    void Bodyshot()
    {
        health.Bodyshot(damage);
    }

    [ContextMenu("Legshot")]
    void Legshot()
    {
        health.Legshot(damage);
    }

    [ContextMenu("Die")]
    void Die()
    {
        health.KillZmb();
    }
    #endregion

    #region Statuses
    [ContextMenu("Attempt Stumble")]
    void AttemptStumble()
    {
        status.AttemptStumble();
    }

    [ContextMenu("Do Stumble")]
    void DoStumble()
    {
        status.DoStumble();
    }

    [ContextMenu("Stop Stumble")]
    void StopStumble()
    {
        status.StopStumble();
    }

    [ContextMenu("Attempt Stun (Head Damage)")]
    void AttemptStunHead()
    {
        status.AttemptStun(DmgRegionEnum.Head);
    }

    [ContextMenu("Attempt Stun (Body Damage)")]
    void AttemptStunBody()
    {
        status.AttemptStun(DmgRegionEnum.Body);
    }

    [ContextMenu("Do Stun")]
    void DoStun()
    {
        status.DoStun();
    }

    [ContextMenu("Stop Stun")]
    void StopStun()
    {
        status.StopStun();
    }

    [ContextMenu("Attempt Fall Forward")]
    void AttemptFallForward()
    {
        status.AttemptFallForward();
    }

    [ContextMenu("Do Fall Forward")]
    void DoFallForward()
    {
        status.DoFallForward();
    }

    [ContextMenu("Stop Fall Forward")]
    void StopFallForward()
    {
        status.StopFallForward();
    }

    [ContextMenu("Attempt Fall Backward (Head Damage)")]
    void AttemptFallBackwardHead()
    {
        status.AttemptFallBackward(DmgRegionEnum.Head);
    }

    [ContextMenu("Attempt Fall Backward (Body Damage)")]
    void AttemptFallBackwardBody()
    {
        status.AttemptFallBackward(DmgRegionEnum.Body);
    }

    [ContextMenu("Do Fall Backward")]
    void DoFallBackward()
    {
        status.DoFallBackward();
    }

    [ContextMenu("Stop Fall Backward")]
    void StopFallBackward()
    {
        status.StopFallBackward();
    }

    [ContextMenu("Do Enrage")]
    void DoEnrage()
    {
        status.DoEnrage();
    }

    [ContextMenu("Stop Enrage")]
    void StopEnrage()
    {
        status.StopEnrage();
    }

    [ContextMenu("Do Crawl")]
    void DoCrawl()
    {
        status.DoCrawl();
    }

    [ContextMenu("Stop Crawl")]
    void StopCrawl()
    {
        status.StopCrawl();
    }

    #endregion

    #region Limb Breaks

    [ContextMenu("Attempt Head Break")]
    void AttemptHeadBreak()
    {
        limbloss.AttemptHeadBreak(health.maxHealth, health.currentHealth);
    }

    [ContextMenu("Attempt Arm Break")]
    void AttemptArmBreak()
    {
        limbloss.AttemptArmLoss(health.bodyHealth);
    }

    [ContextMenu("Attempt Leg Break")]
    void AttemptLegBreak()
    {
        limbloss.AttemptLegBreak();
    }

    [ContextMenu("Break Head")]
    void BreakHead()
    {
        limbloss.BreakHead(health.maxHealth, health.currentHealth);
    }

    [ContextMenu("Break Arm")]
    void BreakArm()
    {
        limbloss.LoseArm();
    }

    [ContextMenu("Break Legs")]
    void BreakLegs()
    {
        limbloss.BreakLegs();
    }

    #endregion

}
