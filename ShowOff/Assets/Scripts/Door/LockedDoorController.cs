using UnityEngine;

public class LockedDoorController : DoorController
{
    [SerializeField] bool isDoorLocked = true; //is the door locked or unlocked
    [SerializeField] itemType unlockItem = itemType.Key;
    public override void Interact()
    {
        if (isDoorLocked) {
            doorAudioController.PlayDoorNotOpeningSound();
            PlayerInputs.instance.OnOpenInventoryInteraction(this);
            return;
        }

        //Open / close door
        HandleDoor();
    }

    public override void InteractWithInventory(ItemController inventoryItem) {
        if (inventoryItem.item.itemType == unlockItem)
        {
            InventoryManager.instance.RemoveItem(inventoryItem);

            isDoorLocked = false;

            Interact();
        }
    }
}
