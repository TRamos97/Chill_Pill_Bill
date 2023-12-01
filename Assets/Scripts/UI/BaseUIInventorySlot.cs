using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUIInventorySlot : MonoBehaviour
{
    [HideInInspector]
    public InventorySlot slot;
    //public Sprite displaySprite;
    public Image image;
    public virtual void UpdateDisplay()
    {
    }
    public void SetupSlotDisplay(Sprite displaySprite)
    {
        image.sprite = displaySprite;
    }
}
