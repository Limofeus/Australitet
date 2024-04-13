using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventUiHandler : MonoBehaviour
{
    public static EventUiHandler Singleton;
    [SerializeField] private GameObject smallEventWindow;
    [SerializeField] private TextMeshProUGUI smallEventNameText;
    [SerializeField] private TextMeshProUGUI smallEventDescText;
    [SerializeField] private GameObject bigEventWindow;

    private void Awake()
    {
        Singleton = this;
    }

    public void SetSmallEvent(string name, string desc)
    {
        smallEventNameText.text = name;
        smallEventDescText.text = desc;
    }
}
