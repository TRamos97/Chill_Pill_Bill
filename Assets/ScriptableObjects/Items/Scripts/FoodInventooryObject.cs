using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodInventooryObject : InventoryItemObject
{
    public int recoveredHealth;
    private void Awake()
    {
        type = ItemType.Consumable;
    }

}
