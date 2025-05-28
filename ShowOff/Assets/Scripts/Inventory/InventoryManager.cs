using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] GameObject UIItemPrefab; //prefab for an empty item image in the inventory

    [Header("Properties")]
    [SerializeField] int inventorySize = 20;
    List<ItemController> items = new List<ItemController>(); //list of gameObjects for the items in the inventory

    List<UIItemController> uiItems = new List<UIItemController>(); //list of all UI items in the inventory

    GameObject inventoryUI; //UI inventory where all the inventory icons will be. Needs a Grid Layout Group component
    string inventoryUITag = "Inventory";

    public bool isInventoryOpenForInteraction = false;

    //Text for using an item for an inventory interaction
    GameObject useItemTextPopup = null; //text to show interact button
    string useItemTextTag = "UseItemText"; //tag of the textPopup object

    void Awake()
    {
        instance = this;

        inventoryUI = GameObject.FindWithTag(inventoryUITag);

        if (inventoryUI != null) {
            inventoryUI.SetActive(false);
        }

        useItemTextPopup = GameObject.FindWithTag(useItemTextTag);

        if (useItemTextPopup != null) {
            useItemTextPopup.SetActive(false);
        }
    }

    //Open / close inventory
    public void ToggleInventory()
    {
        if (!IsInventoryOpen())
        {
            EnableInventory();
        }
        else
        {
            DisableInventory();
        }
    }

    public bool IsInventoryOpen()
    {
        return inventoryUI.activeInHierarchy;
    }

    void EnableInventory()
    {
        inventoryUI.SetActive(true);
        EnableCursor();

        isInventoryOpenForInteraction = false;
    }

    void DisableInventory()
    {
        inventoryUI.SetActive(false);
        DisableCursor();

        ResetItemButtons();

        isInventoryOpenForInteraction = false;

        if (useItemTextPopup != null)
        {
            useItemTextPopup.SetActive(false);
        }
    }

    //Open inventory for item interactions
    public void OpenInventoryInteraction(IInteractable interactableItem)
    {
        if(!IsInventoryOpen())
        {
            EnableInventory();
        }

        AddItemInteractions(interactableItem);

        isInventoryOpenForInteraction = true;

        if (useItemTextPopup != null)
        {
            useItemTextPopup.SetActive(true);
        }
    }

    //Make items work as buttons for specific interactions
    void AddItemInteractions(IInteractable interactableItem)
    {
        foreach (UIItemController uiItem in uiItems)
        {
            AddItemInteraction(uiItem, interactableItem);
        }
    }

    void AddItemInteraction(UIItemController uiItem, IInteractable interactableItem)
    {
        UnityEngine.UI.Button itemButton = uiItem.GetComponent<UnityEngine.UI.Button> ();

        if (itemButton != null) {
            itemButton.onClick.AddListener(() => OnClickItem(itemButton, uiItem.itemController, interactableItem));
        }
    }

    void OnClickItem(UnityEngine.UI.Button button, ItemController itemController, IInteractable interactableItem)
    {
        //Close inventory and enable player movement
        PlayerInputs.instance.OnToggleInventory();

        //Disable button functionality
        button.onClick.RemoveAllListeners();

        //pass the pressed inventory item to the interactable item in the scene 
        if (itemController != null) {
            interactableItem.InteractWithInventory(itemController);
        }
    }

    void ResetItemButtons()
    {
        foreach (UIItemController uiItem in uiItems)
        {
            UnityEngine.UI.Button itemButton = uiItem.GetComponent<UnityEngine.UI.Button>();

            if (itemButton != null)
            {
                itemButton.onClick.RemoveAllListeners();
            }
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

    void AddItem(ItemController item)
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
            AddItem(item);

            //Disable gameObject of the item in the scene
            item.gameObject.SetActive(false);
        }
    }

    public void RemoveItem(ItemController item)
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

        RemoveItem(item);
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

            if (itemIcon != null) {
                //Set its icon
                itemIcon.sprite = itemController.item.itemIcon;
            }
            else
            {
                Debug.LogError("No Image found on UIItem");
            }

            //Reference the item controller to the inventory item
            UIItemController uiItemController = UIItem.GetComponent<UIItemController>();

            if (uiItemController != null) {
                uiItemController.SetItemController(itemController);
                uiItems.Add(uiItemController);
            }
            else
            {
                Debug.LogError("No UIItemController found on UIItem");
            }
        }

    }

    public void DestroyAllUIItems()
    {
        foreach (UIItemController uiItem in uiItems)
        {
            Destroy(uiItem.gameObject);
        }

        uiItems.Clear();
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
