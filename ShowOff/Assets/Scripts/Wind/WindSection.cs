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
    [SerializeField] bool debugSpheres = true;

    [SerializeField] List<GameObject> windSpheres = new List<GameObject>();

    void Start()
    {
        debugCube = GetComponentInChildren<DebugCube>();

        //Sync the debugging of the spheres with the wind section
        SyncDebugging();

        //Clean up list of spheres
        CleanUpSpheres();

        //Disable all possible wind spheres
        DisableAllWindSpheres();
    }

    //Called on inspector changes
    private void OnValidate()
    {
        SyncComponents();
        CleanUpSpheres();
    }

    //private void OnEnable()
    //{
    //    SyncComponents();
    //}

    //Sync the debugging of the spheres
    void SyncDebugging()
    {
        foreach (GameObject wind in windSpheres)
        {
            WindSphere windSphere = wind.GetComponent<WindSphere>();
            windSphere.debugRadius = debugSpheres;

            windSphere.SyncComponents();
        }
    }

    public void CleanUpSpheres()
    {
        for (int i = windSpheres.Count - 1; i >= 0; i--)
        {
            if (windSpheres[i] == null)
            {
                windSpheres.RemoveAt(i);
            }
        }
    }

    void DisableAllWindSpheres()
    {
        foreach (GameObject wind in windSpheres)
        {
            wind.SetActive(false);
        }
    }

    //Adds a new windSphere object to the list
    public GameObject AddWindSphere()
    {
        GameObject newSphere = Instantiate(windSpherePrefab, transform.position, Quaternion.identity, transform);

        windSpheres.Add(newSphere);

        return newSphere;
    }

    //Get a random windSphere from the list
    GameObject GetRandomWindSphere()
    {
        CleanUpSpheres();

        int randIndex = Random.Range(0, windSpheres.Count);

        return windSpheres[randIndex];
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
            SyncDebugging();

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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            return;
        }

        //enable a random windSphere from the list
        GetRandomWindSphere().SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            return;
        }

        DisableAllWindSpheres();
    }
}
