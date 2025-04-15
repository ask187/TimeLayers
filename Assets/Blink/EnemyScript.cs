using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Script to encorporate enemy patrolling and chasing behavior
/// </summary>
public class EnemyScript : MonoBehaviour
{
    public Transform[] waypoints;
    public float waitBeforeStart = 2f;
    public float rotationSpeed = 5f;
    public float arrivalThreshold = 1f;

    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool isPatrolling = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(BeginPatrolWithDelay());
    }

    IEnumerator BeginPatrolWithDelay()
    {
        // Start Idle
        agent.isStopped = true;
        yield return new WaitForSeconds(waitBeforeStart);

        // Begin Patrol
        isPatrolling = true;
        GoToNextWaypoint();
    }

    void Update()
    {
        if (!isPatrolling || waypoints.Length == 0) return;

        // Smoothly rotate towards the next point
        Vector3 direction = agent.steeringTarget - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // If close to destination, switch to next
        if (!agent.pathPending && agent.remainingDistance <= arrivalThreshold)
        {
            GoToNextWaypoint();
        }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}