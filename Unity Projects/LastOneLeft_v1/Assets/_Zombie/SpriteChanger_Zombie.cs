using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(SpriteRenderer),
    typeof(HitboxChanger_Zombie)
    )]

/// <summary>
/// placed on all views of a zombie. changes it's sprite and its hitboxes
/// </summary>
public class SpriteChanger_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    //used for implementing different handling of sprites depending on view
    [SerializeField] ViewEnum view;

    [Header("DEBUG")]
    [SerializeField] SpriteRenderer mySpriteRenderer;
    [SerializeField] HitboxChanger_Zombie hitboxChangerScript;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        hitboxChangerScript = GetComponent<HitboxChanger_Zombie>();
    }

    /// <summary>
    /// parses a sprite list for the correct sprite and then applies it if found
    /// </summary>
    /// <param name="sprites"></param>
    /// <param name="isHeadless"></param>
    /// <param name="isOneArm"></param>
    /// <param name="isArmless"></param>
    /// <param name="isLegless"></param>
    public void ChangeSprite(List<SpriteData_Zombie> sprites, bool isHeadless, bool isOneArm, bool isArmless, bool isLegless)
    {
        
        foreach (SpriteData_Zombie spriteData in sprites)
        {
            bool correctHeadState = isHeadless == spriteData.headless;
            bool correctOneArmState = isOneArm == spriteData.oneArm;
            bool correctArmlessState = isArmless == spriteData.armless;
            bool correctLegState = isLegless == spriteData.legless;

            //apply sprite if it fits the parameters
            if (correctHeadState && correctOneArmState &&
                correctArmlessState && correctLegState)
            {
                mySpriteRenderer.sprite = spriteData.sprite;
            }
        }
        //TODO: make it consider its view and choose the sprite that matches the bools AND is in its view
        //cant do this yet because we dont have view specifric sprites

        //change hitboxes to match sprite
        hitboxChangerScript.ChangeHitbox(isHeadless, isLegless);
    }

}
