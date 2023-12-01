using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicUI : InventoryUI
{
    public Transform leftTopItemPos;
    public int x_space_between_item;
    public int y_space_between_item;
    public int columns;
    public override void CreateSlots()
    {
        itemsDisplayed = new Dictionary<BaseUIInventorySlot, InventorySlot>();
        for (int i = 0; i < inventory.container.items.Length; i++)
        {
            BaseUIInventorySlot obj = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, panel).GetComponent<BaseUIInventorySlot>();
            obj.GetComponent<RectTransform>().localPosition = leftTopItemPos.localPosition + GetPosition(i);

            AddEvent(obj.gameObject, EventTriggerType.PointerEnter, delegate { OnPointerEnter(obj.gameObject); });
            AddEvent(obj.gameObject, EventTriggerType.PointerExit, delegate { OnPointerExit(obj.gameObject); });
            AddEvent(obj.gameObject, EventTriggerType.BeginDrag, delegate { OnBeginDrag(obj.gameObject); });
            AddEvent(obj.gameObject, EventTriggerType.EndDrag, delegate { OnEndDrag(obj.gameObject); });
            AddEvent(obj.gameObject, EventTriggerType.Drag, delegate { OnDrag(obj.gameObject); });
            AddEvent(obj.gameObject, EventTriggerType.PointerClick, delegate { OnPointerClick(obj.gameObject); });

            itemsDisplayed.Add(obj, inventory.container.items[i]);
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(x_space_between_item * (i % columns), (-y_space_between_item * (i / columns)), 0);
    }
}
