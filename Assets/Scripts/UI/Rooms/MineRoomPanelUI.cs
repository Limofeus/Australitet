using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineRoomPanelUI : RoomPanelUI
{
    [SerializeField] private Toggle _checkBox;
    private bool _lastConditional = false;

    public MineRoom room;

    public void Init()
    {
        _lastConditional = false;
        _checkBox.isOn = false;
    }

    public void OnCheckBoxChanged()
    {
        if (_lastConditional == _checkBox.isOn)
        {
            return;
        }

        var isCanBeOn = room.WorkerCount <= Totalres.people.Available;
        if (isCanBeOn && _checkBox.isOn)
        {
            room.OnCheckBoxChanged(true);
        }
        else if (!_checkBox.isOn)
        {
            room.OnCheckBoxChanged(false);
        }
        else
        {
            _checkBox.isOn = false;
        }
        _lastConditional = _checkBox.isOn;
    }
}
