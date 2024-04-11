using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeInit : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer _AudioMixer;
    void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(volumeParameter, volumeParameter == "MusicVol" ? -40f : -80f);
        _AudioMixer.SetFloat(volumeParameter, volumeValue);
    }
}
