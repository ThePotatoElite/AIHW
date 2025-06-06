using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public static SettingMenu Instance;

    public AudioMixer audioMixer;
    public TMP_Dropdown ResolutionDropDown;
    public TMP_Dropdown GraphicsDropDown;
    
    [SerializeField] Slider MasterVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;
    [SerializeField] Slider MusicVolumeSlider;
    [SerializeField] Toggle FullscreenToggle;

    Resolution[] resolutions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        resolutions = Screen.resolutions;
        ResolutionDropDown.ClearOptions();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                    currentResolutionIndex = i;
        }

        ResolutionDropDown.AddOptions(options);

        LoadSavedSettings();
    }

    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = resolutions[ResolutionIndex];
        GameManager.Instance.fullSaveData.settingsData.GameResolution = ResolutionIndex;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void LoadSavedSettings()
    {
        ResolutionDropDown.value = GameManager.Instance.fullSaveData.settingsData.GameResolution;
        Resolution SavedResolution = resolutions[ResolutionDropDown.value];
        Screen.SetResolution(SavedResolution.width, SavedResolution.height, Screen.fullScreen);

        GraphicsDropDown.value = GameManager.Instance.fullSaveData.settingsData.QualityIndex;
        QualitySettings.SetQualityLevel(GraphicsDropDown.value);
        GraphicsDropDown.RefreshShownValue();

        FullscreenToggle.isOn = GameManager.Instance.fullSaveData.settingsData.IsFullScreen;
        Screen.fullScreen = FullscreenToggle.isOn;

        MasterVolumeSlider.value = GameManager.Instance.fullSaveData.settingsData.MasterVolumeSlider;
        float volumeMaster = MasterVolumeSlider.value;
        audioMixer.SetFloat("master", Mathf.Log10(volumeMaster) *20);

        MusicVolumeSlider.value = GameManager.Instance.fullSaveData.settingsData.MusicVolumeSlider;
        float volumeMusic = MusicVolumeSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volumeMusic) * 20);

        SFXVolumeSlider.value = GameManager.Instance.fullSaveData.settingsData.SFXVolumeSlider;
        float volumeSFX = SFXVolumeSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volumeSFX) * 20);
    }
    
    public void SetMasterVolume()
    {
        float volume = MasterVolumeSlider.value;
        GameManager.Instance.fullSaveData.settingsData.MasterVolumeSlider = volume;
        audioMixer.SetFloat("master", Mathf.Log10(volume)*20);
    }

    public void SetMusicVolume()
    {
        float volume = MusicVolumeSlider.value;
        GameManager.Instance.fullSaveData.settingsData.MusicVolumeSlider = volume;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20);
    }
    
    public void SetSFXVolume()
    {
        float volume = SFXVolumeSlider.value;
        GameManager.Instance.fullSaveData.settingsData.SFXVolumeSlider = volume;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume)*20);
    }

    public void SetQuality(int qualityIndex)
    {
        GameManager.Instance.fullSaveData.settingsData.QualityIndex = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        GameManager.Instance.fullSaveData.settingsData.IsFullScreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
}