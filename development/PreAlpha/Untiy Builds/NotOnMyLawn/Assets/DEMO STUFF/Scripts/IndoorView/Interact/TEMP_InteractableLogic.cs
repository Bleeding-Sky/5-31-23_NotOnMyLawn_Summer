using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles interactable prompt and interaction
/// </summary>
public class TEMP_InteractableLogic : MonoBehaviour
{
    [Header("CONFIG")]
    public float promptHeightAboveObject = 1.5f;
    public GameObject promptPrefab;

    [Header("DEBUG")]
    public TEMP_InteractableStates stateHolder;
    public GameObject myPrompt;

    // Update is called once per frame
    void Update()
    {
        HandlePrompt();

        bool ableToActivate = stateHolder.isInteractable && stateHolder.isInRange;


        if (Input.GetKeyDown(KeyCode.F) &&
            ableToActivate)
        {
            //if toggleable, toggle active status
            if (stateHolder.isToggleable)
            {
                stateHolder.isActivated = !stateHolder.isActivated;
            }
            //if not toggleable, turn it on
            else
            {
                stateHolder.isActivated = true;
            }
            
        }

    }

    private void HandlePrompt()
    {
        //create prompt when in range
        if (stateHolder.isInteractable &&
            stateHolder.isInRange && !stateHolder.promptActive)
        {
            //create prompt
            Vector3 promptPosition = new Vector3(transform.position.x,
                                                transform.position.y + promptHeightAboveObject,
                                                transform.position.z);

            myPrompt = Instantiate(promptPrefab, promptPosition, Quaternion.identity);

            stateHolder.promptActive = true;
        }

        //get rid of prompt when out of range or object is no longer interactable
        if ( (!stateHolder.isInRange || !stateHolder.isInteractable) && 
            stateHolder.promptActive)
        {
            Destroy(myPrompt);

            stateHolder.promptActive = false;
        }
    }
}
