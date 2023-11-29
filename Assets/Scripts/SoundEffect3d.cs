using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect3d : MonoBehaviour
{
    public GameSettings settigns;
    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = settigns.SoundEffectsVolume;
    }
    void Update()
    {
        if (!audioSource.isPlaying)
            Destroy(gameObject);
    }
    public void SetClip(AudioClip val)
    {
        audioSource.clip = val;
        audioSource.Play();
    }
    public void Reuse()
    {

    }
}
