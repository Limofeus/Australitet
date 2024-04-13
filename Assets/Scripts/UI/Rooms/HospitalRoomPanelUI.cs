using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HospitalRoomPanelUI : RoomPanelUI
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _peopleCountInResults;
    [SerializeField] private TMP_Text _workerCountInResults;
    [HideInInspector] public Hospital room;
    private int lastValueOnSlider = 0;

    public void Init()
    {
        _slider.maxValue = room.HospitalCapacity;
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
        _peopleCountInResults.text = (room.peopleInHospital).ToString();
        _workerCountInResults.text = ((room.peopleInHospital) > 0 ? room.WorkerForCure : 0).ToString();
    }
}
