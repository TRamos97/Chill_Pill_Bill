using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedCollectablle : MonoBehaviour
{
    public int pointsValue = 1;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerData pl = other.transform.root.GetComponent<PlayerData>();
            pl.seedPoints += pointsValue;
            Destroy(gameObject);
        }
    }
}
