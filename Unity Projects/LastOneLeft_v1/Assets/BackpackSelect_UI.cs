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
        foreach(Transform section in sectionParent.transform)
        {
            sections.Add(section.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hoveringRadial)
        {
            DetermineDirection();
            DetermineSection();
        }
        else
        {
            GameObject temp = new GameObject();
            DisableSection(temp);
        }
    }

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

    public void CopyRadialSlotInfo()
    {
        for (int i = 0; i < backpack.quickAccessAmount; i++)
        {
            backpack.CopySlots(backpack.slots[i], radialSlots[i],true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveringRadial = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveringRadial= false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        if(currentHoveredSlot != null)
        {
            currentHoveredSlot.UseItem();
        }
    }
}
