using ECM.Components;
using ECM.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerPad : MonoBehaviour
{
    public float power = 3000f;
    public Transform bouceDirection;
    public AudioClip[] bounceSounds;
    public float damage = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterMovement movement = other.GetComponent<CharacterMovement>();
        //PlayerData pl = other.transform.root.GetComponent<PlayerData>();
        if (movement)
        {
            Vector3 dir = bouceDirection.position - transform.position;
            //pl.AddForce(dir.normalized * power);
            movement.ApplyImpulse(dir.normalized * power);
            movement.DisableGrounding();
            PlayerData data = other.GetComponent<PlayerData>();
            data.TakeDamage(damage);

        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    CharacterMovement movement = other.GetComponent<CharacterMovement>();
    //    //PlayerData pl = other.transform.root.GetComponent<PlayerData>();
    //    if (movement)
    //    {
    //        Vector3 dir = bouceDirection.position - transform.position;
    //        //pl.AddForce(dir.normalized * power);
    //        movement.ApplyImpulse(Vector3.up * power);
    //        movement.DisableGrounding();
    //    }
    //}
}
