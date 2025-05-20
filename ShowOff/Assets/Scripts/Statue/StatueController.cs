using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NavMeshMoveBehaviour))]
public class StatueController : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] NavMeshMoveBehaviour moveBehaviour;

    [Header("Properties")]
    [SerializeField] float speed = 7;

    GameObject player;

    private void Awake()
    {
        moveBehaviour = GetComponent<NavMeshMoveBehaviour>();
        player = FindFirstObjectByType<PlayerController>().gameObject;
    }

    private void Start()
    {
        if(moveBehaviour == null || player == null)
        {
            return;
        }

        moveBehaviour.SetSpeed(speed);
        SetTargetPosition(player.transform);
    }

    private void FixedUpdate()
    {
        if (moveBehaviour == null)
        {
            return;
        }

        if (moveBehaviour.IsTargetReached())
        {
            Debug.Log("reached player");
            //Die();
        }
    }

    private void SetTargetPosition(Transform target)
    {
        if (moveBehaviour != null)
        {
            moveBehaviour.SetTargetPosition(target);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        LightSource light;
        if (!other.TryGetComponent<LightSource>(out light))
        {
            return;
        }

        if(light == null)
        {
            return;
        }

        if (light.isLightOn) {
            StopMovement();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        LightSource light;
        if (!other.TryGetComponent<LightSource>(out light))
        {
            return;
        }

        if (light == null)
        {
            return;
        }

        if (light.isLightOn)
        {
            StopMovement();
        }
        else
        {
            StartMovement();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<LightSource>(out LightSource light))
        {
            StartMovement();
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
}
