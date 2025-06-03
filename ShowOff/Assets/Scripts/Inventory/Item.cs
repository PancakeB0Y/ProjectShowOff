using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public Sprite itemIcon;
    public bool stackable = false;
}

public enum ItemType { Chalice, Rosary, HolyOil, Matchstick, Key}