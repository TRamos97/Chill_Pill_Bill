using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Infinite Inventory")]
public class InfiniteInventoryObject : ScriptableObject
{
    public string savePath;
    public  ItemDataBase database;
    public InfiniteInventory container;
    public void AddItem(Item item, int amount)
    {
        if (!item.isStackable | item.buffs.Length > 0)
        {
            container.items.Add(new InventorySlot(item.Id, item, amount));
            return;
        }
        foreach (var slot in container.items)
        {
            if(slot.item.Id == item.Id)
            {
                slot.AddAmount(amount);
                return;
            }
        }
        container.items.Add(new InventorySlot(item.Id, item, amount));
    }

    [ContextMenu("Save")]
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
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
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        container = new InfiniteInventory();
    }
}
[System.Serializable]
public class InfiniteInventory
{
    public List<InventorySlot> items = new List<InventorySlot>();
}

//[System.Serializable]
//public class InventorySlot
//{
//    public int Id;
//    public Item item;
//    public int amount;
//    public InventorySlot(int _id, Item _item, int _amount)
//    {
//        Id = _id;
//        item = _item;
//        amount = _amount;
//    }
//    public void AddAmount(int value)
//    {
//        amount += value;
//    }
//}