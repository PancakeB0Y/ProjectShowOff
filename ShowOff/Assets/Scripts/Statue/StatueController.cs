using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(NavMeshMoveBehaviour))]
public class StatueController : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] NavMeshMoveBehaviour moveBehaviour;

    private StatueState statueState;

    PlayerController player;

    private void Awake()
    {
        moveBehaviour = GetComponent<NavMeshMoveBehaviour>();
        player = FindFirstObjectByType<PlayerController>();

        player.OnStatueFollow += SetupPlayerFollow;
    }

    private void Start()
    {
        if(moveBehaviour == null || player == null)
        {
            return;
        }

        SetState(StatueState.Disabled);
    }

    void Update()
    {
        Debug.Log(statueState);

        switch (statueState)
        {
            case StatueState.Disabled:
                break;
            case StatueState.Chasing:
                moveBehaviour.SetTargetPosition(player.transform.position);
                break;
            default:
                break;
        }
    }

    void SetState(StatueState newState)
    {
        switch (newState)
        {
            case StatueState.Disabled:
                gameObject.SetActive(false);
                statueState = newState;
                break;
            case StatueState.Chasing:
                gameObject.SetActive(true);
                statueState = newState;
                break;
            case StatueState.Freezed:
                statueState = newState;
                break;
            default:
                break;
        }
    }

    private void SetupPlayerFollow(Vector3 playerPosition)
    {
        SetState(StatueState.Chasing);

        if (moveBehaviour)
        {
            moveBehaviour.SetupPlayerFollow(playerPosition);
        }
    }

    public void StopMovement()
    {
        if (moveBehaviour != null)
        {
            moveBehaviour.StopMovement();
        }
    }

    public void StartMovement()
    {
        if (moveBehaviour != null)
        {
            moveBehaviour.StartMovement();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player.gameObject
            && statueState != StatueState.Freezed)
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
            SetState(StatueState.Freezed);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<LightSourceCollisionDetection>(out LightSourceCollisionDetection light))
        {
            StopMovement();
            SetState(StatueState.Disabled);
        }
    }

    void OnDestroy()
    {
        player.OnStatueFollow -= SetupPlayerFollow;
    }
}

public enum StatueState
{
    Freezed,
    Disabled,
    Chasing,
}