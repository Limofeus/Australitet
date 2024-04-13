using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecreationRoomPanelUI : RoomPanelUI
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _peopleCountInResults;    
    [HideInInspector] public Recreation room;
    private int lastValueOnSlider = 0;

    public void Init()
    {
        _slider.maxValue = room.RecreationCapacity; 
        _slider.value = 0;
        lastValueOnSlider = 0;
        UpdateResults();
    }

    public void OnSliderChanged()
    {
        var different = (int)_slider.value - lastValueOnSlider;
        if (different <= Totalres.people.Available)
        {
            room.OnSliderValueChanged((int)_slider.value);
            lastValueOnSlider = (int)_slider.value;
        }
        else
        {
            _slider.value = lastValueOnSlider;
        }
        UpdateResults();
    }

    private void UpdateResults()
    {
        _peopleCountInResults.text = ((int)_slider.value).ToString();
    }
}
