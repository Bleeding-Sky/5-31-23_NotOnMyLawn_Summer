using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Interaction : MonoBehaviour
{
    public GameObject player;
    public GameObject hand;
    private GameObject itemNearBy;

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
                itemsInField.Remove(itemsInField[closestElement]);
                PickUpItem();
            }
        }
    }

    public void PickUpItem()
    {
        Revolver_GunScript GunScript = closetItem.GetComponent<Revolver_GunScript>();
        BoxCollider2D gunCollider = closetItem.GetComponent<BoxCollider2D>();
        GunScript.pickedUp = true;
        GunScript.player = player;
        GunScript.GunLocation.transform.parent = hand.transform;
        gunCollider.enabled = false;
        closetItem = null;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            itemsInField.Add(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            itemInRange = true;           
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Gun"))
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
}
