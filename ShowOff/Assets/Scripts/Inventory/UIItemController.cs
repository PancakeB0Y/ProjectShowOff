using UnityEngine;

//Inventory item that references the item object
public class UIItemController : MonoBehaviour
{
    [HideInInspector] public ItemController itemController { get; private set; } = null;

    public void SetItemController(ItemController itemController)
    {
        this.itemController = itemController;
    }
}
