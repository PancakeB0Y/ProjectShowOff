using UnityEngine;

//Control the visibility of a GameObject
public class DebugShape : MonoBehaviour
{
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetVisibility(bool visible)
    {
        if (this == null)
        {
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
