using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public string _volumeParam = "MasterVolume";

    public AudioMixer _AudioMixer;

    public Slider _Slider;

    private float _volumeValue;
    const float multuply = 20f;
    private void Awake()
    {
        _Slider.onValueChanged.AddListener(SliderChangerHandler);
    }

    private void SliderChangerHandler(float value)
    {
        _volumeValue = Mathf.Log10(value) * multuply;
        _AudioMixer.SetFloat(_volumeParam, _volumeValue);
    }

    void Start()
    {
        _volumeValue = PlayerPrefs.GetFloat(_volumeParam, Mathf.Log10(_Slider.value) * 20f);
        _Slider.value = Mathf.Pow(10f, _volumeValue / multuply);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParam, _volumeValue);
    }
}
