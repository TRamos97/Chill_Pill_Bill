using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float maxSpeed = 5;
    public float acceleration = 1;
    private float speed;
    public float waitTime;
    private float waitCounter;
    public Transform[] checkpoints;
    public Transform currentCheckpoint;
    private int cuurPointIdx;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (checkpoints.Length > 0)
        {
            cuurPointIdx = 0;
            currentCheckpoint = checkpoints[0];
        }
    }
    void FixedUpdate()
    {
        if(waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
            return;
        }
        if(currentCheckpoint)
        {
            Vector3 dir = currentCheckpoint.position - transform.position;
            float dist = dir.sqrMagnitude;
            if(dist > speed * speed * Time.deltaTime)
            {
                //rb.velocity = dir.normalized * speed * Time.deltaTime;
                Vector3 p = transform.position + dir.normalized * speed * Time.deltaTime;
                //transform.position += dir.normalized * speed * Time.deltaTime;
                rb.MovePosition(p);
                if (speed < maxSpeed)
                    speed = Mathf.Min(maxSpeed, speed + acceleration * Time.deltaTime);
            }
            else
            {
                //transform.position = currentCheckpoint.position;
                rb.velocity = Vector3.zero;
                speed = 0;
                waitCounter = waitTime;
                if(checkpoints.Length > 0)
                {
                    if (cuurPointIdx == checkpoints.Length - 1)
                        cuurPointIdx = 0;
                    else
                        cuurPointIdx++;
                    currentCheckpoint = checkpoints[cuurPointIdx];
                }
            }
        }
    }
}
