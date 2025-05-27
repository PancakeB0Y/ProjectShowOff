using UnityEngine;

//Objects the player can interact with
public interface IInteractable
{
    public string interactText { get;}

    public void Interact();

    public void InteractWithInventory(ItemController inventoryItem);

}
