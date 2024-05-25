using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracking_Zombie : MonoBehaviour
{

    //Tracks the player's position and room location
    public RoomTracking_Player player;
    public PositionTracker_Player playerPos;

    //Keeps the current room the zombie is in and the goal it is trying to reach
    public GameObject ZombieCurrentRoom;
    public GameObject doorGoal;

    //variables that are self explanatory
    public bool findPlayer;
    public float moveSpeed;
    public bool enteredNewRoom;
    public bool searchForPlayer;

    //Holds the total possible routes to the player and the quickest route to the player
    public List<List<GameObject>> routes;
    public List<GameObject> quickestRoute;


    // Start is called before the first frame update
    void Start()
    {
        findPlayer = false;
        enteredNewRoom = true;
    }

    void Update()
    {
        //Always makes sure that the door that the zombie is trying to reach is collidable
        if (doorGoal != null)
        {
            Debug.Log("Enabling Colliders");
            Physics2D.IgnoreCollision(doorGoal.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        }

        //Updates when the player switches rooms
        if(enteredNewRoom)
        {
            EnteredNewRoom();
        }

        //Decides the enemy action depending on whether the player and enemy are in the same room
        if (player.CurrentRoom != ZombieCurrentRoom)
        {
            FindPlayer();
        }
        else if (player.CurrentRoom == ZombieCurrentRoom)
        {
            findPlayer = false;
        }
    }

    /// <summary>
    /// Updates the current room that the enemy is in
    /// </summary>
    private void EnteredNewRoom()
    {
        //When the zombie enters a new room then the data updates which room it is in
        if (enteredNewRoom)
        {
            foreach (GameObject room in player.Rooms)
            {
                Room_Environment roomTracker = room.GetComponent<Room_Environment>();
                roomTracker.EnemyTracking(gameObject);
                enteredNewRoom = false;
            }
        }
    }
    /// <summary>
    /// The main collision allows the enemy to go through a door while ignoring other collisions
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Zombie Reached Door");
        //Ensures 3 things that the collision is a door, the correct door, and the enemy is trying to go through it
        if (collision.gameObject.tag == "Door" && findPlayer && collision.gameObject == doorGoal)
        {
            
            GameObject door = doorGoal;
            BackgroundDoor_Environment doorInfo = door.GetComponent<BackgroundDoor_Environment>();
            InteractionIdentification_Item doorIdentity = door.GetComponent<InteractionIdentification_Item>(); 

            //finds the exit point of the door and exits out of it
            if(door == doorInfo.door1 && doorIdentity.isBackgroundDoor)
            {
                transform.position = new Vector3(doorInfo.door2.transform.position.x,0,0);
            }
            else if(door == doorInfo.door2 && doorIdentity.isBackgroundDoor)
            {
                transform.position = new Vector3(doorInfo.door1.transform.position.x, 0, 0);
            }
            else if(door == doorInfo.door1 && doorIdentity.isSideDoor)
            {
                transform.position = new Vector3(doorInfo.door2.transform.position.x - 2, 0, 0);
            }
            else if(door == doorInfo.door2 && doorIdentity.isSideDoor)
            {
                transform.position = new Vector3(doorInfo.door1.transform.position.x + 2, 0, 0);
            }


            //Confirms that the enemy entered a new room
            enteredNewRoom = true;
        }
        else if (collision.gameObject != doorGoal && collision.gameObject.tag != "Platform")
        {
            //Any collider that is not a platform or the door goal is ignored
            Debug.Log("Ignoring the collision");
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        }
    }

    /// <summary>
    /// Begins the look for the player by checking the doors in the room thus starting the routes
    /// </summary>
    public void FindPlayer()
    {
        //The room's information is provided
        Room_Environment roomInfo = ZombieCurrentRoom.GetComponent<Room_Environment>();

        //The route is restarted to create a new list of them
        routes = new List<List<GameObject>>();

        //Goes through all the doors in the room
        foreach (GameObject door in roomInfo.Doors)
        {
            //Gets the door's information and adds the room the zombie is currently in to the checked room list
            BackgroundDoor_Environment doorInfo = door.GetComponent<BackgroundDoor_Environment>();
            List<GameObject> RoomsChecked = new List<GameObject>();
            RoomsChecked.Add(ZombieCurrentRoom);

            //Finds the door's exit point and then checks that room for the player
            if (door == doorInfo.door1)
            {
                //Passes the door information and the rooms checked to map out the route
                BackgroundDoor_Environment door2Info = doorInfo.door2.GetComponent<BackgroundDoor_Environment>();
                findAvailableRoutes(doorInfo.door2, RoomsChecked);
            }
            else if (door == doorInfo.door2)
            {
                //Passes the door information and the rooms checked to map out the route
                BackgroundDoor_Environment door1Info = doorInfo.door1.GetComponent<BackgroundDoor_Environment>();
                findAvailableRoutes(doorInfo.door1, RoomsChecked);
            }
        }
        Debug.Log("Checked");

        //Takes the route and finds the quickest route and the door the enemy should go through
        calculateQuickestRoute();
        findTargetDoor();
        findPlayer = true;
    }

    /// <summary>
    /// Recursivley finds all the routes to the player while ignoring any that dont
    /// </summary>
    /// <param name="door"></param>
    /// <param name="RoomsChecked"></param>
    private void findAvailableRoutes(GameObject door, List<GameObject> RoomsChecked)
    {
        //Gets the door information passed from the FindPlayer function
        BackgroundDoor_Environment doorInfo = door.GetComponent<BackgroundDoor_Environment>();
        GameObject currentRoomChecked = doorInfo.inRoom;

        //Makes sure that a room that has already been checked isn't checked again
        bool roomLooped = false;
        foreach (GameObject room in RoomsChecked)
        {
            if (room == currentRoomChecked)
            {
                roomLooped = true;
            }
        }

        //adds the current room being check to the list
        RoomsChecked.Add(currentRoomChecked);

        //Conditional to make sure that this isnt the room the player is in
        if (player.CurrentRoom != currentRoomChecked)
        {
            Room_Environment roomInfo = currentRoomChecked.GetComponent<Room_Environment>();
            //checks all the doors in the current room 
            foreach (GameObject checkDoor in roomInfo.Doors)
            {
                //Checks to ensure the route hasnt looped and the current door being checked isnt the door that the enemy already came through
                if (checkDoor != door && roomLooped == false)
                {
                    BackgroundDoor_Environment checkDoorInfo = checkDoor.GetComponent<BackgroundDoor_Environment>();
                    //Goes through the door if it is a new route   
                    if (checkDoor == checkDoorInfo.door1)
                    {
                        BackgroundDoor_Environment checkDoor2Info = checkDoorInfo.door2.GetComponent<BackgroundDoor_Environment>();
                        //recursion to continue the search for the player
                        findAvailableRoutes(checkDoorInfo.door2, RoomsChecked);
                    }
                    else if (checkDoor == checkDoorInfo.door2)
                    {
                        BackgroundDoor_Environment checkDoor1Info = checkDoorInfo.door1.GetComponent<BackgroundDoor_Environment>();
                        //recursion to continue the search for the player
                        findAvailableRoutes(checkDoorInfo.door1, RoomsChecked);
                    }
                }
            }
        }
        //If the player is in the room the route is mapped
        else
        {
            routes.Add(RoomsChecked);
        }

    }

    /// <summary>
    /// Finds the quickest way to get to the player
    /// </summary>
    public void calculateQuickestRoute()
    {
        //Keeps track of which item in the list is the quickest way
        int i = 0;
        int j = 0;
        int shortestRoute = 1000;

        //Goes through each route and gets the size of each
        foreach (List<GameObject> list in routes)
        {
            Debug.Log(list.Count);
            //Keeps the index of the shortest route
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
        //Sets the quickest route to the outcome
        quickestRoute = routes[j];
    }


    /// <summary>
    /// Finds what the target door based on the route that was chosen
    /// </summary>
    public void findTargetDoor()
    {
        //Sets the goal to the second element in the list
        //This is because the first element will always be the room the enemy is already in so the second is the next room over
        GameObject goalRoom = quickestRoute[1];
        Room_Environment roomInfo = ZombieCurrentRoom.GetComponent<Room_Environment>();

        //Goes through each of the doors in the room and finds the appropriate one to get to the goal room
        foreach (GameObject door in roomInfo.Doors)
        {
            BackgroundDoor_Environment doorInfo = door.GetComponent<BackgroundDoor_Environment>();

            //Decides which door to follow once it has found a match to the associated room goal
            if (door == doorInfo.door1)
            {
                BackgroundDoor_Environment door2Info = doorInfo.door2.GetComponent<BackgroundDoor_Environment>();
                if (door2Info.inRoom == goalRoom)
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

