using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float textSpeed = 1;
    public TextMeshPro txtMsg;
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, textSpeed * Time.deltaTime, 0);
    }
    public void SetText(string txt)
    {
        txtMsg.text = txt;
        //transform.LookAt(transform.position + Camera.main.transform.forward);

        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

        //transform.rotation = Camera.main.transform.rotation;
    }
}
