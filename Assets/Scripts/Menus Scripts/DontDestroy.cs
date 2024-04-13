using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;

    void Start()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if(!instance.GetComponent<AudioSource>().isPlaying)
            instance.GetComponent<AudioSource>().Play();
    }

    public void MuteMusic()
    {
        instance.GetComponent<AudioSource>().Stop();
    }
}
