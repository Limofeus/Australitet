using MainSceen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTypeSelector : MonoBehaviour
{
    public EmptyRoom emptyRoom;
    public float minScale;
    public float maxScale;
    public float screenRato;
    public float clampRatio;
    public float screenTenth;

    public bool hide = false;
    private void Start()
    {
        screenTenth = Screen.height / screenRato;
    }
    private void Update()
    {
        float posDifference = Vector3.Distance(Input.mousePosition, ((RectTransform)transform).position);
        transform.localScale = Vector3.one * Mathf.Clamp01((screenTenth - posDifference) / (screenTenth * clampRatio)) * Mathf.Lerp(minScale, maxScale, CameraMover._currentZoom) * (hide ? 0f : 1f);
    }

    public void ButtonClicked()
    {
        if(!hide)
            emptyRoom.ShowRoomTypeSelectWindow();
        hide = true;
    }
}
