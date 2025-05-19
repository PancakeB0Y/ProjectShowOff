using System.Collections;
using UnityEngine;

public class DebugCube : DebugShape
{
    public void UpdateBounds(Vector3 bounds)
    {
        transform.localScale = bounds;
    }
}
