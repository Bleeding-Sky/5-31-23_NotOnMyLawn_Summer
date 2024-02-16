using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class OverheadPathing_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    public Transform target;
    [SerializeField] float normalSpeed = 3;
    [SerializeField] float stunSpeed = 0;
    [SerializeField] float stumbleSpeed = 1.5f;
    [SerializeField] float fallenMoveSpeed = 0;
    [SerializeField] float enragedSpeed = 5;

    [Header("DEBUG")]
    public NavMeshAgent myAgent;
    public Status_Zombie statusScript; //passed in by spawner_zombie script

    // Start is called before the first frame update
    void Start()
    {
        //fetch and configure agent component
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.updateRotation = false;
        myAgent.updateUpAxis = false;

        //enable agent after configuration is complete to avoid errors
        myAgent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //set destination to the position of the target every frame
        myAgent.SetDestination(target.position);
        UpdateAgentSpeedBasedOnStatus();

    }

    private void UpdateAgentSpeedBasedOnStatus()
    {
        float currentSpeed = 0;
        switch (statusScript.standingState)
        {
            case ZmbStandingStateEnum.NoStatus:
                currentSpeed = normalSpeed;
                break;

            case ZmbStandingStateEnum.Stunned:
                currentSpeed = stunSpeed;
                break;

            case ZmbStandingStateEnum.Stumbling:
                currentSpeed = stumbleSpeed;
                break;

            case ZmbStandingStateEnum.FallenForward:
                currentSpeed = fallenMoveSpeed;
                break;

            case ZmbStandingStateEnum.FallenBackward:
                currentSpeed = fallenMoveSpeed;
                break;

            case ZmbStandingStateEnum.Enraged:
                currentSpeed = enragedSpeed;
                break;
        }

        myAgent.speed = currentSpeed;
    }
}
