using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager instance;
    public GameObject SoundEffect3dPrefab;
    public GameObject floatTextPrefab;
    private void Awake()
    {
        instance = this;
    }
    public void MakeSound3d(Vector3 pos, AudioClip clip)
    {
        GameObject go = Instantiate(SoundEffect3dPrefab, pos, Quaternion.identity);
        go.GetComponent<SoundEffect3d>().SetClip(clip);
    }
    public void MakeFloatingText(Vector3 pos, string text)
    {
        FloatingText floatText = Instantiate(floatTextPrefab, pos, Quaternion.Euler(0, 0, 0)).GetComponent<FloatingText>();
        floatText.SetText(text);
    }
}
