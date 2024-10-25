
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Consumable,
    Equipable,
    Resource,
}

public enum ConsumableType
{
    Health,
    Stamina,
}
[System.Serializable]
public class Consumable
{
    public ConsumableType consumableType;
    public int amount;
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Datas/ItemData")]
public class ItemData : InteractData
{
    public ItemType itemType;
    public Sprite icon;
    public bool stackable;
    public int maxStack;
    public List<Consumable> consumables;

    public GameObject equipPrefab;
}