using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StackUIInventorySlot : BaseUIInventorySlot
{
    public TextMeshProUGUI amountText;
    void Start()
    {
        
    }
    private void Update()
    {
        UpdateDisplay();
    }

    public override void UpdateDisplay()
    {
        if (slot != null)
        {
            if(slot.amount > 1)
                amountText.text = slot.amount.ToString();
            else
                amountText.text = "";
        }
        else
            amountText.text = "";
    }
}
