using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarController : MonoBehaviour, IInteractable
{
    public string interactText { get; } = "Press [E] to interact";

    [SerializeField] private float secondsWaitBeforeLightsBackOn = 1.0f;
    [SerializeField] private float secondsWaitBeforeStatueDestroy = 1.0f;

    [SerializeField] Transform[] itemSlots = new Transform[3]; //positions where the items will be placed
    Dictionary<ItemType, bool> isItemTypePlaced = new Dictionary<ItemType, bool>()
    {
        {ItemType.Chalice, false},
        {ItemType.HolyOil, false},
        {ItemType.Rosary, false}
    };

    //Open / close door
    public virtual void Interact()
    {
        if (PlayerInputs.instance != null) {
            PlayerInputs.instance.OnOpenInventoryInteraction(this);
        }
    }

    public virtual void InteractWithInventory(ItemController inventoryItem) {
        if (InventoryManager.instance == null) {
            return;
        }

        if (inventoryItem.item.itemType == ItemType.Chalice || inventoryItem.item.itemType == ItemType.HolyOil || inventoryItem.item.itemType == ItemType.Rosary)
        {
            PlaceItem(inventoryItem);

            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayInventoryItemSelected();
            }

            if (areAllRitualItemsPlaced())
            {
                PerformRitual();
            }
        }
        else
        {
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayWrongItemChosen();
            }
        }
    }

    void PlaceItem(ItemController itemController) {
        ItemType currentItemType = itemController.item.itemType;
        if (!isItemTypePlaced[currentItemType])
        {
            Vector3 dropPosition;

            switch (currentItemType)
            {
                case ItemType.Chalice:
                    dropPosition = itemSlots[0].position;
                    break;
                case ItemType.HolyOil:
                    dropPosition = itemSlots[1].position;
                    break;
                case ItemType.Rosary:
                    dropPosition = itemSlots[2].position;
                    break;
                default:
                    dropPosition = transform.position;
                    break;
            }

            InventoryManager.instance.DropItem(itemController, dropPosition);

            isItemTypePlaced[currentItemType] = true;

            //Disable the item so the player cannot interact with the object
            itemController.DisableInteractions();
        }
    }

    bool areAllRitualItemsPlaced()
    {
        foreach(bool value in isItemTypePlaced.Values)
        {
            if (!value)
            {
                return false;
            }
        }

        return true;
    }

    void PerformRitual()
    {
        Debug.Log("Perform Ritual");

        NavMeshMoveBehaviour statue = FindFirstObjectByType<NavMeshMoveBehaviour>(FindObjectsInactive.Include);
        statue.UpdateSpawnRanges();

        //TURN ALL LIGHTS OFF
        LightSourceController[] lights = FindObjectsByType<LightSourceController>(FindObjectsSortMode.None);

        foreach (LightSourceController light in lights) {
            light.TurnLightOff();
        }

        StartCoroutine(TurnLightsBackOn(lights, statue));
    }

    IEnumerator TurnLightsBackOn(LightSourceController[] lights, NavMeshMoveBehaviour statue)
    {
        yield return new WaitForSeconds(secondsWaitBeforeLightsBackOn);

        foreach (LightSourceController light in lights)
        {
            light.TurnLightOn();
        }

        StartCoroutine(DestroyStatue(statue));
    }

    IEnumerator DestroyStatue(NavMeshMoveBehaviour statue)
    {
        yield return new WaitForSeconds(secondsWaitBeforeStatueDestroy);

        statue.GetDestroyed();

        if (TutorialManager.Instance != null) {
            TutorialManager.Instance.SetTutorialIndex(5);
        }
    }
}
