using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    public itemType itemType;
    public Sprite itemIcon;
    public bool stackable = false;
}

public enum itemType { Chalice, Rosary, HolyOil, Matchstick, Key}