using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject settingsPanel;
    public AudioSource clip;

    public void OptionsPanel() 
    {
        Time.timeScale = 0f;
        optionsPanel.SetActive(true);
    }

    public void Return() 
    {
        Time.timeScale = 1f;
        optionsPanel.SetActive(false);
    }

    public void Settings() 
    {
        settingsPanel.SetActive(true);
    }

    public void GoMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToSettingsMenu() 
    {
        SceneManager.LoadScene("Settings");
    }

    

    public void StartGame()
    {
        SceneManager.LoadScene("Nivel1");
    }

    //Sound
    public void PlaySoundButton() 
    {
        clip.Play();
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
