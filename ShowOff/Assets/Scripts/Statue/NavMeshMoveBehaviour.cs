using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

        // Connect player transform to audio controller for distance/occlusion checks
        statueAudioController.playerTransform = player.transform;
    }

    void Start()
    {
        maxSpawnRangeFromPlayerPos = maxSpawnRangeFromPlayerPosInitial;
        minSpawnRangeFromPlayerPos = minSpawnRangeFromPlayerPosInitial;

        SetState(MoveState.Freezed);
        agent.enabled = false;
    }

    void SetState(MoveState newState)
    {
        Debug.Log($"SetState called. Changing from {statueState} to {newState}");

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
        }

        statueState = newState;
    }

    void Update()
    {
        if (statueState == MoveState.Chasing)
        {
            SetTargetPosition(player.transform.position);
        }
    }

    public void SetupPlayerFollow(Vector3 playerPos)
    {
        if (!agent.enabled)
            agent.enabled = true;

        //Debug.Log($"SetupPlayerFollow called. Current State: {statueState}");
        //if (statueState == MoveState.Chasing)
        //{
        //    Debug.Log("SetupPlayerFollow ignored because statue is already chasing.");
        //    return;
        //}

        Debug.Log("Setting up player follow.");
        SetState(MoveState.Chasing);
        agent.Warp(NavMeshSamplePoint(playerPos));
        statueAudioController.SetupPlayerFollowAudio();
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
                Debug.Log("Error: infinite while loop. Cannot find point on nav mesh");
                return Vector3.zero;
            }

            if (Vector3.Distance(randomPoint, center) < minSpawnRangeFromPlayerPos)
                continue;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
    }

    public void SetTargetPosition(Vector3 targetPos)
    {
        agent.SetDestination(targetPos);
        // No longer needed here because audio updates itself in StatueAudioController.Update()
        // statueAudioController.AdjustAudioDistanceParameter(transform.position, player.transform.position);
    }

    public void StopMovement()
    {
        if (agent != null)
        {
            if (!agent)
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
        if (collision.gameObject == player.gameObject && statueState != MoveState.Freezed)
        {
            SetState(MoveState.Freezed);

            PlayerCaughtHandler playerCaughtHandler = player.GetComponent<PlayerCaughtHandler>();
            playerCaughtHandler.Die(transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<LightSourceCollisionDetection>(out _))
        {
            Debug.Log("Entered light, stopping statue.");
            StopMovement();
            SetState(MoveState.Freezed);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<LightSourceCollisionDetection>(out _))
        {
            Debug.Log("Exited light, disabling statue.");
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
