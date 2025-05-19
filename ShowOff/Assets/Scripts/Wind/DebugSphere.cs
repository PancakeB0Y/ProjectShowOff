using System.Collections;
using UnityEngine;

public class DebugSphere : MonoBehaviour
{
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetVisibility(bool visible)
    {
        if (this == null) { 
            return;
        }

        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        if (meshRenderer != null && meshRenderer.enabled != visible)
        {
            meshRenderer.enabled = visible;
        }
    }
}
