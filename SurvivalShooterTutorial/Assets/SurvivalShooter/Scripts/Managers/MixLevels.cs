using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetFXVolume(float fxVolume)
    {
        masterMixer.SetFloat("FXVolume", fxVolume); 
    }

    public void SetMusicVolume(float musicVolume)
    {
        masterMixer.SetFloat("MusicVolume", musicVolume);
    }
}
