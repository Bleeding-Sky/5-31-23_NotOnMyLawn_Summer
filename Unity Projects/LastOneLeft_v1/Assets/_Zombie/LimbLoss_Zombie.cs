using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(  typeof(Status_Zombie), 
                    typeof(Health_Zombie), 
                    typeof(SpriteController_Zombie))]

public class LimbLoss_Zombie : MonoBehaviour
{

    [Header("CONFIG")]
    //chance out of 100 that the limb will break when it takes damage at 0 limb health
    [SerializeField] float armLossChance = 20;
    [SerializeField] float bothArmsLossChance = 50;
    [SerializeField] float legBreakChance = 60;
    [SerializeField] float headBreakChance = 50;
    [SerializeField] float highHealthHeadBreakEndureChance = 70;
    [SerializeField] float lowHealthHeadBreakEndureChance = 50;

    [Header("DEBUG")]
    [SerializeField] Status_Zombie statusScript;
    [SerializeField] Health_Zombie healthScript;
    [SerializeField] SpriteController_Zombie spriteController;
    [SerializeField] bool headless = false;
    public bool oneArmBroken = false;
    public bool armless = false;
    public bool legless = false;

    private void Awake()
    {
        statusScript = GetComponent<Status_Zombie>();
        healthScript = GetComponent<Health_Zombie>();
        spriteController = GetComponent<SpriteController_Zombie>();
    }

    private void Start()
    {
        ChangeSprite();
    }


    #region Attempt Part Break Methods

    /// <summary>
    /// rolls to break the zombie's head if its head health is at 0
    /// </summary>
    /// <param name="maxHealth"></param>
    /// <param name="currentHealth"></param>
    public void AttemptHeadBreak(float maxHealth, float currentHealth)
    {
        if (RNGRolls_System.RollUnder(headBreakChance))
        {
            BreakHead(maxHealth, currentHealth);
        }

    }

    /// <summary>
    /// rolls to break a zombie's arm on torso damage
    /// </summary>
    /// <param name="bodyHealth"></param>
    public void AttemptArmLoss(float bodyHealth)
    {
        float breakChance;
        if (bodyHealth > 0)
        {
            breakChance = armLossChance;
        }
        else
        {
            breakChance = bothArmsLossChance;
        }

        if (RNGRolls_System.RollUnder(breakChance))
        {
            LoseArm();
        }
    }

    /// <summary>
    /// rolls to break zombie's legs if its legs health is at 0
    /// </summary>
    public void AttemptLegBreak()
    {
        if (RNGRolls_System.RollUnder(legBreakChance))
        {
            BreakLegs();
        }
    }
    #endregion

    #region Break Part Methods

    public void BreakHead(float maxHealth, float currentHealth)
    {
        headless = true;
        AttemptEndureHeadBreak(maxHealth, currentHealth);

        ChangeSprite();
    }

    private void AttemptEndureHeadBreak(float maxHealth, float currentHealth)
    {
        bool underHalfHealth = currentHealth < (maxHealth / 2);
        float endureChance = 0;
        if (underHalfHealth)
        {
            endureChance = lowHealthHeadBreakEndureChance;
        }
        else
        {
            endureChance = highHealthHeadBreakEndureChance;
        }

        //roll for surviving the head break
        //on failure, zombie dies
        if (!RNGRolls_System.RollUnder(endureChance))
        {
            healthScript.KillZmb();
        }
    }

    public void LoseArm()
    {
        //break one arm if no arms broken, break other arm if one arm broken
        if (!armless)
        {
            if (oneArmBroken)
            {
                armless = true;
                oneArmBroken = false;
            }
            else if (!oneArmBroken)
            {
                oneArmBroken = true;
            }
        }

        ChangeSprite();
    }

    public void BreakLegs()
    {
        legless = true;
        statusScript.DoCrawl();

        ChangeSprite();
    }
    #endregion

    /// <summary>
    /// calls method on a spritecontroller to change sprite according to limb status booleans
    /// </summary>
    public void ChangeSprite()
    {
        spriteController.ActivateSpriteChangers(headless, oneArmBroken, armless, legless);
    }

}
