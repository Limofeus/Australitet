using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Singleton { get; private set; }
    public enum EventType { SmallEvent, BigEvent};
    public List<EventType> eventSeq = new List<EventType> { EventType.SmallEvent, EventType.SmallEvent, EventType.SmallEvent, EventType.BigEvent};
    public int eventCounter = -1;

    public Sprite[] eventIcons;
    public BigEvent[] bigEvents = new BigEvent[] 
    
    { 
        new BigTestEvent("АВСТРАЛИЙСКАЯ ЧУМА", -1),
        new NewPeopleBigEvent("Люди на поверхности", "До нас добралась группа людей из соседнего поселения, они просят убежища, однако еды на всех может не хватить", 1, "Впустить", "Не пускать"),
    };

    public SmallEvent[] smallEvents = new SmallEvent[]
    {      
        new GenericSmallEvent("День Кенгуру", "Выжившие празднуют день кенгуру, часть свободных людей развлекаются", new (availablePeople:2, happinessPercent:-0.9f,sickPercent:0.1f), new(availablePeople:-2, happinessPercent:0.1f,sickPercent:0.1f), false, true),
        new GenericSmallEvent("Повар отжигает", "Повар.. сам не свой, всё", new (hungryPercent:-0.9f), new(hungryPercent: 0.15f), false, true),
        new GenericSmallEvent("Кашель", "Похоже, что часть людей болеет", new (sickPercent: 0.5f), new(), false),
        new GenericSmallEvent("Жвачка Джоржа", "Джорж уронил жвачку, а Боб её съел.. из о рта в рот (через пол) получается микроб", new (), new(sickPercent: 0.05f), false, true),
        new GenericSmallEvent("Охота с бумерангом", "Стивен пошёл на охоту с бумерангом, у него получилось убить пару грибных косуль, но ему пришлось пожертвовать собой (стоило использовать лук)", new (maxPeople:1), new(maxPeople:-1, sickPercent: 0.05f), true, true),
        new GenericSmallEvent("БУНТ!", "Бунтари собрались.. капут", new (happinessPercent: -0.2f), new(availablePeople:-4), false, true),
        new GenericSmallEvent("На 3 метра ниже", "Селяни решили похоронить собаку, которую они нашли под землёй, копая проход", new (happinessPercent: 0.3f), new(availablePeople:2), false, true),
        new GenericSmallEvent("Новые селяне!", "Вам посчастливилось найти новых людей для своей колонии, но они оказались трупами :( ...ничего не изменилось...", new (), new(), true, true),
        new GenericSmallEvent("Во имя отечества: гимн", "Группа жителей решила создать гимн нашего королевства", new(happinessPercent: 0.2f), new(availablePeople: -3, happinessPercent: 0.05f), true),
        new GenericSmallEvent("Во имя отечества: флаг", "Группа жителей решила создать флаг нашего королевства", new(happinessPercent: 0.2f), new(availablePeople: -3, happinessPercent: 0.05f), true),
        new GenericSmallEvent("Во имя отечества: герб", "Группа жителей решила создать герб нашего королевства", new(happinessPercent: 0.2f), new(availablePeople: -3, happinessPercent: 0.05f), true),
        new GenericSmallEvent("Свадьба Короля!", "Король нашёл себе избранницу и приглашает всё королевство на грибной пир", new(food: 30), new(food: -30, happinessPercent: 0.25f), true),
        new GenericSmallEvent("Я и моя тень", "Счастливое рождение двойни. Муж Лары безумно рад!", new(), new(maxPeople: 2), true),
        new GenericSmallEvent("Дракон!", "Во время расширения подземного королевства были обнаружены останки динозавров. Поселенцы напуганы!", new(happinessPercent: 0.7f), new(happinessPercent: -0.1f), true),
        new GenericSmallEvent("Мышиный король", "Мыши прорвались к грибам и часть запасов бесследно пропала", new(rawfood: 15), new(rawfood: -15), true),
        new GenericSmallEvent("Мастер на все руки", "Сын лекаря решил помочь отцу в работе и начал изготавливать врачебные приспособления", new(sickPercent: 0.5f), new(metal: -10, sickPercent: -0.08f, availablePeople:-1, happinessPercent: 0.05f), true),
        new GenericSmallEvent("Продовольственный просчёт", "При перепроверке запасов оказалось что поселяне допустили ошибки при подсчёте продуктов", new(rawfood: 5), new(rawfood: -5, happinessPercent: -0.03f), false),
        new GenericSmallEvent("ФУ! ПЛЕСЕНЬ!", "Часть запасов готовой еды была уничтожена", new(food: 10), new(food: -10, happinessPercent: -0.03f), false),
        new GenericSmallEvent("Проблемы со сном", "Жителям ночью не давало спать завывание ветра", new(), new(happinessPercent: -0.05f), false),
        new GenericSmallEvent("День Рождения", "У нескольких жителей День Рождения! Они приглашают всё королевство на обед", new(food: 10), new(rawfood: -5, happinessPercent: 0.05f), true),
        new GenericSmallEvent("Грибное разнообразие", "Повара экспериментируют с грибными блюдами, чтобы еда не начала приедаться", new(rawfood: 10), new(rawfood: -10, food: 4, happinessPercent: 0.04f), false),
        new GenericSmallEvent("Самый обычный день", "Ничего плохого не случилось-уже хорошо", new(), new(happinessPercent: 0.02f), false),
    };

    public bool testCastEvent;
    public bool testCastBigEvent;
    public bool testCastSmallEvent;
    public bool listEventDebug;

    const int limitAddition = 2;

    private void Awake()
    {
        Singleton = this;
        LimitBigEventTime();
    }

    void Update()
    {
        if (testCastEvent)
        {
            testCastEvent = false;
            CastRandEvent();
        }
        if (testCastBigEvent)
        {
            testCastBigEvent = false;
            CastBigEvent();
        }
        if (testCastSmallEvent)
        {
            testCastSmallEvent = false;
            CastSmallEvent();
        }
        if (listEventDebug)
        {
            listEventDebug = false;
            DebugEvents(smallEvents);
        }
    }
    public void CastRandEvent()
    {
        EventUiHandler.Singleton.HideEventWindows();

        if(eventCounter < 0)
        {
            List<EventType> newEventSeq = new List<EventType>();
            int evseqLen = eventSeq.Count;
            for(int i = 0; i < evseqLen; i++)
            {
                var randEv = eventSeq[Random.Range(0, eventSeq.Count)];
                newEventSeq.Add(randEv);
                eventSeq.Remove(randEv);
            }
            eventSeq = newEventSeq;
            eventCounter = eventSeq.Count - 1;
        }
        //Debug.Log(eventCounter);
        switch (eventSeq[eventCounter])
        {
            case EventType.SmallEvent:
                CastSmallEvent();
                break;
            case EventType.BigEvent: 
                CastBigEvent(); 
                break;
        }
        eventCounter--;
    }
    private void CastBigEvent()
    {
        GameEvent eventToCastMightBeNull = SelectMaxTimeEvent(bigEvents);
        if(eventToCastMightBeNull != null)
        {
            BigEvent eventToCast = (BigEvent)eventToCastMightBeNull;
            UpdateAllEventCastTime(bigEvents);
            eventToCast.Cast();
            EventUiHandler.Singleton.SetBigEvent(eventToCast);
            Debug.Log($"CASTED >{eventToCast.eventName}< EVENT");
        }
    }
    private void CastSmallEvent()
    {
        GameEvent eventToCastMightBeNull = SelectMaxTimeEvent(smallEvents);
        if(eventToCastMightBeNull != null)
        {
            SmallEvent eventToCast = (SmallEvent)eventToCastMightBeNull;
            UpdateAllEventCastTime(smallEvents);
            eventToCast.Cast();
            EventUiHandler.Singleton.SetSmallEvent(eventToCast.eventName, eventToCast.eventDescription);
            Debug.Log($"CASTED >{eventToCast.eventName} | {eventToCast.eventDescription}< EVENT");
        }
    }

    private void LimitBigEventTime()
    {
        foreach(var bigEvent in bigEvents)
        {
            bigEvent.limitTimeSinceCast = bigEvents.Length - limitAddition;
        }
    }
    private GameEvent SelectMaxTimeEvent(GameEvent[] events)
    {
        List<GameEvent> possibleEvents = new List<GameEvent> ();
        int maxTimeSince = 0;
        foreach(var checkEvent in events)
        {
            if (checkEvent.IsPossible())
            {
                possibleEvents.Add(checkEvent);
                if(checkEvent.timeSinceEvent >= 0)
                {
                    if(maxTimeSince < checkEvent.timeSinceEvent && maxTimeSince >= 0)
                    {
                        maxTimeSince = checkEvent.timeSinceEvent;
                    }
                }
                else
                {
                    maxTimeSince = checkEvent.timeSinceEvent;
                }
            }
        }
        Debug.Log($"Pos EV Count: {possibleEvents.Count}");

        if(possibleEvents.Count == 0) return null;

        List<GameEvent> maxTimeEvents = new List<GameEvent>();
        foreach (var checkEvent in possibleEvents)
        {
            if(maxTimeSince < 0)
            {
                if(checkEvent.timeSinceEvent < 0)
                {
                    maxTimeEvents.Add(checkEvent);
                }
            }
            else
            {
                if(checkEvent.timeSinceEvent >= maxTimeSince)
                {
                    maxTimeEvents.Add(checkEvent);
                }
            }
        }
        return maxTimeEvents[Random.Range(0, maxTimeEvents.Count)];
    }
    private void UpdateAllEventCastTime(GameEvent[] events)
    {
        foreach(var updEvent in events)
        {
            updEvent.UpdateTimeSinceCast();
        }
    }
    private void DebugEvents(GameEvent[] events)
    {
        foreach(var debEvent in events)
        {
            Debug.Log($"NAME {debEvent.eventName} DESC {debEvent.eventDescription} TIME {debEvent.timeSinceEvent}");
        }
    }
}
