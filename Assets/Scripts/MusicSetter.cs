using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSetter : MonoBehaviour
{
    public AudioSource audioSource;
    public GameSettings settings;
    void Start()
    {
        audioSource.volume = settings.MusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
