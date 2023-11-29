using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Material glowActive;
    public Material glowInactive;
    public GameObject indicator;
    public Light lightSource;
    void Start()
    {

    }
    public void Activate()
    {
        indicator.GetComponent<MeshRenderer>().material = glowActive;
        lightSource.color = Color.green;
    }
    public void Deactivate()
    {
        indicator.GetComponent<MeshRenderer>().material = glowInactive;
        lightSource.color = Color.yellow;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerData pl = other.transform.root.GetComponent<PlayerData>();
            pl.SetCheckpoint(this);
        }
    }
}
