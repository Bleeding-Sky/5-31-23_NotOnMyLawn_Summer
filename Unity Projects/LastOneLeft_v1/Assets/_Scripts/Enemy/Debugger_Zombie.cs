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
    [SerializeField] float bulletDamage;
    [SerializeField] float bulletPenetration;
    [SerializeField] float bulletStatusMultiplier = 1;
    [SerializeField] float bulletCritDamageMultiplier = 2;
    [SerializeField] float bulletArmoredDamageMultiplier = 1;
    [SerializeField] float bulletWeakDamageMultiplier = 0.5f;

    #region Damage
    [ContextMenu("True Damage")]
    void TakeDamage()
    {
        health.DamageHealth(damage);
    }

    [ContextMenu("Bullet Damage (Crit)")]
    void CritDamage()
    {
        health.DamageCrit(bulletDamage, bulletCritDamageMultiplier, bulletStatusMultiplier);
    }

    [ContextMenu("Bullet Damage (Armored)")]
    void ArmoredDamage()
    {
        health.DamageArmored(damage, bulletArmoredDamageMultiplier, bulletStatusMultiplier);
    }

    [ContextMenu("Bullet Damage (Weak)")]
    void WeakDamage()
    {
        health.DamageWeak(damage, bulletWeakDamageMultiplier, bulletStatusMultiplier);
    }

    [ContextMenu("Die")]
    void Die()
    {
        health.Die();
    }
    #endregion

    #region Statuses
    [ContextMenu("Attempt Stumble")]
    void AttemptStumble()
    {
        status.AttemptStumble(bulletStatusMultiplier);
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
        status.AttemptStun(DmgRegionEnum.Crit, bulletStatusMultiplier);
    }

    [ContextMenu("Attempt Stun (Body Damage)")]
    void AttemptStunBody()
    {
        status.AttemptStun(DmgRegionEnum.Armored, bulletStatusMultiplier);
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
        status.AttemptFallForward(bulletStatusMultiplier);
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
        status.AttemptFallBackward(DmgRegionEnum.Crit, bulletStatusMultiplier);
    }

    [ContextMenu("Attempt Fall Backward (Body Damage)")]
    void AttemptFallBackwardBody()
    {
        status.AttemptFallBackward(DmgRegionEnum.Armored, bulletStatusMultiplier);
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

    /* NOT NECESSARY ANYMORE BECAUSE LIMBS BREAK WHEN AT 0 HEALTH
    [ContextMenu("Attempt Head Break")]
    void AttemptHeadBreak()
    {
        limbloss.AttemptCritBreak(health.maxHealth, health.currentHealth);
    }

    [ContextMenu("Attempt Arm Break")]
    void AttemptArmBreak()
    {
        limbloss.AttemptArmLoss(health.armoredRegionHealth);
    }

    [ContextMenu("Attempt Leg Break")]
    void AttemptLegBreak()
    {
        limbloss.AttemptLegBreak();
    }
    */

    [ContextMenu("Break Head")]
    void BreakHead()
    {
        limbloss.BreakCritRegion(health.maxHealth, health.currentHealth);
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
