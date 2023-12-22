using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Script : MonoBehaviour
{
    public Transform RoomPoint;
    public Transform RoomPointA;
    public Transform RoomPointB;
    public Vector2 RoomSize;
    public LayerMask PlayerMask;
    public GameObject mapCounterPart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerTracking();
    }

    public void PlayerTracking()
    {
        Collider2D[] PlayersInRoom = Physics2D.OverlapAreaAll(RoomPointA.position,RoomPointB.position);

        foreach (Collider2D player in PlayersInRoom)
        {
            if (player.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Room_Tracking tracking = gameObject.transform.parent.GetComponent<Room_Tracking>();
                tracking.OccupiedRoom = gameObject;
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (RoomPoint == null)
            return;
        Vector2 center = CenterOfRectangle();
        Vector2 area = AreaOfRectangle();
        Gizmos.DrawWireCube(center, area);
    }

    private Vector2 CenterOfRectangle()
    {
        float width = RoomPointB.position.x - RoomPointA.position.x;
        float height = RoomPointA.position.y - RoomPointB.position.y;

        Vector2 centerPoint = new Vector2((RoomPointA.position.x + (width / 2)), (RoomPointB.position.y + (height / 2)));

        return centerPoint;
    }

    private Vector2 AreaOfRectangle()
    {
        float width = RoomPointB.position.x - RoomPointA.position.x;
        float height = RoomPointA.position.y - RoomPointB.position.y;

        Vector2 area = new Vector2(width, height);

        return area;
    }
}
