using UnityEngine;

//Handle all player input functions
public class PlayerInputs : MonoBehaviour
{
    public static PlayerInputs instance { get; private set; }

    InteractRaycast interactRaycast; //interacting with items inputs
    LanternController lanternController; //Lantern inputs
    PlayerCameraController cameraController;

    private void Awake()
    {
        instance = this;

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

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    OnDropFirstItem();
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnCloseMenu();
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
        if (InventoryManager.instance == null)
        {
            return;
        }


        if (InventoryManager.instance.isInspectingItem)
        {
            OnStopInspectingItem();
        }
        else if (InventoryManager.instance.isInventoryOpenForInteraction)
        {
            OnCloseInventoryInteraction();
        }
        else
        {
            OnItemInteract();
        }
    }

    public void OnItemInteract()
    {
        if (interactRaycast == null)
        {
            interactRaycast = transform.parent.GetComponentInChildren<InteractRaycast>();
        }

        if (interactRaycast != null)
        {
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

        if (InventoryManager.instance.isInspectingItem)
        {
            OnStopInspectingItem();
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
            if (InventoryManager.instance.isInventoryOpen)
            {
                cameraController.DisableCameraMovement();
                Time.timeScale = 0f;
            }
            else
            {
                cameraController.EnableCameraMovement();
                Time.timeScale = 1f;
            }
        }
    }

    public void OnCloseInventory()
    {
        if (InventoryManager.instance == null)
        {
            return;
        }

        //Close inventory
        InventoryManager.instance.DisableInventory();

        if (cameraController == null)
        {
            cameraController = GetComponent<PlayerCameraController>();
        }

        if (cameraController != null)
        {
            cameraController.EnableCameraMovement();
            Time.timeScale = 1f;
        }
    }

    //Open inventory for item interactions
    public void OnOpenInventoryInteraction(IInteractable interactableItem)
    {
        if (InventoryManager.instance == null)
        {
            return;
        }

        //Open inventory
        InventoryManager.instance.OpenInventoryInteraction(interactableItem);

        if (cameraController == null)
        {
            cameraController = GetComponent<PlayerCameraController>();
        }

        if (cameraController != null)
        {
            cameraController.DisableCameraMovement();
            Time.timeScale = 0f;
        }
    }

    public void OnCloseInventoryInteraction()
    {
        if (InventoryManager.instance == null)
        {
            return;
        }

        if (InventoryManager.instance.isInventoryOpenForInteraction)
        {
            OnToggleInventory();
        }
    }

    public void OnStopInspectingItem()
    {
        if (InventoryManager.instance == null)
        {
            return;
        }

        if (InventoryManager.instance.isInspectingItem)
        {
            InventoryManager.instance.StopInspectingItem();
        }
    }

    public void OnCloseMenu()
    {
        if (InventoryManager.instance == null)
        {
            return;
        }

        if (InventoryManager.instance.isInspectingItem)
        {
            OnStopInspectingItem();
        }
        else if (InventoryManager.instance.isInventoryOpenForInteraction)
        {
            OnCloseInventoryInteraction();
        }
        else if(InventoryManager.instance.isInventoryOpen)
        {
            OnCloseInventory();
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
