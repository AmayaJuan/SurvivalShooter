using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider fXVolumeSlider;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    Canvas canvas;
    
    void LoadState()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);
        fXVolumeSlider.value = PlayerPrefs.GetFloat("FXVolume", 0);
    }

    void SaveState()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("FXVolume", fXVolumeSlider.value);
    }

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        LoadState();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }
    
    public void Pause()
    {
        canvas.enabled = !canvas.enabled;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        if (!canvas.enabled)
        {
            SaveState();
            unpaused.TransitionTo(0.01f);
        }
        else
            paused.TransitionTo(0.01f);
    }
    
    public void Quit()
    {
        SaveState();
        #if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
        #else 
        Application.Quit();
        #endif
    }
}