using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    PlayerData player;
    public string displayMessage;
    public UnityEvent action;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerData pl = other.transform.root.gameObject.GetComponent<PlayerData>();
            if(pl)
                pl.SetInteractable(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerData pl = other.transform.root.gameObject.GetComponent<PlayerData>();
            if (pl)
                pl.RemoveInteractable(this);
        }
    }

    internal void Interact()
    {
        if (action != null)
            action.Invoke();    
    }
    public void DisableObject()
    {
        FindObjectOfType<PlayerData>().RemoveInteractable(this);
        Destroy(this);
    }
}
