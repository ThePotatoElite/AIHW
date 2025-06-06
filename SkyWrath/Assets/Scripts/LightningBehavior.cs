using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class LightningBehavior : MonoBehaviour
{
    private const int ENEMIES_LAYERMASK = 6;
    
    [SerializeField] private VisualEffect lightningEffect;
    
    public GameObject Wrapper;

    [Header("Values")]
    [SerializeField] float DamageRadius;
    
    
    [Header("Particles")]

    public ParticleSystem Lightning_Explosion;
    public ParticleSystem Lightning_Residue;
    public AudioSource Lightning_SFX;
    public AudioSource DeathScream_SFX;
    
    private bool isActivated = false;

    private Vector3 lightingPos;
    private bool _isPaused;


    /*private void OnEnable()
    {
          PlayerInputManager.Instance.OnMousePress.AddListener(LightningInit);
    }

    private void OnDisable()
    {
          PlayerInputManager.Instance.OnMousePress.RemoveListener(LightningInit);
    }*/
    
   

    private void OnEnable()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    public void LightningInit(Vector3 test)
    {
        if(_isPaused)
            return;
        
        if (!isActivated)
        {
            isActivated = true;
            
            lightingPos = test;

            StartCoroutine("LightningFull");
        }
    }

    IEnumerator LightningFull()
    {
        Wrapper.transform.position = lightingPos;

        lightningEffect.Play();
        Lightning_SFX.Play();
        Lightning_Residue.Play();

        Lightning_Explosion.Play();
        
        yield return new WaitForSeconds(0.5f);
        
        DestroyHitEnemies();
    
        yield return new WaitForSeconds(0.5f);

        Lightning_Explosion.Stop();

        yield return new WaitForSecondsRealtime(1);

        isActivated = false;

        yield break;
    }


    private Collider[] getHitColliders()
    {
        return Physics.OverlapSphere(lightingPos, DamageRadius);
    }

    private void DestroyHitEnemies()
    {
        var e = getHitColliders();
        
        foreach (var col in e)
        {
            if (col.CompareTag("Enemy"))
            {
                DeathScream_SFX.Play();
                Destroy(col.GameObject());
                GameManager.Instance.AddScore((int)Random.Range(75, 150));
            }
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