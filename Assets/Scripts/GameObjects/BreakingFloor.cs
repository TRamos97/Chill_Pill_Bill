using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingFloor : MonoBehaviour
{
    public GameObject segmentPrefab;
    public Transform[] segmentSpawns;
    private List<GameObject> segments;
    public float breakTime = 0.75f;
    private float breakCounter;

    public float recoverTime = 5f;
    private float recoverCounter;
    private bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        segments = new List<GameObject>();
        BuildFloor();
    }
    public void BuildFloor()
    {
        foreach (var item in segmentSpawns)
        {
            segments.Add(Instantiate(segmentPrefab, item.position, item.rotation, transform));
        }
    }
    public void BreakFloor()
    {
        foreach (var item in segments)
        {
            item.GetComponent<Rigidbody>().isKinematic = false;
            Vector3 dir = item.transform.position - transform.position;
            item.GetComponent<Rigidbody>().AddForce(dir.normalized * Random.Range(50, 100));
            item.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10));
            Destroy(item, Random.Range(1f, 2.5f));
        }
        segments.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            if (breakCounter > 0)
                breakCounter -= Time.deltaTime;
            else
            {
                BreakFloor();
                recoverCounter = recoverTime;
                isTriggered = false;
            }
        }
        else
        {
            if (recoverCounter > 0)
            {
                recoverCounter -= Time.deltaTime;
            }
            else if(segments.Count == 0)
            {
                BuildFloor();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        PlayerData pl = other.transform.root.GetComponent<PlayerData>();
        if (pl & !isTriggered & segments.Count > 0)
        {
            breakCounter = breakTime;
            isTriggered = true;
        }
    }
}
