using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    public float rotationSpeed = 30.0f;
    private Rigidbody rb;
    // leave 0 at axis you don't want to spin
    public Vector3 rotationAxis;
    private Vector3 currentRotation;
    private Vector3 rotationStep;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentRotation = rb.rotation.eulerAngles;
        rotationStep = rotationAxis * rotationSpeed * Time.deltaTime;
        rb.isKinematic = true;
    }
    public void FixedUpdate()
    {
        currentRotation += rotationStep;

        var rotation = Quaternion.Euler(currentRotation);
        rb.MoveRotation(rotation);
    }
}
