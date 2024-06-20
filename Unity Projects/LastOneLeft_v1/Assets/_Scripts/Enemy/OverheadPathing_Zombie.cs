using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class OverheadPathing_Zombie : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float normalSpeed = 1;
    [SerializeField] float stunSpeed = 0;
    [SerializeField] float stumbleSpeed = .5f;
    [SerializeField] float fallenMoveSpeed = 0;
    [SerializeField] float enragedSpeed = 2.5f;
    [SerializeField] float crawlMoveSpeed = 0.5f;

    [Header("DEBUG")]
    public NavMeshAgent myAgent;
    public Status_Zombie statusScript;
    public Transform target;

    // Start is called before the first frame update
    void Awake()
    {
        statusScript = GetComponentInParent<Status_Zombie>();

        //fetch and configure agent component
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.updateRotation = false;
        myAgent.updateUpAxis = false;

    }

    private void Start()
    {
        //enable agent only after configuration is complete to avoid errors
        myAgent.enabled = true;

        target = FindClosestWindowTransform();

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

        //crawling overrides all other states
        if (statusScript.isCrawling)
        {
            currentSpeed = crawlMoveSpeed;
        }
        else
        {
            //change move speed based on current state
            switch (statusScript.status)
            {
                case FodderStatus.Idle:
                    currentSpeed = normalSpeed;
                    break;

                case FodderStatus.Stunned:
                    currentSpeed = stunSpeed;
                    break;

                case FodderStatus.Stumbling:
                    currentSpeed = stumbleSpeed;
                    break;

                case FodderStatus.FallenFaceDown:
                    currentSpeed = fallenMoveSpeed;
                    break;

                case FodderStatus.FallenFaceUp:
                    currentSpeed = fallenMoveSpeed;
                    break;

                case FodderStatus.Enraged:
                    currentSpeed = enragedSpeed;
                    break;
            }
        }

        myAgent.speed = currentSpeed;
    }

    /// <summary>
    /// returns the transform of the closest overhead window to the zombie
    /// </summary>
    /// <returns></returns>
    Transform FindClosestWindowTransform()
    {
        GameObject[] OverheadWindowList = GameObject.FindGameObjectsWithTag("Overhead Window");

        Transform closestWindowTransform = null;
        float ?closestWindowDistance = null;

        //find the closest window to the zombie
        foreach (GameObject window in OverheadWindowList)
        {
            float currentWindowDistance = Vector3.Distance(window.transform.position, transform.position);

            if (closestWindowDistance == null)
            {
                closestWindowDistance = currentWindowDistance;
                closestWindowTransform = window.transform;
            }
            else if (currentWindowDistance < closestWindowDistance)
            {
                closestWindowDistance = currentWindowDistance;
                closestWindowTransform = window.transform;
            }
        }

        return closestWindowTransform;

    }

}
