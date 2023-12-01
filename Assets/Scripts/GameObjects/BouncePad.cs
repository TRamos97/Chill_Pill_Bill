using ECM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float power = 3000f;
    public Transform bouceDirection;
    public AudioClip[] bounceSounds;
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
            EffectsManager.instance.MakeSound3d(transform.position, bounceSounds[Random.Range(0, bounceSounds.Length)]);
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
