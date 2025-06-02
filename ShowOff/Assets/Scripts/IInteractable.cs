using UnityEngine;

//Objects the player can interact with
public interface IInteractable
{
    public string interactText { get;}

    public void Interact();

    //Is called when the interactable has interactions with an inventory item
    public void InteractWithInventory(ItemController inventoryItem);

}
