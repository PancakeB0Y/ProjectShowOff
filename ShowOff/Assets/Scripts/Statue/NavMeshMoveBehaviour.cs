using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMoveBehaviour : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [Header("Player follow")]
    [SerializeField] private float maxSpawnRangeFromPlayerPos = 15.0f;
    [SerializeField] private float minSpawnRangeFromPlayerPos = 10.0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetupPlayerFollow(Vector3 playerPos)
    {
        Debug.Log("Statue follow setup");

        agent.Warp(NavMeshSamplePoint(playerPos));
        SetTargetPosition(playerPos);
    }

    Vector3 NavMeshSamplePoint(Vector3 center)
    {
        int countOut = 0;

        Vector3 randomPoint;
        while (true)
        {
            randomPoint = center + UnityEngine.Random.insideUnitSphere * maxSpawnRangeFromPlayerPos;

            countOut++;
            if (countOut > 1000)
            {
                throw new System.Exception("Error: infinite while loop. Cannot find point on nav mesh");
            }

            // If sample point is closer than the minimum range
            if (Vector3.Distance(randomPoint, center) < minSpawnRangeFromPlayerPos)
                continue;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
                //or add a for loop like in the documentation
                return hit.position;
            }
        }
    }

    public void SetTargetPosition(Vector3 targetPos)
    {
        agent.SetDestination(targetPos);
    }

    public void StopMovement()
    {
        if (agent != null)
        {
            agent.isStopped = true;
        }
    }

    public void StartMovement()
    {
        if (agent != null)
        {
            agent.isStopped = false;
        }
    }
}
