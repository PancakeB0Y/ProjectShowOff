using System.Collections;
using UnityEngine;

public class DebugSphere : DebugShape
{
    public void UpdateRadius(float radius)
    {
        Vector3 sphereScale = new Vector3(radius, radius, radius);
        transform.localScale = sphereScale;
    }
}
