using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IInteractable
{
    public Item item;

    public void Interact()
    {
        if(InventoryManager.instance == null)
        {
            return;
        }

        InventoryManager.instance.PickupItem(this);
    }
}
