using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiToggle : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            uiCanvas.SetActive(!uiCanvas.active);
        }
    }
}
