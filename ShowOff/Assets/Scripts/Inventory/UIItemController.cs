using UnityEngine;

//Inventory item that references the item object
public class UIItemController : MonoBehaviour
{
     public ItemController itemController = null;

    public void SetItemController(ItemController itemController)
    {
        this.itemController = itemController;
    }
}
