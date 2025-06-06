using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject SettingsMenu;
    
    public AudioManager audioManager; 
    public void Pause()
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay
            ? GameState.Pause
            : GameState.Gameplay;

        GameStateManager.Instance.SetState(newGameState);
        PauseMenu.SetActive(true);
        // Time.timeScale = 0;
    }
    public void Resume()
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay
            ? GameState.Pause
            : GameState.Gameplay;

        GameStateManager.Instance.SetState(newGameState);
        PauseMenu.SetActive(false);
        // Time.timeScale = 1;
    }

    public void Settings()
    {
        SettingsMenu.SetActive(true);
    }

    public void Return()
    {
        SettingsMenu.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}