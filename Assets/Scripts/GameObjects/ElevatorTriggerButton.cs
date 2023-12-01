using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTriggerButton : MonoBehaviour
{
    public ElevatorTriggerPlatform platform;
    void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platform.SetDirectionToB();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platform.SetDirectionToA();
        }
    }
}
