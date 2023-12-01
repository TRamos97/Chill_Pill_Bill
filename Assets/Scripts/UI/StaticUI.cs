using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticUI : InventoryUI
{
    public BaseUIInventorySlot[] slots;
    public override void CreateSlots()
    {
        itemsDisplayed = new Dictionary<BaseUIInventorySlot, InventorySlot>();
        for (int i = 0; i < inventory.container.items.Length; i++)
        {
            var obj = slots[i];

            AddEvent(obj.gameObject, EventTriggerType.PointerEnter, delegate { OnPointerEnter(obj.gameObject); });
            AddEvent(obj.gameObject, EventTriggerType.PointerExit, delegate { OnPointerExit(obj.gameObject); });
            AddEvent(obj.gameObject, EventTriggerType.BeginDrag, delegate { OnBeginDrag(obj.gameObject); });
            AddEvent(obj.gameObject, EventTriggerType.EndDrag, delegate { OnEndDrag(obj.gameObject); });
            AddEvent(obj.gameObject, EventTriggerType.Drag, delegate { OnDrag(obj.gameObject); });
            AddEvent(obj.gameObject, EventTriggerType.PointerClick, delegate { OnPointerClick(obj.gameObject); });

            itemsDisplayed.Add(obj, inventory.container.items[i]);
        }
    }
}
