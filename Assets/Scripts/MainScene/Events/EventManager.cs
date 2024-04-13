using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public enum EventType { SmallEvent, BigEvent};
    public List<EventType> eventSeq = new List<EventType> { EventType.SmallEvent, EventType.SmallEvent, EventType.SmallEvent, EventType.BigEvent};
    public int eventCounter = -1;

    public Sprite[] eventIcons;
    public BigEvent[] bigEvents = new BigEvent[] 
    
    { 
        new BigTestEvent("B-EV1", -1), 
        new BigTestEvent("B-EV2", -1), 
        new BigTestEvent("B-EV3", -1),
        new BigTestEvent("B-EV4", -1),
        new BigTestEvent("B-EV5", -1),
        new BigTestEvent("B-EV6", -1),
        new BigTestEvent("B-EV7", -1)
    };

    public SmallEvent[] smallEvents = new SmallEvent[]
    {
        new GenericSmallEvent("День Кенгуру", "Выжившие празднуют день кенгуру, часть свободных людей развлекаются", new (2, 0, -0.9f, 0.1f, 0f), new(-2, 0, 0.1f, 0.1f, 0f), false, true),
        new GenericSmallEvent("Повар отжигает", "Повар.. сам не свой, всё", new (0, 0, 0f, -0.9f, 0f), new(0, 0, 0f, 0.15f, 0f), false, true),
        new GenericSmallEvent("Кашель", "Похоже, что часть людей болеет", new (0, 0, 0f, 0f, 0.5f), new(0, 0, 0f, 0f, 0f), false, true),
        new GenericSmallEvent("Жвачка Джоржа", "Джорж уронил жвачку, а Боб её съел.. из о рта в рот (через пол) получается микроб", new (0, 0, 0f, 0f, 0f), new(0, 0, 0f, 0f, 0.05f), false, true),
        new GenericSmallEvent("Охота с бумерангом", "Стивен пошёл на охоту с бумерангом, у него получилось убить пару грибных косуль, но ему пришлось пожертвовать собой (стоило использовать лук)", new (0, 1, 0f, 0f, 0f), new(0, -1, 0f, 0f, 0.05f), true, true),
        new GenericSmallEvent("БУНТ!", "Бунтари собрались.. капут", new (0, 0, -0.2f, 0f, 0f), new(-4, 0, 0f, 0f, 0f), false, true),
        new GenericSmallEvent("На 3 метра ниже", "Селяни решили похоронить собаку, которую они нашли под землёй, копая проход", new (0, 0, 0.3f, 0f, 0f), new(-2, 0, 0f, 0f, 0f), false, true),
        new GenericSmallEvent("Новые селяне!", "Вам посчастливилось найти новых людей для своей колонии, но они оказались трупами :( ...ничего не изменилось...", new (0, 0, 0f, 0f, 0f), new(0, 0, 0f, 0f, 0f), true, true),
    };

    public bool testCastEvent;
    public bool testCastBigEvent;
    public bool testCastSmallEvent;
    public bool listEventDebug;

    const int limitAddition = 2;

    private void Awake()
    {
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
    private void CastRandEvent()
    {
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
        BigEvent eventToCast = (BigEvent)SelectMaxTimeEvent(bigEvents);
        UpdateAllEventCastTime(bigEvents);
        eventToCast.Cast();
        Debug.Log($"CASTED >{eventToCast.eventName}< EVENT");
    }
    private void CastSmallEvent()
    {
        SmallEvent eventToCast = (SmallEvent)SelectMaxTimeEvent(smallEvents);
        UpdateAllEventCastTime(smallEvents);
        eventToCast.Cast();
        Debug.Log($"CASTED >{eventToCast.eventName} | {eventToCast.eventDescription}< EVENT");
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
