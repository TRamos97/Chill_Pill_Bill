using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public void Resume() 
    {
        Game.instance.TogglePause(false);
    }
    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
