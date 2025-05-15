using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    public itemType itemType;
    public string itemName;
    public Sprite icon;
}

public enum itemType { Knife, Gloves, Pan, Blood, DepressedSoul, Pasta, Beef, Sauce, Limbs, Wings, Spaghetti, Heart, Milk, Steak, VigorSalad, LimbBurger, Lasagna, MilkWings }