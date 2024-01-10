using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class OverheadPathing_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    public Transform target;

    [Header("DEBUG")]
    public NavMeshAgent myAgent;

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
    }
}
