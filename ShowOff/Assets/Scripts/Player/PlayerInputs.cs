using UnityEngine;

//Handle all player input functions
public class PlayerInputs : MonoBehaviour
{
    InteractRaycast interactRaycast; //interacting with items inputs
    LanternController lanternController; //Lantern inputs

    private void Awake()
    {
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

        if (Input.GetKeyDown(KeyCode.I))
        {
            OnHandleInventory();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnDropFirstItem();
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

    //Open / close inventory
    public void OnHandleInventory()
    {
        if (InventoryManager.instance != null)
        {
            InventoryManager.instance.OnHandleInventory();
        }
    }

    public void OnDropFirstItem()
    {
        if (InventoryManager.instance != null)
        {
            InventoryManager.instance.DropFirstItem();
        }
    }
}
