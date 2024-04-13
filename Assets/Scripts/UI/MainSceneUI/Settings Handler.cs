using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject UserUI;
    private GameObject Instance;
    public bool isClose = true;

    public void OpenAndCloseMenu()
    {
        if (isClose) Instance = Instantiate(SettingsPanel);
        else Destroy(Instance);
        
        isClose = !isClose;
        UserUI.SetActive(isClose);
    }
}
