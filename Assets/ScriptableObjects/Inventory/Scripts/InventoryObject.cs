using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public  ItemDataBase database;
    public Inventory container;
    public void AddItem(Item item, int amount)
    {
        if (!item.isStackable | item.buffs.Length > 0)
        {
            GetEmptySlot(item, amount);
            return;
        }
        foreach (var slot in container.items)
        {
            if (slot.Id == item.Id)
            {
                slot.AddAmount(amount);
                return;
            }
        }
        GetEmptySlot(item, amount);
    }
    public InventorySlot GetEmptySlot(Item _item, int amount)
    {
        foreach (var item in container.items)
        {
            if(item.Id <= -1)
            {
                item.UpdateSlot(_item.Id, _item, amount);
                return item;
            }
        }
        // Inventory is full!
        return null;
    }
    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        Debug.Log("Move Item");
        InventorySlot tmp = new InventorySlot(item2.Id, item2.item, item2.amount);
        item2.UpdateSlot(item1.Id, item1.item, item1.amount);
        item1.UpdateSlot(tmp.Id, tmp.item, tmp.amount);
    }
    public void RemoveItem(InventorySlot item)
    {
        Debug.Log("RemoveItem");
        item.UpdateSlot(-1, null, 0);
    }

    [ContextMenu("Save")]
    public void Save()
    {
        string saveData = JsonUtility.ToJson(container, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), container);
            file.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        container.Clear();
    }
}
[System.Serializable]
public class Inventory
{
    //public List<InventorySlot> items = new List<InventorySlot>();
    public InventorySlot[] items = new InventorySlot[25];
    public void Clear()
    {
        foreach (var item in items)
        {
            item.UpdateSlot(-1, new Item(), 0);
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    public InventoryUI parent;
    public int Id = -1;
    public Item item;
    public int amount;
    public InventorySlot()
    {
        Id = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, Item _item, int _amount)
    {
        Id = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        Id = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public bool CanPlaceInSlot(InventoryItemObject _item)
    {
        if (AllowedItems.Length == 0)
            return true;
        foreach (var item in AllowedItems)
        {
            if (_item.type == item)
                return true;
        }
        return false;
    }
}