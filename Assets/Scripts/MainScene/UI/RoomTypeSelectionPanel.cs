using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTypeSelectionPanel : MonoBehaviour
{
    //public RectTransform panelHolder;
    public RectTransform panelTransform;
    public bool panelOpened = false;
    public float lerpPower;
    public EmptyRoom room;
    private void Start()
    {
        panelTransform.localScale = Vector3.zero;
    }
    void Update()
    {
        Vector3 lerpTarget = Vector3.zero;
        if (panelOpened)
        {
            lerpTarget = Vector3.one;
        }
        panelTransform.localScale = Vector3.Lerp(panelTransform.localScale, lerpTarget, 1 - Mathf.Exp(-lerpPower * Time.deltaTime));
        if (Mathf.Abs(Input.mousePosition.x - panelTransform.position.x) > (panelTransform.sizeDelta.x / 2f) || Mathf.Abs(Input.mousePosition.y - panelTransform.position.y) > (panelTransform.sizeDelta.y / 2f))
        {
            if (panelOpened)
            {
                room.ShowTypeSelector();
            }
            panelOpened = false;
        }
        else
        {
            //panelOpened = true;
        }
    }
    public void SelectRoom(int roomId)
    {
        room.SelectRoom(roomId);
    }
}
