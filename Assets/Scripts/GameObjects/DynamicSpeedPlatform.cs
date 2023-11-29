using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSpeedPlatform : MonoBehaviour
{
    // to make multople moving platforms move in sync this platform will calculate
    public float TranzitionTime = 5;
    private float speed;
    public float waitTime;
    private float waitCounter;
    public Transform[] checkpoints;
    public Transform currentCheckpoint;
    private int curPointIdx;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (checkpoints.Length > 0)
        {
            curPointIdx = 0;
            currentCheckpoint = checkpoints[0];
        }
        Vector3 newDir = currentCheckpoint.position - transform.position;
        speed = newDir.magnitude / TranzitionTime;
    }
    void FixedUpdate()
    {
        if (waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
            return;
        }
        if (currentCheckpoint)
        {
            Vector3 dir = currentCheckpoint.position - transform.position;
            float dist = dir.magnitude;
            if (dist > speed * Time.deltaTime)
            {
                //rb.velocity = dir.normalized * speed * Time.deltaTime;
                Vector3 p = transform.position + dir.normalized * speed * Time.deltaTime;
                //transform.position += dir.normalized * speed * Time.deltaTime;
                rb.MovePosition(p);
            }
            else
            {
                transform.position = currentCheckpoint.position;
                rb.velocity = Vector3.zero;
                waitCounter = waitTime;
                if (checkpoints.Length > 0)
                {
                    if (curPointIdx == checkpoints.Length - 1)
                        curPointIdx = 0;
                    else
                        curPointIdx++;
                    currentCheckpoint = checkpoints[curPointIdx];
                }
                Vector3 newDir = currentCheckpoint.position - transform.position;
                speed = newDir.magnitude / TranzitionTime;
            }
        }
    }
}
