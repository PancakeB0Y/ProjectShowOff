using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IInteractable
{
    public string interactText { get; } = "Press [E] to pick up";

    public Item item;

    public void Interact()
    {
        if(InventoryManager.instance == null)
        {
            return;
        }

        InventoryManager.instance.PickupItem(this);
    }

    public void InteractWithInventory(ItemController inventoryItem){}
}
