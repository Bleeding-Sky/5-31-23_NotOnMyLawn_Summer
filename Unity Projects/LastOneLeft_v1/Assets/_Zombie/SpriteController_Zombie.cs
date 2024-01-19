using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// changes zombie sprite
/// </summary>
public class SpriteController_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] SpriteList_Zombie spriteListSO;

    [Header("DEBUG")]
    [SerializeField] List<SpriteData_Zombie> sprites;
    public SpriteRenderer[] childrenSpriteRenderers;

    private void Awake()
    {
        sprites = spriteListSO.zombieSpriteList;
    }


    /// <summary>
    /// refreshes the sprite renderer list
    /// </summary>
    public void fetchSpriteRenderers()
    {
        childrenSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// changes zombie sprite based on limb status
    /// </summary>
    /// <param name="isHeadless"></param>
    /// <param name="isOneArm"></param>
    /// <param name="isArmless"></param>
    /// <param name="isLegless"></param>
    public void changeSprite(bool isHeadless, bool isOneArm, bool isArmless, bool isLegless)
    {

        foreach (SpriteData_Zombie spriteData in sprites)
        {
            bool correctHeadState = isHeadless == spriteData.headless;
            bool correctOneArmState = isOneArm == spriteData.oneArm;
            bool correctArmlessState = isArmless == spriteData.armless;
            bool correctLegState = isLegless == spriteData.legless;

            if (correctHeadState && correctOneArmState &&
                correctArmlessState && correctLegState)
            {
                foreach (SpriteRenderer renderer in childrenSpriteRenderers)
                renderer.sprite = spriteData.sprite;
            }
        }
    }
    
}
