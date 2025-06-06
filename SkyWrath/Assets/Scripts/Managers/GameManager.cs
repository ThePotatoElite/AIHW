using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public FullsaveData fullSaveData = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        fullSaveData.scoreAmount = 0;

        loadGame(loadSetting.Start);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            saveGame();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            loadGame(loadSetting.Update);
        }

        if (fullSaveData.templeInfo.HP <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void DamageTemple(int amount)
    {
        fullSaveData.templeInfo.HP -= amount;
    }

    public void AddScore(int scoreAmnt)
    {
        fullSaveData.scoreAmount += scoreAmnt;
    }

    public enum loadSetting
    {
        Start,
        Update
    }

    public void saveGame()
    {
        SaveSystemService.instance.SaveToJson(fullSaveData);
    }

    public void loadGame(loadSetting setting)
    {
        switch (setting)
        {
            case loadSetting.Start:
                SaveSystemService.instance.LoadFromJson();
                break;
            case loadSetting.Update:
                SaveSystemService.instance.LoadFromJson();
                SettingMenu.Instance.LoadSavedSettings();
                break;
            default:
                SaveSystemService.instance.LoadFromJson();
                break;
        }
    }
}

[System.Serializable]
public class FullsaveData
{
    public int scoreAmount;
    public TempleData templeInfo;
    public SettingsData settingsData;
}

[System.Serializable]
public class TempleData
{
    public int HP = 100;
}

[System.Serializable]
public class SettingsData
{
    public int GameResolution;
    public bool IsFullScreen = true;
    public int QualityIndex = 2;
    public float MasterVolumeSlider = 1;
    public float MusicVolumeSlider = 1;
    public float SFXVolumeSlider = 1;
}