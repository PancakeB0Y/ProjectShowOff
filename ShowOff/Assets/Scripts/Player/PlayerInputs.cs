using UnityEngine;

//Handle all player input functions
public class PlayerInputs : MonoBehaviour
{
    LanternController lanternController; //Lantern inputs
    InteractRaycast interactRaycast; //interacting with items inputs
    InventoryManager inventoryManager; //inventory inputs

    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        interactRaycast = transform.parent.GetComponentInChildren<InteractRaycast>();
        lanternController = transform.parent.GetComponentInChildren<LanternController>();
    }
}
