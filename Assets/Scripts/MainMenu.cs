using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void GameScene()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void NameMenu(string nameMenu)
    {
        
        SceneManager.LoadScene(nameMenu);
    }

    public void GameExit ()
    {
        Application.Quit();
    }
}
