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
        UpdateResults();
    }

    public void OnSliderChanged()
    {
        room.OnSliderValueChanged((int)_slider.value);
        UpdateResults();
    }

    private void UpdateResults()
    {
        _peopleCountInResults.text = ((int)_slider.value).ToString();
        _workerCountInResults.text = (((int)_slider.value) > 0 ? room.WorkerForCure : 0).ToString();
    }
}
