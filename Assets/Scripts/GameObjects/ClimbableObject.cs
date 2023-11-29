using ECM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HookState { None, Hold, Relese}
public class ClimbableObject : MonoBehaviour
{
    private PlayerData player;
    public Transform attachPoint;
    public HookState state = HookState.None;
    void Start()
    {
        // hide mesh when in game
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (player && player.state != PlayerForm.Normal)
        //{
        //    player.GetComponent<Rigidbody>().isKinematic = false;
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (state != HookState.None || !enabled)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter");
            PlayerData pl = other.transform.root.GetComponent<PlayerData>();
            if (pl && pl.state == PlayerForm.Normal)
            {
                if (pl.climbableObject != this)
                {
                    player = pl;
                    pl.climbableObject = this;
                    pl.transform.position = attachPoint.position;
                    pl.GetComponent<Rigidbody>().isKinematic = true;
                    state = HookState.Hold;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (state != HookState.Relese || !enabled)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerData pl = other.transform.root.GetComponent<PlayerData>();
            if (pl.climbableObject == this)
            {
                StartCoroutine(Release());
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!enabled || !player)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            RaycastHit hit;

            if (player.state == PlayerForm.Ball || !Physics.Raycast(player.transform.position, player.transform.forward, out hit, 2.0f))
            {
                player.GetComponent<Rigidbody>().isKinematic = false;
                player.climbableObject.state = HookState.Relese;
                StartCoroutine(Release());
            }
        }
    }
    private IEnumerator Release()
    {
        // changing rigifbody.isKinematic activates OnTriggerEnter/Exit events
        Debug.Log("OnTriggerExit");
        player.climbableObject = null;
        player = null;
        state = HookState.None;
        this.enabled = false;
        float time = 0;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        this.enabled = true;
    }
}
