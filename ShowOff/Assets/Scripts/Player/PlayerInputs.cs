using UnityEngine;

//Handle all player input functions
public class PlayerInputs : MonoBehaviour
{
    InteractRaycast interactRaycast; //interacting with items inputs
    LanternController lanternController; //Lantern inputs
    PlayerCameraController cameraController;

    private void Awake()
    {
        cameraController = GetComponent<PlayerCameraController>();

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
            OnToggleInventory();
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
            lanternController.LightLantern();
        }
    }

    public void OnInteract()
    {
        if (interactRaycast == null) {
            interactRaycast = transform.parent.GetComponentInChildren<InteractRaycast>();
        }

        if (interactRaycast != null) {
            interactRaycast.Interact();
        }
    }

    //Open / close inventory
    public void OnToggleInventory()
    {
        if (InventoryManager.instance == null)
        {
            return;
        }

        //Open / Close inventory
        InventoryManager.instance.ToggleInventory();

        if (cameraController == null)
        {
            cameraController = GetComponent<PlayerCameraController>();
        }

        if (cameraController != null)
        {
            //Enable / disable mouse
            cameraController.ToggleCameraMovement();
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
