using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Tracking : MonoBehaviour
{
    public List<Transform> Rooms;
    public GameObject OccupiedRoom;
    // Start is called before the first frame update
    void Start()
    {
        List<Transform> children = GetChildren(transform);
        Rooms = children;
    }

    /// <summary>
    /// Gets the children of the room tracking object and 
    /// lists the rooms in the room list to be accessed 
    /// by the map and tracking scripts
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
     List<Transform> GetChildren(Transform parent)
    {
        List<Transform> children = new List<Transform>();

        foreach(Transform child in parent)
        {
            children.Add(child);
        }
        return children;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
