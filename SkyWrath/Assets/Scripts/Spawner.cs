using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEditor;
using System.Threading.Tasks;

public class Spawner : MonoBehaviour
{
    [SerializeField] private string[] AddressableEnemiesLabels;
    [SerializeField] private float TimeToSpawnMin = 1;
    [SerializeField] private float TimeToSpawnMax = 3;

    [SerializeField] private GameObject[] Enemies;

    private float _spawnTimer;
    private bool _isPaused;
    

    private void OnEnable()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void Update()
    {
        if(_isPaused)
            return;
        
        SpawnerBehavior();
    }
    
    private async void SpawnAsync()
    {
        /*AsyncOperationHandle<GameObject> handle =
            Addressables.LoadAssetAsync<GameObject>(
                AddressableEnemiesLabels[Random.Range(0, AddressableEnemiesLabels.Length)]);*/

        //await handle.Task;

        await Task.Delay(100);

        int randomspawn = Random.Range(0, Enemies.Length);

        Instantiate(Enemies[randomspawn], transform.position, Quaternion.identity);
        
        //Addressables.Release(handle);
    }

    private void SpawnerBehavior()
    {
        if (_spawnTimer > 0f)
            _spawnTimer -= Time.deltaTime;
        else
        {
            SpawnAsync();
            _spawnTimer = Random.Range(TimeToSpawnMin, TimeToSpawnMax);
        }
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Pause)
            _isPaused = true;
        else
        {
            _isPaused = false;
        }

    }
}