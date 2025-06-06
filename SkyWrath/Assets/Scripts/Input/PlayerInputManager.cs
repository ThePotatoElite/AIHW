using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{    
    public static PlayerInputManager Instance;
    
    public MousePressEvent OnMousePress;

    private Vector3 _position;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void MousePress(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                    OnMousePress?.Invoke(hit.point);
            }            
        }
    }
    
    public void SaveGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameManager.Instance.saveGame();
        }
    }

    public void LoadGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameManager.Instance.loadGame(GameManager.loadSetting.Update);
        }
    }

    public void RestartGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
            {
                DeathManager.instance.RestartGame();
            }
        }
    }
}
