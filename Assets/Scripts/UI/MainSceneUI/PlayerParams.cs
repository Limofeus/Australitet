using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParams : MonoBehaviour
{
    public static PlayerParams Singleton;
    [SerializeField] private GameEndPanel _gameEndPanelPrefab;
    [SerializeField] private Image _blackPanel;
    
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
        Totalres.Reset();
        Totalres.people.SetTimeout(0);
    }

    private void Start()
    {
        UpdateRealtimeParams();
        UpdateDayParams();
        StartCoroutine(GameStart());
    }

    public void UpdateRealtimeParams()
    {
        PeopleCounter.text = Totalres.people?.Available + " / " + Totalres.people?.Max;
        FoodCounter.text = Totalres.food?.CurrentValue + " / " + Totalres.food?.MaxValue;
        RawFoodCounter.text = Totalres.rawFood?.CurrentValue + " / " + Totalres.rawFood?.MaxValue;
        MaterialsCounter.text = Totalres.metal.CurrentValue + " / " + Totalres.metal.MaxValue;
    }

    public void UpdateDayParams()
    {
        DayCounter.text = "Неделя " + (Totalres.weekCount + 1);
        SickPeopleCounter.text = GetSickPeopleString();
        HungryPeopleCounter.text = (int)(Totalres.GetHungryPeopleFraction() * 100) + "%";
        HappyPeopleCounter.text = (int)(Totalres.GetHappyPeopleFraction() * 100) + "%";
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
        StartCoroutine(DayChanged());
    }

    private IEnumerator DayChanged()
    {
        var effectTime = 0.4f;
        var timer = effectTime;
        _blackPanel.gameObject.SetActive(true);
        while (timer > 0)
        {
            _blackPanel.color = new Color(0, 0, 0, 1 - timer / effectTime);
            timer -= Time.deltaTime;
            yield return null;
        }
        Totalres.weekCount++;
        Totalres.reviewedPeopleCount = 0;
        Totalres.people.ReturnTimeoutPeople();

        EventManager.Singleton.CastRandEvent();


        Totalres.BadDay();

        Totalres.Eat();
        foreach (var room in BuildGrid.Singleton.rooms)
        {
            var activeRoom = room as ActivatedRoom;
            activeRoom?.OnTheEndOfDay();
        }
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

        PeopleVisualManager.Singleton.SetCheliks();

        while (timer < effectTime)
        {
            _blackPanel.color = new Color(0, 0, 0, 1 - timer / effectTime);
            timer += Time.deltaTime;
            yield return null;
        }
        _blackPanel.gameObject.SetActive(false);
    }

    private IEnumerator GameStart()
    {
        var effectTime = 1f;
        var timer = 0f;
        _blackPanel.gameObject.SetActive(true);
        _blackPanel.color = new Color(0, 0, 0, 1);

        PeopleVisualManager.Singleton.SetCheliks();

        while (timer < effectTime)
        {
            _blackPanel.color = new Color(0, 0, 0, 1 - timer / effectTime);
            timer += Time.deltaTime;
            yield return null;
        }
        _blackPanel.gameObject.SetActive(false);
    }

    private bool ChechEndGame()
    {
        if (Totalres.people.Max <= 2f)
        {
            GameOver("Поселение не смогло переждать зиму", 
                $"Число жителей стало недостаточным для выживания. {Totalres.weekCount} неделя стала для поселения последней");
            return true;
        }
        if (Totalres.GetHappyPeopleFraction() < 0.02f)
        {
            GameOver("Внезапная смерть", $" Ваше правление не нравилось людям. " +
                $"Вы нее смогли принимать правильные решения, из-за чего пострадало много людей. " +
                $"В конце концов, Вы обнаружили яд в своей еде, но было уже поздно.");
        }

        if (Totalres.weekCount >= 53)
        {
            if (Totalres.GetSickyPeopleFraction() > 0.8f)
                GameOver("Чума", $" Ваше поселение оказалось запертым под землей." +
$" Неблагоприятные условия и отсутствие надежды сказались на здоровье людей." +
$" Число больных с каждым днем увеличивалось, пока все жители не стали разносчиками болезни." +
$" В конце концов, поселение вымерло.");
            else if (Totalres.GetHungryPeopleFraction() > 0.8f)
                GameOver("Голодная смерть",
$"Вы смогли продержаться год, но температура на поверхности сделала ее необитаемой." +
$" Имеющихся у вас ресурсов не хватало на всех, и число людей, умерших от голода, с каждым днем росло." +
$" Число жителей стало недостаточным для выживания.");
            else if (Totalres.GetHappyPeopleFraction() < 0.36f)
                GameOver("Внезапная смерть",$" Ваше правление не нравилось людям. " +
                    $"Вы нее смогли принимать правильные решения, из-за чего пострадало много людей. " +
                    $"В конце концов, Вы обнаружили яд в своей еде, но было уже поздно.");
            else
                GameOver("Поселение смогло переждать зиму",
    $"Вы хорошо справились в сложившихся условиях." +
    $" Вы смогли правильно распределить ресурсы и организовать людей." +
    $" Ваше поселение смогло пережить зиму.");
            return true;
        }
        return false;
    }

    private void GameOver(string name, string descr)
    {
        var panel = Instantiate(_gameEndPanelPrefab.gameObject);
        panel.GetComponent<GameEndPanel>().Init(name, descr);
    }
}
