using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SaveSystemService : MonoBehaviour
{
    public static SaveSystemService instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SaveToJson<T>(T serializableStuff)
    {
        string saveData = JsonUtility.ToJson(serializableStuff);
        string filePath = Application.persistentDataPath + "/SaveData.json";
        Debug.Log(filePath);

        System.IO.File.WriteAllText(filePath, saveData);
        Debug.Log("Savefile successfully written");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";

        if (System.IO.File.Exists(filePath))
        {
            string saveData = System.IO.File.ReadAllText(filePath);

            GameManager.Instance.fullSaveData = JsonUtility.FromJson<FullsaveData>(saveData);
            Debug.Log("SaveData Loaded");
        }
        else
        {
            return;
        }        
    }
}
