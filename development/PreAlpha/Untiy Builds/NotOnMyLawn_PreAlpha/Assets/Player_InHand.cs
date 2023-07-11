using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InHand : MonoBehaviour
{
    public GameObject objectInHand;
    // Update is called once per frame
    void Update()
    {

    }
    public void PlaceObjectInHand(GameObject iteminHand)
    {
        objectInHand = iteminHand;
        objectInHand.SetActive(true);
        objectInHand.transform.parent = gameObject.transform;
    }

    public void removeItemFromHand()
    {
        objectInHand.transform.SetAsLastSibling();
        objectInHand = null;
    }
}
