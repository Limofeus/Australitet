using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomPanelUI : MonoBehaviour
{
    public float minScale = 0.8f;
    public float maxScale = 1f;
    public float screenRato = 4f;
    public float clampRatio = 0.4f;
    public float screenTenth;


    private void Update()
    {
        screenTenth = Screen.height / screenRato;
        float posDifference = Vector3.Distance(Input.mousePosition, ((RectTransform)transform).position) / Mathf.Lerp(minScale, maxScale, MainSceen.CameraMover._currentZoom);
        transform.localScale = Vector3.one * Mathf.Clamp01((screenTenth - posDifference) / (screenTenth * clampRatio)) * Mathf.Lerp(minScale, maxScale, MainSceen.CameraMover._currentZoom);
    }
}
