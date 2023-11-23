using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorZombie_Movement : MonoBehaviour
{
    public Player_PositionTracker playerPosition;
    public IndoorZombie_States zombieStates;
    public Vector3 zombiePosition;
    public float normalZombieSpeed;
    // Update is called once per frame
    void Update()
    {
        zombiePosition = transform.position;
        ChasingPlayer();
    }

    private void ChasingPlayer()
    {
        zombieStates.isChasing = true;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.playerPosition, normalZombieSpeed * Time.deltaTime);
    }

    private void FindPlayer()
    {

    }

    private void IdleZombie()
    {

    }

    
}
