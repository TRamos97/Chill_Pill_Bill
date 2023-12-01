using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneVolumeCheck : MonoBehaviour
{
    public GameSettings settigns;
    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = settigns.MusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
