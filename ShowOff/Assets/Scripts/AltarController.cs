using System.Collections.Generic;
using UnityEngine;

public class AltarController : MonoBehaviour, IInteractable
{
    public string interactText { get; } = "Press [E] to interact";

    [SerializeField] Transform[] itemSlots = new Transform[3]; //positions where the items will be placed
    Dictionary<itemType, bool> isItemTypePlaced = new Dictionary<itemType, bool>()
    {
        {itemType.Chalice, false},
        {itemType.HolyOil, false},
        {itemType.Rosary, false}
    };

    //Open / close door
    public virtual void Interact()
    {
        if (PlayerInputs.instance != null) {
            PlayerInputs.instance.OnOpenInventoryInteraction(this);
        }
    }

    public virtual void InteractWithInventory(ItemController inventoryItem) {
        if (InventoryManager.instance == null) {
            return;
        }

        if (inventoryItem.item.itemType == itemType.Chalice || inventoryItem.item.itemType == itemType.HolyOil || inventoryItem.item.itemType == itemType.Rosary)
        {
            PlaceItem(inventoryItem);
            
            if (areAllRitualItemsPlaced())
            {
                PerformRitual();
            }
        }
    }

    void PlaceItem(ItemController itemController) {
        itemType currentItemType = itemController.item.itemType;
        if (!isItemTypePlaced[currentItemType])
        {
            Vector3 dropPosition;

            switch (currentItemType)
            {
                case itemType.Chalice:
                    dropPosition = itemSlots[0].position;
                    break;
                case itemType.HolyOil:
                    dropPosition = itemSlots[1].position;
                    break;
                case itemType.Rosary:
                    dropPosition = itemSlots[2].position;
                    break;
                default:
                    dropPosition = transform.position;
                    break;
            }

            InventoryManager.instance.DropItem(itemController, dropPosition);

            isItemTypePlaced[currentItemType] = true;

            //Disable the item so the player cannot interact with the object
            itemController.DisableInteractions();
        }
    }


    bool areAllRitualItemsPlaced()
    {
        foreach(bool value in isItemTypePlaced.Values)
        {
            if (!value)
            {
                return false;
            }
        }

        return true;
    }

    void PerformRitual()
    {
        Debug.Log("Perform Ritual");
    }
}
