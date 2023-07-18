using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Interaction : MonoBehaviour
{

    [Header("DEBUG")]
    public List<GameObject> itemsInField;
    public bool itemInRange;
    public Vector3 playerPosition;
    public GameObject Inventory;
    public GameObject closetItem;
    public float lastItem;
    public int itemLength;
    public int closestElement;


    //establishes that there is no item to begin with
    void Start()
    {
        itemInRange = false;

    }
    // Update is called once per frame
    void Update()
    {
        //keeps track of the player position and an interactable is in range it finds the closest interactable and identifies if its an item or enviornment
       playerPosition = transform.position;
       if(itemInRange)
        {
            FindClosestItem();
            InteractableIdentification();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //once the interactable is in range it adds it to the list
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            itemsInField.Add(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if a collision is an interactable then it sets in range to true
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            itemInRange = true;           
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if an item leaves it sets the in range to false and removes objects from the list
        if(collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            itemInRange = false;
            itemsInField.Remove(collision.gameObject);
        }    
    }

    //A function to find the closest interactable
    public void FindClosestItem()
    {   
        //gets the list length and sets what the last item is to infinity inorder to ensure no item is bigger than the initial closest item
        itemLength = itemsInField.Count;
        lastItem = Mathf.Infinity;

        //goes through each interactable and determines the closest interactable
        for (int i = 0; i < itemLength; i++)
        {
            //triangulates the actual closest distance using pythag theorum
            Vector3 distanceFromPlayer = itemsInField[i].transform.position - playerPosition;
            float distance;
            distance = CalculateDistance(distanceFromPlayer);

            //if the current distance is less then the last item then it is the closest
            if(distance < lastItem)
            {
                closetItem = itemsInField[i];
                closestElement = i;
                lastItem = distance;
            }
        }
        lastItem = Mathf.Infinity;
    }

    //calculates the distance of one object from another
    public float CalculateDistance(Vector3 distance)
    {
        float pythagDistance;
        pythagDistance = Mathf.Sqrt((distance.x * distance.x) + (distance.y * distance.y));

        return pythagDistance;
    }

    //once the closest item is found this function determines whether the interactable is item or enviornment
    public void InteractableIdentification()
    {
        Interaction_Identification Interactable = closetItem.GetComponent<Interaction_Identification>();
        //if the interaction is an item it puts it in the inventory
        if(Interactable.isItem && !Interactable.isEnviormentObject)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Item_PickedUp itemAssignment = GetComponent<Item_PickedUp>();
                Item_Interaction ItemInteraction = GetComponent<Item_Interaction>();
                Inventory_Script inventoryStorage = Inventory.GetComponent<Inventory_Script>();

                inventoryStorage.itemAssignment = itemAssignment;
                inventoryStorage.ItemInteractionScript = ItemInteraction;
                inventoryStorage.StoreItems(closetItem);
            }
        }
        //if the interaction is an envionrmental object it interacts with it
        else if(!Interactable.isItem && Interactable.isEnviormentObject)
        {
            Enviorment_Interaction enviornment = GetComponent<Enviorment_Interaction>();
            enviornment.Interact(closetItem);
        }
    }
}
