using ECM.Components;
using ECM.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackHazard : MonoBehaviour
{
    public float damage = 5;
    public float power = 500;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterMovement movement = other.transform.root.GetComponent<CharacterMovement>();
        PlayerData data = other.GetComponent<PlayerData>();
        if (data && movement && data.state != PlayerForm.Ball)
        {
            Vector3 dir = other.transform.position - transform.position;
            movement.ApplyImpulse(dir.normalized * power);
            movement.DisableGrounding();
            //pl.GetComponent<Rigidbody>().velocity = dir.normalized * 15;
            data.TakeDamage(damage);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        CharacterMovement movement = collision.gameObject.GetComponent<CharacterMovement>();
        PlayerData data = collision.gameObject.GetComponent<PlayerData>();
        if (data && movement)
        {
            Vector3 dir = collision.transform.position - transform.position;
            movement.ApplyImpulse(dir.normalized * power);
            movement.DisableGrounding();
            //pl.GetComponent<Rigidbody>().velocity = dir.normalized * 15;
            data.TakeDamage(damage);
        }
    }
}
