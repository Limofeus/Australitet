using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BigEventButton : MonoBehaviour
{
    private EventUiHandler eventUiHandler;
    public TextMeshProUGUI buttonText;
    private int butId;

    public void Init(EventUiHandler teventUiHandler, int tbutId, string butText)
    {
        eventUiHandler = teventUiHandler;
        butId = tbutId;
        buttonText.text = butText;
    }

    public void CastEventChoise()
    {
        eventUiHandler.CastEventChoise(butId);
    }
}
