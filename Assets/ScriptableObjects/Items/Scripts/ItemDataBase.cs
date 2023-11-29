using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDataBase : ScriptableObject, ISerializationCallbackReceiver
{
    public InventoryItemObject[] Items;
    public Dictionary<int, InventoryItemObject> GetItem = new Dictionary<int, InventoryItemObject>();

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].Id = i;
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, InventoryItemObject>();
    }
}
