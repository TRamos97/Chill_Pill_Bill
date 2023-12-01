using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
public class DefaultInventoryObject : InventoryItemObject
{
    private void Awake()
    {
        type = ItemType.Default;
    }
}
