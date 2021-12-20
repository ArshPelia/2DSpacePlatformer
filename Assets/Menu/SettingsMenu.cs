using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audiomixer; 
    public void SetVolume(float Volume)
    {
        audiomixer.SetFloat("volume", Volume);  
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
