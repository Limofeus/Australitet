using Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedRoom : Room
{
    private const int _inhabitantsCount = 2;
    private bool _isRoomActivated = false;

    public void ChangeRoomActive()
    {
        if (_isRoomActivated)
        {
            _isRoomActivated = false;
            PlayerResources.AvailableInhabitantsNumber += _inhabitantsCount;
        }
        else if (PlayerResources.AvailableInhabitantsNumber >= _inhabitantsCount)
        {
            _isRoomActivated = true;
            PlayerResources.AvailableInhabitantsNumber -= _inhabitantsCount;
        }
    }
}
