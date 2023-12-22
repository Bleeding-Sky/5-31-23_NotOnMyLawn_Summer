using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_BoxColorChange : MonoBehaviour
{

    public TEMP_InteractableStates stateHolder;
    public SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stateHolder.isActivated && stateHolder.isInteractable)
        {
            mySpriteRenderer.color = Color.green;
            stateHolder.isInteractable = false;
        }
    }
}
