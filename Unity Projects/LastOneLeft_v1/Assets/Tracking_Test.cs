using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking_Test : MonoBehaviour
{
    
    public RoomTracking_Player player;
    public GameObject ZombieCurrentRoom;
    public GameObject doorGoal;

    public bool findPlayer;
    public float moveSpeed;

    public List<List<GameObject>> routes;
    public List<GameObject> quickestRoute;
    // Start is called before the first frame update
    void Start()
    {
        findPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            foreach(GameObject room in player.Rooms)
            {
                Room_Environment roomTracker = room.GetComponent<Room_Environment>();
                roomTracker.EnemyTracking(gameObject);
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(player.CurrentRoom != ZombieCurrentRoom)
            {
                FindDoor();
            }
        }

        if(findPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorGoal.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Door" && findPlayer && collision.gameObject == doorGoal)
        {
            GameObject door = doorGoal;
            BackgroundDoor_Environment doorInfo = door.GetComponent<BackgroundDoor_Environment>();
            if (door == doorInfo.door1)
            {
                transform.position = doorInfo.door2.transform.position;
            }
            else if (door == doorInfo.door2)
            {
                BackgroundDoor_Environment door1Info = doorInfo.door1.GetComponent<BackgroundDoor_Environment>();
                transform.position = doorInfo.door1.transform.position;
            }
        }
    }

    private void FindDoor()
    {
        Room_Environment roomInfo = ZombieCurrentRoom.GetComponent<Room_Environment>();
        routes = new List<List<GameObject>>();

        foreach(GameObject door in roomInfo.Doors)
        {
            BackgroundDoor_Environment doorInfo = door.GetComponent<BackgroundDoor_Environment>();
            List<GameObject> RoomsChecked = new List<GameObject>();

            RoomsChecked.Add(ZombieCurrentRoom);
            if(door == doorInfo.door1)
            {
                BackgroundDoor_Environment door2Info = doorInfo.door2.GetComponent<BackgroundDoor_Environment>();
                findShortestRoute(doorInfo.door2, RoomsChecked);
            }
            else if(door == doorInfo.door2)
            {
                BackgroundDoor_Environment door1Info = doorInfo.door1.GetComponent<BackgroundDoor_Environment>();
                findShortestRoute(doorInfo.door1, RoomsChecked);
            }
        }
        Debug.Log("Checked");

        calculateQuickestRoute();
        findTargetDoor();
        findPlayer = true;
    }

    private void findShortestRoute(GameObject door, List<GameObject> RoomsChecked)
    {
        
        BackgroundDoor_Environment doorInfo = door.GetComponent<BackgroundDoor_Environment>();
        GameObject currentRoomChecked = doorInfo.inRoom;
        Debug.Log("Checking " + currentRoomChecked.name);
        Debug.Log("went through room door" + door.name);
        bool roomLooped = false;
        foreach(GameObject room in RoomsChecked)
        {
            if(room == currentRoomChecked)
            {
                roomLooped = true;
                Debug.Log("room looped");
            }
        }
        RoomsChecked.Add(currentRoomChecked);

        if (player.CurrentRoom != currentRoomChecked)
        {
            Room_Environment roomInfo = currentRoomChecked.GetComponent<Room_Environment>();
            foreach(GameObject checkDoor in roomInfo.Doors)
            {
                Debug.Log("Checking door" + checkDoor.name);
                if (checkDoor != door && roomLooped == false)
                {
                    BackgroundDoor_Environment checkDoorInfo = checkDoor.GetComponent<BackgroundDoor_Environment>();
                    
                    if (checkDoor == checkDoorInfo.door1)
                    {
                        BackgroundDoor_Environment checkDoor2Info = checkDoorInfo.door2.GetComponent<BackgroundDoor_Environment>();
                        findShortestRoute(checkDoorInfo.door2, RoomsChecked);
                    }
                    else if (checkDoor == checkDoorInfo.door2)
                    {
                        BackgroundDoor_Environment checkDoor1Info = checkDoorInfo.door1.GetComponent<BackgroundDoor_Environment>();
                        findShortestRoute(checkDoorInfo.door1, RoomsChecked);
                    }
                }
            }
        }
        else
        {
            routes.Add(RoomsChecked);
            Debug.Log("Player Found");
        }
        
    }
    
    public void calculateQuickestRoute()
    {
        int i = 0;
        int j = 0;
        int shortestRoute = 1000;
        foreach (List<GameObject> list in routes)
        {
            Debug.Log(list.Count);
            if (shortestRoute > list.Count)
            {
                shortestRoute = list.Count;
                j = i;
            }
            i += 1;
        }

        foreach (GameObject room in routes[j])
        {
            Debug.Log(room.name);
        }
        quickestRoute = routes[j];
    }

    public void findTargetDoor()
    {
        GameObject goalRoom = quickestRoute[1];
        Room_Environment roomInfo = ZombieCurrentRoom.GetComponent<Room_Environment>();

        foreach(GameObject door in roomInfo.Doors)
        {
            BackgroundDoor_Environment doorInfo = door.GetComponent<BackgroundDoor_Environment>();
            if (door == doorInfo.door1)
            {
                BackgroundDoor_Environment door2Info = doorInfo.door2.GetComponent<BackgroundDoor_Environment>();
                if(door2Info.inRoom == goalRoom)
                {
                    doorGoal = door;
                }
            }
            else if (door == doorInfo.door2)
            {
                BackgroundDoor_Environment door1Info = doorInfo.door1.GetComponent<BackgroundDoor_Environment>();
                if (door1Info.inRoom == goalRoom)
                {
                    doorGoal = door;
                }
            }
        }
    }
}

