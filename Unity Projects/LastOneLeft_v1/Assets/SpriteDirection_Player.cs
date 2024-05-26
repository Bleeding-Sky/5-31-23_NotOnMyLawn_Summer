using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDirection_Player : MonoBehaviour
{
    public Animator walkingAnim;
    public States_Player playerStates;
    public AudioClip importantSound;
    public AudioSource importantSource;
    public GameObject handInv;

    private void Start()
    {
        importantSource.clip = importantSound;
        importantSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateWalkingAnimations();
        
    }

    public void UpdateWalkingAnimations()
    {
        HandInventory_Player handInfo = handInv.GetComponent<HandInventory_Player>();
        
        
        if (playerStates.isWalking == true)
        {
            walkingAnim.SetBool("isWalking", true);
           
            if(handInfo.itemInHand)
            {
                InteractionIdentification_Item handInvInfo = handInfo.objectInHand.GetComponent<InteractionIdentification_Item>();

                if(handInvInfo.isGun)
                {
                    importantSource.UnPause();
                }

            }
        }
        else
        {
            walkingAnim.SetBool("isWalking", false);
            importantSource.Pause();
        }

        if (playerStates.inputDirection == 1)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (playerStates.inputDirection == -1)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
