using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Object", menuName = "Inventory System/Items/Tool")]
public class ToolInventoryObjet : InventoryItemObject
{
    private void Awake()
    {
        type = ItemType.Tool;
    }
}