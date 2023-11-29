using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEffects", menuName = "Game Systems/PlayerEffects")]
public class PlayerEffects : ScriptableObject
{
    public AudioClip soundJump;
    public AudioClip soundTransform;
    public AudioClip soundLanding;
    public GameObject transformParticles;
}
