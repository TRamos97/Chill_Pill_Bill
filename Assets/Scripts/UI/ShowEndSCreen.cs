using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEndSCreen : MonoBehaviour
{
    // could also de done with a collider and OnTriggerEnter()
    public void FinishGame()
    {
        Game.instance.ShowEndScreen();
    }
}
