using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TailBone : MonoBehaviour
{
    public Transform followObj;
    public PlayerData pd;
    private float tailY;
    private Vector3 startOffset;
    public TextMeshPro txt; 

    void Start()
    {
        startOffset = transform.localPosition;
        transform.SetParent(null);
    }
    void Update()
    {
        float angle;
        if (pd.facingDirection.x > 0)
            angle = Vector3.Angle(Vector3.forward, pd.facingDirection);
        else
            angle = 360 - Vector3.Angle(Vector3.forward, pd.facingDirection);

        Vector3 newOffset = Quaternion.Euler(0, angle, 0) * startOffset;
        transform.position = followObj.position + newOffset - Vector3.up * .5f;
        transform.rotation = Quaternion.LookRotation(pd.facingDirection, Vector3.up);
        transform.Rotate(-41, 0, 0);
    }
}
