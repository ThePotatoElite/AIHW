using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public static DeathManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
