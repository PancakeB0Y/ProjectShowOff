using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] GameObject UIItemPrefab; //prefab for an empty item image in the inventory

    [Header("Properties")]
    [SerializeField] int inventorySize = 20;
    List<ItemController> items = new List<ItemController>(); //list of all items in the inventory

    GameObject inventoryUI; //UI inventory where all the inventory icons will be. Needs a Grid Layout Group component
    string inventoryUITag = "Inventory";

    void Awake()
    {
        instance = this;

        inventoryUI = GameObject.FindWithTag(inventoryUITag);

        if (inventoryUI != null) {
            inventoryUI.SetActive(false);
        }
    }

    //Open / close inventory
    public void ToggleInventory()
    {
        if (inventoryUI.activeInHierarchy)
        {
            inventoryUI.SetActive(false);
            DisableCursor();
        }
        else
        {
            inventoryUI.SetActive(true);
            EnableCursor();
        }
    }

    void EnableCursor()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.visible = true;
    }

    void DisableCursor()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    void Add(ItemController item)
    {
        if (!IsInventoryFull())
        {
            items.Add(item);

            UpdateItems();
        }
    }

    public void PickupItem(ItemController item)
    {
        if (!IsInventoryFull())
        {
            //Add item to inventory
            Add(item);

            //Disable gameObject of the item in the scene
            item.gameObject.SetActive(false);
        }
    }

    public void Remove(ItemController item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);

            UpdateItems();
        }
    }

    public void DropFirstItem()
    {
        if (items.Count > 0)
        {
            DropItem(items[0]);
        }
    }

    public void DropItem(ItemController item)
    {
        if (!items.Contains(item))
        {
            return;
        }

        //Calculate where the item should be dropped
        float dropDistance = 1.2f; //distance to drop from the player
        Vector3 dropPosition = transform.position + transform.forward * dropDistance;

        item.gameObject.transform.position = dropPosition;
        item.gameObject.SetActive(true);

        Remove(item);
    }

    //update inventory icons 
    public void UpdateItems()
    {
        if(inventoryUI == null)
        {
            return;
        }

        DestroyAllUIItems();

        foreach(ItemController itemController in items)
        {
            //Create a UI object for each inventory item
            GameObject UIItem = Instantiate(UIItemPrefab, inventoryUI.transform);

            UnityEngine.UI.Image itemIcon = UIItem.GetComponent<UnityEngine.UI.Image>();

            //Set its icon
            itemIcon.sprite = itemController.item.itemIcon;
        }

    }

    public void DestroyAllUIItems()
    {
        foreach (Transform child in inventoryUI.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public ItemController GetItem(int inventorySlot)
    {
        return items[inventorySlot];
    }

    bool IsInventoryFull()
    {
        return items.Count >= inventorySize;
    }
}
