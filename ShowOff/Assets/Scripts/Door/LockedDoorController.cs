using UnityEngine;

public class LockedDoorController : DoorController
{
    [SerializeField] bool isDoorLocked = true; //is the door locked or unlocked
    [SerializeField] bool canBeUnlocked = false; //is the door locked or unlocked
    [SerializeField] ItemType unlockItem = ItemType.Key;

    public override void Interact()
    {
        if (!canBeUnlocked)
        {
            doorAudioController.PlayDoorNotOpeningSound();
            return;
        }

        if (isDoorLocked && PlayerInputs.instance != null) {
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

            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayInventoryItemSelected();
            }

            Interact();
        }
        else
        {
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayWrongItemChosen();
            }
        }
    }
}
