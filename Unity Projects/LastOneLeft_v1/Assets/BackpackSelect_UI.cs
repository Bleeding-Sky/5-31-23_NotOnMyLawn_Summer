using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackpackSelect_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{
    public Backpack_UI backpack;
    public List<ItemSlot_UI> radialSlots;
    public GameObject radialSlot;
    public ItemSlot_UI currentHoveredSlot;

    public List<GameObject> sections;
    public GameObject sectionParent;
    public GameObject centerPoint;
    public bool hoveringRadial;

    public float rotZ;
    void Start()
    {
        //Used to automaticaly add all the sections into the list
        foreach(Transform section in sectionParent.transform)
        {
            sections.Add(section.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Used to determine whether the user is currently hovering over the 
        //radial menu and if it isn't then the section will be disabled
        if(hoveringRadial)
        {
            DetermineDirection();
            DetermineSection();
        }
        else
        {
            GameObject temp = new GameObject();
            DisableSection(temp);
            Destroy(temp);
        }
    }


    #region Section Highlighting
    /// <summary>
    /// Determines where the mouse is relative to the center of the
    /// menu so that it can highlight the appropriate section
    /// </summary>
    private void DetermineDirection()
    {
        //Determines the direction from the center of the radial menu
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - Camera.main.ScreenToWorldPoint(centerPoint.transform.position);
        rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotZ = rotZ + 180;
    }
    

    private void DetermineSection()
    {
        //Highlights the right section depending on the angle of the mouse to menu
        if(rotZ <= 225 && rotZ >= 135)
        {
            sections[5].SetActive(true);
            DisableSection(sections[5]);
            currentHoveredSlot = null;
        }
        else if (rotZ <= 135 && rotZ >= 90)
        {
            sections[4].SetActive(true);
            DisableSection(sections[4]);
            currentHoveredSlot = radialSlots[4];
        }
        else if (rotZ <= 90 && rotZ >= 45)
        {
            sections[3].SetActive(true);
            DisableSection(sections[3]);
            currentHoveredSlot = radialSlots[3];
        }
        else if (rotZ <= 255 && rotZ >= 225)
        {
            sections[2].SetActive(true);
            DisableSection(sections[2]);
            currentHoveredSlot = radialSlots[2];
        }
        else if (rotZ <= 285 && rotZ >= 255)
        {
            sections[1].SetActive(true);
            DisableSection(sections[1]);
            currentHoveredSlot = radialSlots[1];
        }
        else if (rotZ <= 315 && rotZ >= 285)
        {
            sections[0].SetActive(true);
            DisableSection(sections[0]);
            currentHoveredSlot = radialSlots[0];
        }
        else if (rotZ > 315 || rotZ < 45)
        {
            sections[6].SetActive(true);
            DisableSection(sections[6]);
            currentHoveredSlot = null;
        }
    }

    /// <summary>
    /// Disables the appropriate section depending on if the player
    /// is hovering 1 or 0 menu items
    /// </summary>
    /// <param name="selectedSection"></param>
    private void DisableSection(GameObject selectedSection)
    {
        int i = 0;
        foreach(GameObject section in sections)
        {
            if(section != selectedSection)
            {
                section.SetActive(false);
            }
            i += 1;
        }

        if(i < sections.Count)
        {
            currentHoveredSlot = null;
        }


    }
    #endregion
    #region Radial Slot Management

    /// <summary>
    /// Adds the appropriate slots to the list of of radial slots
    /// from the parent object
    /// </summary>
    public void AddSlots()
    {
        int i = 0;
        //Get the radial slots from the game object parent
        foreach (Transform child in radialSlot.transform)
        {
            ItemSlot_UI childInfo = child.GetComponent<ItemSlot_UI>();
            childInfo.isRadialSlot = true;
            childInfo.slotID = i;
            radialSlots.Add(childInfo);
            i += 1;
        }
    }

    /// <summary>
    /// Copies the slots from the priority slots
    /// Into the radial slots
    /// </summary>
    public void CopyRadialSlotInfo()
    {
        for (int i = 0; i < backpack.quickAccessAmount; i++)
        {
            backpack.CopySlots(backpack.slots[i], radialSlots[i],true);
        }
    }
    #endregion
    #region Radial Menu Events
    /// <summary>
    /// Shows that the player is hovering over a slot
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveringRadial = true;
    }

    /// <summary>
    /// Shows that the player is not hovering over a slot
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        hoveringRadial= false;
    }

    /// <summary>
    /// Uses the Item from the slot script
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        if(currentHoveredSlot != null)
        {
            currentHoveredSlot.UseItem();
        }
    }
    #endregion
}
