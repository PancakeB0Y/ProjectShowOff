using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

//Section where wind will spawn randomly
public class WindSection : MonoBehaviour
{
    BoxCollider boundsCollider;
    DebugCube debugCube; //cube to show radius

    [Header("Prefabs")]
    [SerializeField] GameObject windSpherePrefab;

    [Header("Properties")]
    [SerializeField] private Vector3 bounds = Vector3.one;

    [Header("Debugging")]
    [SerializeField] bool debugBounds = true;

    [SerializeField] List<GameObject> windSpheres = new List<GameObject>();

    void Start()
    {
        debugCube = GetComponentInChildren<DebugCube>();
    }

    //Called on inspector changes
    private void OnValidate()
    {
        SyncComponents();
        CleanUpSpheres();
    }

    private void OnEnable()
    {
        SyncComponents();
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
        CleanUpSpheres();
    }

    //Updates the collider and debugCube based on bounds 
    private void SyncComponents()
    {
        //Sync collider
        if (boundsCollider == null)
        {
            boundsCollider = GetComponent<BoxCollider>();
        }

        if (boundsCollider != null)
        {
            boundsCollider.size = bounds;
        }

        //Sync debugCube
        if (debugCube == null)
        {
            debugCube = GetComponentInChildren<DebugCube>();
        }

        if (debugCube != null)
        {
            //Show collision bounds at runtime
            debugCube.SetVisibility(debugBounds);

            //Scale cube if visible
            if (debugBounds)
            {
                debugCube.UpdateBounds(bounds);
            }
        }
    }

    public Vector3 GetBounds()
    {
        return bounds;
    }

    public void UpdateBounds(Vector3 newBounds)
    {
        bounds = newBounds;
        SyncComponents();
    }

    //Adds a new WindSphere object to the list
    public GameObject AddWindSphere()
    {
        GameObject newSphere = Instantiate(windSpherePrefab, transform.position, Quaternion.identity, transform);

        windSpheres.Add(newSphere);

        return newSphere;
    }

    public void CleanUpSpheres()
    {
        for (int i = windSpheres.Count - 1; i >= 0; i--)
        {
            if(windSpheres[i] == null)
            {
                windSpheres.RemoveAt(i);
            }
        }
    }
}
