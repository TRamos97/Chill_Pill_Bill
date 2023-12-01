using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public InventoryItemObject item;
    public int amount = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerData pl = other.transform.root.GetComponent<PlayerData>();
            if (item)
            {
                pl.AddToInventory(item, amount);
            }
            else
                pl.RestoreHealth(10);
            Destroy(gameObject);
        }
    }
}
