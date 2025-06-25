using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IInteractable
{
    public string interactText { get; } = "Press [E] to pick up";

    public Item item;

    [SerializeField]
    MeshRenderer meshRenderer;
    Material outlineShaderMaterial;

    void Start()
    {
        if (!meshRenderer)
            meshRenderer = GetComponentInChildren<MeshRenderer>();

        outlineShaderMaterial = meshRenderer.materials[1];
    }

    public void Interact()
    {
        if(InventoryManager.instance == null)
        {
            return;
        }

        InventoryManager.instance.PickupItem(this);
    }

    public void InteractWithInventory(ItemController inventoryItem){}

    public bool IsEqual(ItemController other)
    {
        return item == other.item;
    }

    public void DisableInteractions()
    {
        DisableColliders();
    }

    public void DisableOutlineShader()
    {
        meshRenderer.materials[1] = null;
    }

    public void EnableOutlineShader()
    {
        meshRenderer.materials[1] = outlineShaderMaterial;
    }

    //Disable colliders so the player cannot pick up the objects again
    void DisableColliders()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
