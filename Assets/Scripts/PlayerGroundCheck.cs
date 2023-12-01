using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public PlayerData pc;
    private List<GameObject> colliders;
    void Start()
    {
        colliders = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        colliders.Clear();
        pc.canJump = colliders.Count > 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") | other.gameObject.CompareTag("Environment"))
        {
            if (!colliders.Contains(other.gameObject))
                colliders.Add(other.gameObject);
        }
         pc.canJump = colliders.Count > 0;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Ground") | other.gameObject.CompareTag("Environment"))
        {
            if (colliders.Contains(other.gameObject))
                colliders.Remove(other.gameObject);
        }
         pc.canJump = colliders.Count > 0;
    }
}
