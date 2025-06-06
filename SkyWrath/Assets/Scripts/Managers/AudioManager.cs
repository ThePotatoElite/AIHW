using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip thunder;
    public AudioClip deathScream;
    public AudioClip vampireHiss;
    public AudioClip swordHit;
    
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    
    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }
    
}