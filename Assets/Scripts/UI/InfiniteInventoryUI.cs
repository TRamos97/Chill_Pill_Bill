using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteInventoryUI : MonoBehaviour
{
    public static InfiniteInventoryUI instance;
    public InfiniteInventoryObject inventory;
    public Transform panel;
    public Transform leftTopItemPos;
    //public GameObject slotPrefab;

    public int x_space_between_item;
    public int y_space_between_item;
    public int columns;
    private Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }
    public void CreateDisplay()
    {
        int i = 0;
        foreach (var invItem in inventory.container.items)
        {
            GameObject obj = Instantiate(inventory.database.GetItem[invItem.Id].prefab, panel);
            obj.GetComponent<RectTransform>().localPosition = leftTopItemPos.localPosition + GetPosition(i);
            obj.GetComponent<BaseUIInventorySlot>().slot = invItem;
            obj.GetComponent<BaseUIInventorySlot>().SetupSlotDisplay(inventory.database.GetItem[invItem.Id].sprite);
            itemsDisplayed.Add(invItem, obj);
            i++;
        }
    }
    public void UpdateDisplay()
    {
        int i = 0;
        foreach (var item in inventory.container.items)
        {
            if (!itemsDisplayed.ContainsKey(item))
            {
                GameObject obj = Instantiate(inventory.database.GetItem[item.Id].prefab, panel);
                obj.GetComponent<RectTransform>().localPosition = leftTopItemPos.localPosition + GetPosition(i);
                obj.GetComponent<BaseUIInventorySlot>().slot = item;
                obj.GetComponent<BaseUIInventorySlot>().SetupSlotDisplay(inventory.database.GetItem[item.Id].sprite);
                itemsDisplayed.Add(item, obj);
            }
            i++;
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(x_space_between_item * (i % columns), (-y_space_between_item * (i / columns)), 0);
    }
}
