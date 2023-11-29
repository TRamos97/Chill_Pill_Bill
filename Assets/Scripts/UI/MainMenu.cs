using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject credits;
    public GameObject settings;
    public GameSettings gameSettings;

    public ProgressBar barSoundEffects;
    public ProgressBar barMusic;
    private void Start()
    {
        gameSettings.Load();
    }
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ToggleSettings(bool turnOn)
    {
        if (!turnOn)
        {
            gameSettings.Save();
        }
        credits.SetActive(false);
        settings.SetActive(turnOn);
        if (turnOn)
        {
            barSoundEffects.SetProgressValue(gameSettings.SoundEffectsVolume);
            barMusic.SetProgressValue(gameSettings.MusicVolume);
        }
    }
    public void ToogleCredits(bool turnOn)
    {
        credits.SetActive(turnOn);
        settings.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetSoundEffectVolume(float val)
    {
        if(barSoundEffects.gameObject.activeInHierarchy)
            gameSettings.SoundEffectsVolume = val;

    }
    public void SetMusicVolume(float val)
    {
        if (barMusic.gameObject.activeInHierarchy)
            gameSettings.MusicVolume = val;
        FindObjectOfType<MusicSetter>().audioSource.volume = val;
    }
}
