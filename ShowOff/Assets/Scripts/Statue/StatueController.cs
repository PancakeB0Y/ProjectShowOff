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

    string lightTag = "Light";

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
        if (!other.gameObject.CompareTag(lightTag))
        {
            return;
        }

        LightSource light = other.GetComponent<LightSource>();
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
        if (!other.gameObject.CompareTag(lightTag))
        {
            return;
        }

        LightSource light = other.GetComponent<LightSource>();
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
        if (other.gameObject.CompareTag(lightTag))
        {
            StartMovement();
        }
    }

    void StopMovement()
    {
        if (moveBehaviour != null)
        {
            moveBehaviour.StopMovement();
        }
    }

    void StartMovement()
    {
        if (moveBehaviour != null)
        {
            moveBehaviour.StartMovement();
        }
    }
}
