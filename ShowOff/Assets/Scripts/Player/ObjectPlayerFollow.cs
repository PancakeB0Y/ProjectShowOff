using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlayerFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Vector3 offset;

    void Start()
    {
        offset = transform.localPosition;
    }

    void LateUpdate()
    {
        transform.position = player.position + player.rotation * offset;
    }
}