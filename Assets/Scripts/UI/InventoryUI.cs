using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    public InventoryObject inventory;
    public Transform panel;
    public GameObject slotPrefab;
    public MouseUIItem mouseItem;
    public TextMeshProUGUI descriptionText;

    protected Dictionary<BaseUIInventorySlot, InventorySlot> itemsDisplayed = new Dictionary<BaseUIInventorySlot, InventorySlot>();
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        instance = this;
    }
    void Start()
    {
        foreach (var item in inventory.container.items)
            item.parent = this;
        CreateSlots();
    }
    void Update()
    {
        UpdateSlots();
    }
    public void UpdateSlots()
    {
        foreach (KeyValuePair<BaseUIInventorySlot, InventorySlot> slot in itemsDisplayed)
        {
            if(slot.Value.Id >= 0)
            {
                slot.Key.slot = slot.Value;
                slot.Key.SetupSlotDisplay(inventory.database.GetItem[slot.Value.item.Id].sprite);
            }
            else
            {
                slot.Key.slot = null;
                slot.Key.SetupSlotDisplay(null);
            }
        }
    }
    public virtual void CreateSlots()
    {
    }
    public void OnPointerEnter(GameObject obj)
    {
        Debug.Log("OnPointerEnter");
        BaseUIInventorySlot slot = obj.GetComponent<BaseUIInventorySlot>();
        mouseItem.howerEndSlot = itemsDisplayed[slot];
    }
    public void OnPointerExit(GameObject obj)
    {
        Debug.Log("OnPointerExit");
        mouseItem.howerEndSlot = null;
    }
    public void OnBeginDrag(GameObject obj)
    {
        BaseUIInventorySlot slot = obj.GetComponent<BaseUIInventorySlot>();
        mouseItem.gameObject.SetActive(true);
        if (itemsDisplayed[slot].Id >= 0)
            mouseItem.image.sprite = inventory.database.GetItem[itemsDisplayed[slot].Id].sprite;
        else
            mouseItem.image.sprite = null;
        mouseItem.howerStartSlot = itemsDisplayed[slot];
    }
    public void OnEndDrag(GameObject obj)
    {
        mouseItem.gameObject.SetActive(false);

        if(mouseItem.howerStartSlot != null & mouseItem.howerEndSlot != null & mouseItem.howerStartSlot != mouseItem.howerEndSlot)
        {
            if(mouseItem.howerEndSlot.CanPlaceInSlot(inventory.database.GetItem[mouseItem.howerStartSlot.Id]) 
                    && (mouseItem.howerEndSlot.Id <= -1 || (mouseItem.howerEndSlot.Id >= 0 && mouseItem.howerStartSlot.CanPlaceInSlot(inventory.database.GetItem[mouseItem.howerEndSlot.Id]))))
                inventory.MoveItem(mouseItem.howerStartSlot, mouseItem.howerEndSlot);
        } 
        else if (mouseItem.howerStartSlot != null & mouseItem.howerEndSlot == null)
        {
            //inventory.RemoveItem(mouseItem.howerStartSlot); not needed for game
        }
    }
    public InventorySlot GetUISlot(InventorySlot slot)
    {
        return null;
    }
    public void OnDrag(GameObject obj)
    {
        if (mouseItem.isActiveAndEnabled)
            mouseItem.transform.position = Input.mousePosition;
    }   
    public void OnPointerClick(GameObject obj)
    {
        if (descriptionText)
        {
            BaseUIInventorySlot UIslot = obj.GetComponent<BaseUIInventorySlot>();
            InventoryItemObject item = inventory.database.GetItem[UIslot.slot.Id];
            descriptionText.text = item.description;
        }
    }
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponentInChildren<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
}
