using UnityEngine;

//Handle all player input functions
public class PlayerInputs : MonoBehaviour
{
    InventoryManager inventoryManager; //inventory inputs
    InteractRaycast interactRaycast; //interacting with items inputs
    LanternController lanternController; //Lantern inputs

    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        interactRaycast = transform.parent.GetComponentInChildren<InteractRaycast>();
        lanternController = transform.parent.GetComponentInChildren<LanternController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnLightLantern();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    public void OnLightLantern()
    {
        if(lanternController == null)
        {
            lanternController = transform.parent.GetComponentInChildren<LanternController>();
        }

        if (lanternController != null) {
            lanternController.OnLightLantern();
        }
    }

    public void OnInteract()
    {
        if (interactRaycast == null) {
            interactRaycast = transform.parent.GetComponentInChildren<InteractRaycast>();
        }

        if (interactRaycast != null) {
            interactRaycast.OnInteract();
        }
    }
}
