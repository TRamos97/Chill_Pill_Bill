using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void SetProgressValue(float value)
    {
        slider.value = value;
    }
}
