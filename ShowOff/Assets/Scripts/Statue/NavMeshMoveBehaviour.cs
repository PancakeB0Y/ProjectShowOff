using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMoveBehaviour : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] protected float targetRange = 1f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTargetPosition(Transform target)
    {
        if (agent != null)
        {
            StartCoroutine(FollowTarget(targetRange, target));
        }
    }

    private IEnumerator FollowTarget(float range, Transform target)
    {
        Vector3 previousTargetPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity);


        while (Vector3.SqrMagnitude(transform.position - target.position) > 0.1f)
        {
            float distFromPrevLocation = Vector3.SqrMagnitude(previousTargetPosition - target.position);
            if (distFromPrevLocation > 0.1f)
            {
                agent.SetDestination(target.position);
                previousTargetPosition = target.position;
            }

            //wait before checking again
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    public bool IsTargetReached()
    {
        float dist = agent.remainingDistance;

        if (dist != 0 && dist != Mathf.Infinity && dist < targetRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
        agent.acceleration = speed * 2;
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
