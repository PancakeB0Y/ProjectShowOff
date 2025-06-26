using UnityEngine;

//Handle all player input functions
public class PlayerInputs : MonoBehaviour
{
    public static PlayerInputs instance { get; private set; }

    InteractRaycast interactRaycast; //interacting with items inputs
    PlayerCameraController cameraController;

    ArmAnimatorController lightMatchAnimatorController;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        cameraController = GetComponent<PlayerCameraController>();

        interactRaycast = transform.parent.GetComponentInChildren<InteractRaycast>();

        lightMatchAnimatorController = transform.parent.GetComponentInChildren<ArmAnimatorController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnCloseMenus();
            OnPause();
        }

        if (IsGamePaused()) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            OnToggleInventory();
        }

        if (IsInventoryOpen())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            OnLightLantern();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    OnDropFirstItem();
        //}
    }

    public void OnLightLantern()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }

        if(InventoryManager.instance == null)
        {
            return;
        }

        if (lightMatchAnimatorController == null)
        {
            lightMatchAnimatorController = transform.parent.GetComponentInChildren<ArmAnimatorController>();
        }

        if (lightMatchAnimatorController != null && InventoryManager.instance.UseMatchstick()) {
            lightMatchAnimatorController.StartAnimation();
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

    public void OnCloseMenus()
    {
        if (InventoryManager.instance == null)
        {
            return;
        }

        if (InventoryManager.instance.isInspectingItem)
        {
            OnStopInspectingItem();
        }

        if (InventoryManager.instance.isInventoryOpenForInteraction)
        {
            OnCloseInventoryInteraction();
        }

        if (InventoryManager.instance.isInventoryOpen)
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

    public void OnPause()
    {
        if(UIManager.Instance == null)
        {
            return;
        }

        if (UIManager.Instance.IsPauseMenuOpen())
        {
            OnClosePauseMenu();
        }
        else
        {
            OnOpenPauseMenu();
        }
    }

    void OnOpenPauseMenu()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.OpenPauseMenu();
            EnableCursor();
        }
    }

    public void OnClosePauseMenu()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ClosePauseMenu();
            DisableCursor();
        }
    }

    public void EnableCursor()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    public void DisableCursor()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    bool IsGamePaused()
    {
        if (UIManager.Instance == null)
        {
            return false;
        }

        return UIManager.Instance.IsPauseMenuOpen();
    }

    bool IsInventoryOpen()
    {
        if(InventoryManager.instance == null)
        {
            return false;
        }

        return InventoryManager.instance.isInventoryOpen;
    }
}
