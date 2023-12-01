using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Resume() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1; //Resume time
        Game.instance.TogglePause(false);
    }
    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
