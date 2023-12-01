using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTriggerPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private Transform currentPoint;
    public float speed;
    private Rigidbody rb;
    void Start()
    {
        currentPoint = pointA;
        rb = GetComponent<Rigidbody>();
    }
    public void SetDirectionToA()
    {
        currentPoint = pointA;
    }
    public void SetDirectionToB()
    {
        currentPoint = pointB;
    }
    void FixedUpdate()
    {
        if (currentPoint)
        {
            Vector3 dir = currentPoint.position - transform.position;
            float dist = dir.magnitude;
            if (dist > speed * Time.deltaTime)
            {
                Vector3 p = transform.position + dir.normalized * speed * Time.deltaTime;
                rb.MovePosition(p);
            }
            else
            {
                transform.position = currentPoint.position;
            }
        }
    }
}
