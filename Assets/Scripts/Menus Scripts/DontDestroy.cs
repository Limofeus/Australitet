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
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().mute = false;
        }
    }
}
