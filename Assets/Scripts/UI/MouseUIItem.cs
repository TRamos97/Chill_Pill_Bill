using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseUIItem : MonoBehaviour
{
    public static MouseUIItem instance;
    public Image image;
    public  InventorySlot howerStartSlot;
    public InventorySlot howerEndSlot;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
