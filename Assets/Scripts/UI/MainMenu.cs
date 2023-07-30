using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu, gameOverMenu;


    private void Start()
    {
        AudioManager.instance.Play("MainMenu");

        AudioManager.instance.Stop("BGM");
        AudioManager.instance.Stop("Ambience");
        AudioManager.instance.Stop("Moving");
    }

    public void Pause()
    { 
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Menu()
    {
        AudioManager.instance.Play("MainMenu");

        AudioManager.instance.Stop("BGM");
        AudioManager.instance.Stop("Ambience");
        AudioManager.instance.Stop("Moving");

        Time.timeScale = 1f;
        gamePaused = false;
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
