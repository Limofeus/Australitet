using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private int _sceneNumber;
    public static bool isFromMenu;

    public void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) isFromMenu = true;
        if (SceneManager.GetActiveScene().buildIndex == 2) isFromMenu = false;
    }

    public void Trans()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            SceneManager.LoadScene(_sceneNumber);
            return;
        }
        if (isFromMenu) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(2);

    }
}
