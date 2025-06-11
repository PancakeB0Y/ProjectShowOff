using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMoveBehaviour : MonoBehaviour
{
    NavMeshAgent agent;

    [Header("Player follow")]
    [SerializeField] private float maxSpawnRangeFromPlayerPosInitial = 15.0f;
    [SerializeField] private float minSpawnRangeFromPlayerPosInitial = 10.0f;
    [SerializeField] private float maxSpawnRangeFromPlayerPosForRitual = 10.0f;
    [SerializeField] private float minSpawnRangeFromPlayerPosForRitual = 5.0f;

    private float maxSpawnRangeFromPlayerPos;
    private float minSpawnRangeFromPlayerPos;

    private MoveState statueState;

    PlayerController player;

    StatueAudioController statueAudioController;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        player = FindFirstObjectByType<PlayerController>();
        player.OnStatueFollow += SetupPlayerFollow;

        statueAudioController = GetComponent<StatueAudioController>();
    }

    void Start()
    {
        maxSpawnRangeFromPlayerPos = maxSpawnRangeFromPlayerPosInitial;
        minSpawnRangeFromPlayerPos = minSpawnRangeFromPlayerPosInitial;

        SetState(MoveState.Disabled);
    }

    void SetState(MoveState newState)
    {
        switch (newState)
        {
            case MoveState.Disabled:
                gameObject.SetActive(false);
                break;
            case MoveState.Chasing:
                gameObject.SetActive(true);
                break;
            case MoveState.Freezed:
                break;
            default:
                break;
        }

        statueState = newState;
    }

    void Update()
    {
        switch (statueState)
        {
            case MoveState.Chasing:
                SetTargetPosition(player.transform.position);
                break;
            default:
                break;
        }
    }

    public void SetupPlayerFollow(Vector3 playerPos)
    {
        SetState(MoveState.Chasing);

        agent.Warp(NavMeshSamplePoint(playerPos));

        statueAudioController.SetupPlayerFollowAudio(minSpawnRangeFromPlayerPos);
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

        statueAudioController.AdjustAudioSourceVolume(agent.remainingDistance);
    }

    public void StopMovement()
    {
        if (agent != null)
        {
            agent.isStopped = true;
            statueAudioController.StopPlaying();
        }
    }

    public void UpdateSpawnRanges()
    {
        maxSpawnRangeFromPlayerPos = maxSpawnRangeFromPlayerPosForRitual;
        minSpawnRangeFromPlayerPos = minSpawnRangeFromPlayerPosForRitual;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player.gameObject
            && statueState != MoveState.Freezed)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<LightSourceCollisionDetection>(out LightSourceCollisionDetection light))
        {
            StopMovement();
            SetState(MoveState.Freezed);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<LightSourceCollisionDetection>(out LightSourceCollisionDetection light))
        {
            StopMovement();
            SetState(MoveState.Disabled);
        }
    }

    public void GetDestroyed()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        player.OnStatueFollow -= SetupPlayerFollow;
    }
}

public enum MoveState
{
    Freezed,
    Disabled,
    Chasing,
}