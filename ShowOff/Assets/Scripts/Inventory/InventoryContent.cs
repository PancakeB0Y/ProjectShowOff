using NUnit.Framework;
using UnityEngine;

[System.Serializable]
public class InventoryContent
{
    public ItemAndAmount[] items;
}

[System.Serializable]
public struct ItemAndAmount
{
    public ItemType itemType;
    public int amount;
}
