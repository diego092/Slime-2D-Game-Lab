using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public AudioSource clip;

    public void OptionsPanel() 
    {
        Debug.Log("Estoy poniendo en pausa el juego!");
        Time.timeScale = 0f;
        optionsPanel.SetActive(true);
    }

    public void Return() 
    {
        Time.timeScale = 1f;
        optionsPanel.SetActive(false);
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

    public void GoToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame() 
    {
        Debug.Log("Estoy cerrando el juego!");
        Application.Quit();
    }
}
