using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] GameObject UIItemPrefab; //prefab for an empty item image in the inventory
    [SerializeField] GameObject MatchstickPrefab;
    [SerializeField] ItemType[] startInventoryContent;

    [Header("Properties")]
    [SerializeField] int inventorySize = 20;
    List<ItemController> items = new List<ItemController>(); //list of gameObjects for the items in the inventory

    List<UIItemController> uiItems = new List<UIItemController>(); //list of all UI items in the inventory

    GameObject inventoryUI; //UI inventory where all the inventory icons will be. Needs a Grid Layout Group component
    string inventoryUITag = "Inventory";

    [HideInInspector] public bool isInventoryOpen = false;
    [HideInInspector] public bool isInventoryOpenForInteraction = false;
    [HideInInspector] public bool isInspectingItem = false;

    ItemController inspectedItem = null;

    //Text for using an item for an inventory interaction
    GameObject useItemTextPopup = null; //text to show interact button
    string useItemTextTag = "UseItemText"; //tag of the textPopup object

    public event Action<ItemController> OnPickupItem;
    public event Action<ItemController> OnRemoveItem;

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

    private void Start()
    {
        ParseStartingInventory();
    }

    //Open / close inventory
    public void ToggleInventory()
    {
        if (!isInventoryOpen)
        {
            EnableInventory();
        }
        else
        {
            DisableInventory();
        }
    }

    //Make inventory visible in the scene
    void EnableInventory()
    {
        inventoryUI.SetActive(true);
        isInventoryOpen = true;

        EnableCursor();

        UpdateInventoryButtons();

        isInventoryOpenForInteraction = false;
    }

    //Hide inventory in the scene
    public void DisableInventory()
    {
        inventoryUI.SetActive(false);
        isInventoryOpen = false;

        DisableCursor();

        ResetInventoryButtons();

        isInventoryOpenForInteraction = false;

        if (useItemTextPopup != null)
        {
            useItemTextPopup.SetActive(false);
        }
    }

    //Open inventory for item interactions
    public void OpenInventoryInteraction(IInteractable interactableItem)
    {
        if(!isInventoryOpen)
        {
            EnableInventory();
        }

        // remove previous button interaction
        ResetInventoryButtons();

        // add button interaction related to the interactableItem
        AddItemInteractions(interactableItem);

        isInventoryOpenForInteraction = true;

        if (useItemTextPopup != null)
        {
            useItemTextPopup.SetActive(true);
        }
    }

    //Set an onClick function to the buttons of every inventory item
    //The function is to used to interact with other objects (locked doors, altar, etc)
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

    //The function is to used to interact with other objects (locked doors, altar, etc)
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

    //Set an onClick function to the buttons of every inventory item
    //The function opens the item for inspection
    void UpdateInventoryButtons()
    {
        foreach (UIItemController uiItem in uiItems)
        {
            UpdateItemButton(uiItem);
        }
    }

    void UpdateItemButton(UIItemController uiItem)
    {
        UnityEngine.UI.Button itemButton = uiItem.GetComponent<UnityEngine.UI.Button>();

        if (itemButton != null)
        {
            itemButton.onClick.AddListener(() => OnClickInspect(itemButton, uiItem.itemController));
        }
    }

    //Button function to inspect item when clicked
    void OnClickInspect(UnityEngine.UI.Button button, ItemController itemController)
    {
        //Close inventory and enable player movement
        //PlayerInputs.instance.OnToggleInventory();

        //Disable button functionality
        //button.onClick.RemoveAllListeners();

        isInspectingItem = true;

        //pass the pressed inventory item to the interactable item in the scene 
        if (itemController != null)
        {
            inspectedItem = itemController;

            Rotatable rotatable = itemController.gameObject.GetComponent<Rotatable>();

            if (rotatable != null)
            {
                //Show item and place it in front of the player
                itemController.gameObject.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * rotatable.distanceFromCamera);

                //rotate to face the camera
                Vector3 lookDirection = itemController.gameObject.transform.position - transform.position;
                itemController.gameObject.transform.rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(35, 0, 0);

                rotatable.enabled = true;
            }

            itemController.gameObject.SetActive(true);
            inventoryUI.SetActive(false);

        }
    }

    public void StopInspectingItem()
    {
        if(inspectedItem != null)
        {
            inspectedItem.gameObject.SetActive(false);

            if (inspectedItem.TryGetComponent<Rotatable>(out Rotatable rotatable))
            {
                rotatable.enabled = false;
            }
        }

        inventoryUI.SetActive(true);
        isInspectingItem = false;
    }

    //Clear button function from all buttons
    void ResetInventoryButtons()
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

    //Add item to inventory
    void AddItem(ItemController itemController)
    {
        if (IsInventoryFull())
        {
            return;
        }

        items.Add(itemController);

        UpdateItems();
    }

    public void PickupItem(ItemController itemController, bool playSound = true)
    {
        if (!IsInventoryFull())
        {
            //Add item to inventory
            AddItem(itemController);

            if (SoundManager.instance != null && playSound)
            {
                SoundManager.instance.PlayItemPickupSound(gameObject);
            }

            //Disable gameObject of the item in the scene
            itemController.gameObject.SetActive(false);

            OnPickupItem?.Invoke(itemController); 
        }
    }

    //Remove item from inventory
    public void RemoveItem(ItemController itemController)
    {
        if (items.Contains(itemController))
        {
            items.Remove(itemController);

            UpdateItems();

            OnRemoveItem?.Invoke(itemController);
        }
    }

    public void DropFirstItem()
    {
        if (items.Count > 0)
        {
            DropItemForward(items[0]);
        }
    }

    //Drop item in front of player
    void DropItemForward(ItemController itemController)
    {
        //Calculate postition in front of player
        float dropDistance = 1.2f; //distance to drop from the player
        Vector3 dropPosition = transform.position + transform.forward * dropDistance;

        DropItem(itemController, dropPosition);
    }

    public void DropItem(ItemController itemController, Vector3 dropPosition)
    {
        if (!items.Contains(itemController))
        {
            return;
        }

        itemController.gameObject.transform.position = dropPosition;
        itemController.gameObject.SetActive(true);

        RemoveItem(itemController);
    }

    //Update items in the inventory UI
    public void UpdateItems()
    {
        if(inventoryUI == null)
        {
            return;
        }

        DestroyAllUIItems();

        var stackableItems = new List<ItemController>();

        //Create a UI object for each inventory item
        foreach (ItemController itemController in items)
        {
            if (!itemController.item.stackable)
            {
                AddUIItem(itemController);
            }
            else if (!DoesListContainItemController(stackableItems, itemController)) 
            { //make sure to not add the same stackable item multiple times
                stackableItems.Add(itemController);
                AddUIItem(itemController);
            }
        }
    }

    bool DoesListContainItemController(List<ItemController> itemList, ItemController item)
    {
        return itemList.Any(ic => ic.item == item.item);
    }

    //Create a UI object for item
    void AddUIItem(ItemController itemController)
    {
        GameObject UIItem = Instantiate(UIItemPrefab, inventoryUI.transform);

        //Add the icon of the item
        var itemIcon = UIItem.GetComponent<UnityEngine.UI.Image>();
        if (itemIcon != null)
        {
            //Set its icon
            itemIcon.sprite = itemController.item.itemIcon;
        }
        else
        {
            Debug.LogError("No Image found on UIItem");
        }

        //Reference the item controller to the inventory item
        UIItemController uiItemController = UIItem.GetComponent<UIItemController>();
        if (uiItemController != null)
        {
            uiItemController.SetItemController(itemController);
            uiItems.Add(uiItemController);
        }
        else
        {
            Debug.LogError("No UIItemController found on UIItem");
        }

        //Add the count of the item
        var itemCount = UIItem.GetComponentInChildren<TextMeshProUGUI>();
        if (itemCount != null)
        {
            if (itemController.item.stackable) {
                itemCount.text = GetItemCount(itemController).ToString();

                itemCount.gameObject.SetActive(true);
            }
            else
            {
                itemCount.gameObject.SetActive(false);
            } 
        }
        else
        {
            Debug.LogError("No item count text found on UIItem");
        }
    }

    //Get the amount of this item in the inventory
    int GetItemCount(ItemController itemController)
    {
        int itemCount = 0;

        if (!items.Contains(itemController))
        {
            return 0;
        }

        foreach (ItemController currentItemController in items) {
            if(currentItemController.IsEqual(itemController))
            {
                itemCount++;
            }
        }

        return itemCount;
    }

    //Remove all items from the inventory UI
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

    //Use a matchstick if there is one in inventory
    //Returns false if there is no matchstick in inventory
    public bool UseMatchstick()
    {
        foreach(ItemController itemController in items)
        {
            if (itemController.item.itemType == ItemType.Matchstick)
            {
                RemoveItem(itemController);
                Destroy(itemController.gameObject);
                return true;
            }
        }

        return false;
    }

    public int GetMatchstickCount()
    {
        int count = 0;

        foreach (ItemController itemController in items)
        {
            if (itemController.item.itemType == ItemType.Matchstick)
            {
                count++;
            }
        }

        return count;
    }

    //Fill the inventory according to startInventoryContent
    void ParseStartingInventory()
    {
        if(startInventoryContent == null || startInventoryContent.Length == 0|| MatchstickPrefab == null)
        {
            return;
        }

        foreach(ItemType itemType in startInventoryContent)
        {
            if(itemType == ItemType.Matchstick)
            {
                GameObject newMatchstick = Instantiate(MatchstickPrefab);

                if(newMatchstick.TryGetComponent<ItemController>(out ItemController newMatchstickController))
                {
                    PickupItem(newMatchstickController, false);
                }
            }
        }
        
    }
}
