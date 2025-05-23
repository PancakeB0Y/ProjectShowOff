using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector3> OnStatueFollow;

    [SerializeField]
    float moveSpeed;

    Rigidbody rb;

    Vector3 moveInput;

    LanternController lantern;

    List<Light> currentLights = new List<Light>();

    void Awake()
    {
        LightSourceCollisionDetection.OnLightDisabled += DeregisterLight;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lantern = transform.parent.GetComponentInChildren<LanternController>();
    }

    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = moveInput.z * transform.forward + moveInput.x * transform.right;
        moveDirection = moveDirection * moveSpeed;
        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Light>(out Light light)
            && !currentLights.Contains(light))
        {
            currentLights.Add(light);
        }

        if (!other.TryGetComponent<WindSphere>(out WindSphere wind))
        {
            return;
        }

        if (lantern == null)
        {
            lantern = transform.parent.GetComponentInChildren<LanternController>();
        }

        if (lantern != null)
        {
            lantern.HandleWindCollision();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Light>(out Light light))
        {
            DeregisterLight(light);
        }
    }

    void DeregisterLight(Light light)
    {
        if (currentLights.Contains(light))
        {
            currentLights.Remove(light);
            if (currentLights.Count == 0)
            {
                // Send event to statue to start following
                OnStatueFollow?.Invoke(transform.position);
            }
        }
    }

    void OnDestroy()
    {
        LightSourceCollisionDetection.OnLightDisabled -= DeregisterLight;
    }
}
