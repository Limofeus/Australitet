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
        DayCounter.text = "Неделя " + Totalres.weekCount;
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
            GameOver("Поселение не смогло переждать зиму", 
                $"Число жителей стало недостаточным для выживания. Вы смогли продержаться {Totalres.weekCount} дней");
        }
        if (Totalres.weekCount >= 53)
        {
            if (Totalres.GetSickyPeopleFraction() > 0.8f)
                GameOver("Поселение не смогло переждать зиму",
    $"Численность вашего поселения достигла нуля. Вы смогли продержаться {Totalres.weekCount} дней");
            else if (Totalres.GetHungryPeopleFraction() > 0.8f)
                GameOver("Голодная смерть",
$"Вы смогли продержаться год, но температура на поверхности сделала ее необитаемой." +
$" Имеющихся у вас ресурсов не хватало на всех, и число людей, умерших от голода, с каждым днем росло." +
$" Число жителей стало недостаточным для выживания.");
            else if (Totalres.GetHappyPeopleFraction() > 0.8f)
                GameOver("Внезапная смерть",$" Ваше поселение оказалось запертым под землей." +
$" Неблагоприятные условия и отсутствие надежды сказались на здоровье людей." +
$" Число больных с каждым днем увеличивалось, пока все жители не стали разносчиками болезни." +
$" В конце концов, поселение вымерло.");
            else
                GameOver("Поселение смогло переждать зиму",
    $"Вы хорошо справились в сложившихся условиях." +
    $" Вы смогли правильно распределить ресурсы и организовать людей." +
    $" Ваше поселение смогло пережить зиму.");
        }
    }

    private void GameOver(string name, string descr)
    {
        var panel = Instantiate(_gameEndPanelPrefab.gameObject);
        panel.GetComponent<GameEndPanel>().Init(name, descr);
    }
}
