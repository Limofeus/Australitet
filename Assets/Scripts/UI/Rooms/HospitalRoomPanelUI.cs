using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HospitalRoomPanelUI : RoomPanelUI
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Toggle _chechbox;
    [SerializeField] private TMP_Text _peopleCountInResults;
    [SerializeField] private TMP_Text _workerCountInResults;
    [HideInInspector] public Hospital room;
    public int totalPeopleCount = 0;

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

    public void OnCheckboxChanged()
    {
        room.OnCheckboxValueChanged(_chechbox.isOn);
        UpdateResults();
    }

    private void UpdateResults()
    {
        _peopleCountInResults.text = ((int)_slider.value).ToString();

        var workerCount = 0;
        workerCount += _chechbox.isOn ? room.WorkerForAnalysis : 0;
        workerCount += _slider.value != 0? room.WorkerForCure : 0;
        _workerCountInResults.text = workerCount.ToString();

        totalPeopleCount = workerCount + (int)_slider.value;
    }

    private bool IsCanChangePeopleCount(int value)
    {
        return Totalres.people.Available + value >= 0;
    }
}
