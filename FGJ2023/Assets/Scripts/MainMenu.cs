using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Lade die Spielszene
        SceneManager.LoadScene(1);
    }

    

    public void QuitGame()
    {
        // Beende das Spiel (im Build)
        Application.Quit();
    }
}
