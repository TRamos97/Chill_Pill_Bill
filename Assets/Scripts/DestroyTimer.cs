using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public GameObject item;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SelfDestruct();

    }

    private void SelfDestruct()
    {
        Destroy(item, 5); //you have 7 seconds before the objects deletes itself
    }
}