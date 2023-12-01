using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFormMovement : MonoBehaviour
{
    public Vector3 moveDirection;
    public float maxSpeed = 8;
    public float speed = 5.0f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //Debug.Log(moveDirection);
        Vector3 dir = new Vector3(moveDirection.x, 0, moveDirection.z);
        rb.AddForce(dir.normalized * speed);
        //rb.angularVelocity = new Vector3(rb.velocity.z, rb.velocity.y, -rb.velocity.x);
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}
