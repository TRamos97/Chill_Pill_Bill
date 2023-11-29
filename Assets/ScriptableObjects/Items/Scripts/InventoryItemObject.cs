using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Hat,
    Tool,
    Boots,
    Consumable,
    Default
}
public enum ItemAttributes
{
    attr1,
    attr2,
    attr3
}
public abstract class InventoryItemObject : ScriptableObject
{
    public int Id;
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    public Sprite sprite;
    public bool isStackable = true;
    public ItemBuff[] buffs;

    public Item CreateItem()
    {
        Item itm = new Item(this);
        return itm;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;
    public bool isStackable;
    //public Sprite sprite;
    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(InventoryItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        isStackable = item.isStackable;
        //sprite = item.sprite;
        buffs = new ItemBuff[item.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);
            buffs[i].attribute = item.buffs[i].attribute;
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public ItemAttributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }
    public void GenerateValue()
    {
        value = Random.Range(min, max);
    }
}