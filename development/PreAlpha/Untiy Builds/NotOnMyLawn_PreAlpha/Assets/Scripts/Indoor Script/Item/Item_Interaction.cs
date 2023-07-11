using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Interaction : MonoBehaviour
{
    public GameObject player;
    public GameObject hand;
    private GameObject itemNearBy;
    public Transform armRotationPos;
    public Transform handPos;
    public GameObject Inventory;

    public bool itemInRange;

    public List<GameObject> itemsInField;
    public Vector3 playerPosition;

    public GameObject closetItem;
    public float lastItem;
    public int itemLength;
    public int closestElement;


    // Start is called before the first frame update
    void Start()
    {
        itemInRange = false;

    }
    // Update is called once per frame
    void Update()
    {
       playerPosition = transform.position;
       if(itemInRange)
        {
            FindClosestItem();
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractableIdentification();
            }
        }
    }

    public void PickUpItem()
    {
        Revolver_GunScript GunScript = closetItem.GetComponent<Revolver_GunScript>();
        BoxCollider2D gunCollider = closetItem.GetComponent<BoxCollider2D>();
        GunScript.pickedUp = true;
        GunScript.player = player;
        GunScript.gameObject.transform.parent = hand.transform;
        GunScript.GunRotation = armRotationPos;
        GunScript.handPosition = handPos;
        gunCollider.enabled = false;
        closetItem = null;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            itemsInField.Add(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            itemInRange = true;           
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            itemInRange = false;
            itemsInField.Remove(collision.gameObject);
        }    
    }
    public void FindClosestItem()
    {   
        itemLength = itemsInField.Count;
        lastItem = Mathf.Infinity;
        for (int i = 0; i < itemLength; i++)
        {
            Vector3 distanceFromPlayer = itemsInField[i].transform.position - playerPosition;
            float distance;
            distance = calculateDistance(distanceFromPlayer);

            if(distance < lastItem)
            {
                closetItem = itemsInField[i];
                closestElement = i;
                lastItem = distance;
            }
        }
        lastItem = Mathf.Infinity;
    }
    public float calculateDistance(Vector3 distance)
    {
        float pythagDistance;
        pythagDistance = Mathf.Sqrt((distance.x * distance.x) + (distance.y * distance.y));

        return pythagDistance;
    }

    public void InteractableIdentification()
    {
        Interaction_Identification Interactable = closetItem.GetComponent<Interaction_Identification>();
        if(Interactable.isItem && !Interactable.isEnviormentObject)
        {
            Item_Interaction ItemInteraction = GetComponent<Item_Interaction>();
            Inventory_Script inventoryStorage = Inventory.GetComponent<Inventory_Script>();
            inventoryStorage.ItemInteractionScript = ItemInteraction;
            inventoryStorage.storeItems(closetItem);
            

        }
        else if(!Interactable.isItem && Interactable.isEnviormentObject)
        {
            Enviorment_Interaction window = closetItem.GetComponent<Enviorment_Interaction>();

            window.Interact();
        }
    }
}
