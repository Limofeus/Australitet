using UnityEngine;
using UnityEngine.UI;

public class GardenRoomPanelUI : RoomPanelUI
{
    [SerializeField] private Slider _slider;
    public GardenBed room;
    private int _lastCondition = 0;

    public void Init()
    {
        _slider.value = 0;
        _lastCondition = 0;
    }

    public void OnSliderChanged()
    {
        if (_lastCondition == (int)_slider.value)
            return;

        var isCanBeOn = room.WorkerCount <= Totalres.people.Available;
        if (isCanBeOn && _slider.value > 0)
        {
            room.OnSliderChanged((int)_slider.value);
        }
        else if (_slider.value == 0)
        {
            room.OnSliderChanged(0);
        }
        else
        {
            _slider.value = 0;
        }
        _lastCondition = (int)_slider.value;

    }
}
