using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu 2");
    }
}
