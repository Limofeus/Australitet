using TMPro;
using UnityEngine;

public class PlayerParams : MonoBehaviour
{
    public static PlayerParams Singleton;
    [SerializeField] private GameEndPanel _gameEndPanelPrefab;
    
    public TextMeshProUGUI DayCounter;
    public TextMeshProUGUI PeopleCounter;
    public TextMeshProUGUI SickPeopleCounter;
    public TextMeshProUGUI HungryPeopleCounter;
    public TextMeshProUGUI HappyPeopleCounter;
    public TextMeshProUGUI FoodCounter;
    public TextMeshProUGUI RawFoodCounter;
    public TextMeshProUGUI MaterialsCounter;

    private void Awake()
    {
        Singleton = this;
        Totalres.people.SetTimeout(0);
    }

    private void Start()
    {
        UpdateRealtimeParams();
        UpdateDayParams();
    }

    public void UpdateRealtimeParams()
    {
        PeopleCounter.text = Totalres.people?.Available + " / " + Totalres.people?.Max;
        FoodCounter.text = Totalres.food?.CurrentValue + " / " + Totalres.food?.MaxValue;
        RawFoodCounter.text = Totalres.rawFood?.CurrentValue + " / " + Totalres.rawFood?.MaxValue;
    }

    public void UpdateDayParams()
    {
        DayCounter.text = "������ " + Totalres.weekCount;
        SickPeopleCounter.text = GetSickPeopleString();
        HungryPeopleCounter.text = (int)(Totalres.GetHungryPeopleFraction() * 100) + "%";
        HappyPeopleCounter.text = (int)(Totalres.GetHappyPeopleFraction() * 100) + "%";
        MaterialsCounter.text = Totalres.metal.CurrentValue + " / " + Totalres.metal.MaxValue;
    }

    private string GetSickPeopleString()
    {
        int procent = (int)(Totalres.GetSickyPeopleFraction() * 100);
        if (Totalres.GetNoInfoPeopleFraction() > 0.8f)
            return "??%";
        else if (Totalres.GetNoInfoPeopleFraction() > 0.5f)
            return (procent / 10 * (procent.ToString().Length - 1)).ToString() + "?%";
        return procent.ToString() + "%";
    }

    public void OnNewDay()
    {
        Totalres.weekCount++;
        Totalres.reviewedPeopleCount = 0;
        Totalres.people.ReturnTimeoutPeople();

        EventManager.Singleton.CastRandEvent();

        foreach (var room in BuildGrid.Singleton.rooms)
        {
            var activeRoom = room as ActivatedRoom;
            activeRoom?.OnTheEndOfDay();
        }

        Totalres.Eat();
        Totalres.KillHungryPeople();
        ChechEndGame();

        Totalres.KillSickyPeople();
        Totalres.Sick();

        ChechEndGame();

        Totalres.people.UpdatePeopleCount();

        foreach (var room in BuildGrid.Singleton.rooms)
        {
            var activeRoom = room as ActivatedRoom;
            activeRoom?.OnTheStartOfDay();
        }

        UpdateDayParams();
    }

    private void ChechEndGame()
    {
        if (Totalres.people.Max <= 2f)
        {
            GameOver("��������� �� ������ ��������� ����", 
                $"����� ������� ����� ������������� ��� ���������. �� ������ ������������ {Totalres.weekCount} ����");
        }
        if (Totalres.weekCount >= 53)
        {
            if (Totalres.GetSickyPeopleFraction() > 0.8f)
                GameOver("��������� �� ������ ��������� ����",
    $"����������� ������ ��������� �������� ����. �� ������ ������������ {Totalres.weekCount} ����");
            else if (Totalres.GetHungryPeopleFraction() > 0.8f)
                GameOver("�������� ������",
$"�� ������ ������������ ���, �� ����������� �� ����������� ������� �� �����������." +
$" ��������� � ��� �������� �� ������� �� ����, � ����� �����, ������� �� ������, � ������ ���� �����." +
$" ����� ������� ����� ������������� ��� ���������.");
            else if (Totalres.GetHappyPeopleFraction() > 0.8f)
                GameOver("��������� ������",$" ���� ��������� ��������� �������� ��� ������." +
$" ��������������� ������� � ���������� ������� ��������� �� �������� �����." +
$" ����� ������� � ������ ���� �������������, ���� ��� ������ �� ����� ������������ �������." +
$" � ����� ������, ��������� �������.");
            else
                GameOver("��������� ������ ��������� ����",
    $"�� ������ ���������� � ����������� ��������." +
    $" �� ������ ��������� ������������ ������� � ������������ �����." +
    $" ���� ��������� ������ �������� ����.");
        }
    }

    private void GameOver(string name, string descr)
    {
        var panel = Instantiate(_gameEndPanelPrefab.gameObject);
        panel.GetComponent<GameEndPanel>().Init(name, descr);
    }
}
