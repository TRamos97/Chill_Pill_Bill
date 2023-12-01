using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCharacterController : MonoBehaviour
{
    private Vector3 facingDirection;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        facingDirection = transform.forward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
