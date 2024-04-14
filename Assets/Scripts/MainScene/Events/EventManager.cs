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
        new BigTestEvent("������������� ����", -1),
        new NewPeopleBigEvent("���� �� �����������", "�� ��� ��������� ������ ����� �� ��������� ���������, ��� ������ �������, ������ ��� �� ���� ����� �� �������", 1, "��������", "�� �������"),
    };

    public SmallEvent[] smallEvents = new SmallEvent[]
    {      
        new GenericSmallEvent("���� �������", "�������� ��������� ���� �������, ����� ��������� ����� ������������", new (availablePeople:2, happinessPercent:-0.9f,sickPercent:0.1f), new(availablePeople:-2, happinessPercent:0.1f,sickPercent:0.1f), false, true),
        new GenericSmallEvent("����� ��������", "�����.. ��� �� ����, ��", new (hungryPercent:-0.9f), new(hungryPercent: 0.15f), false, true),
        new GenericSmallEvent("������", "������, ��� ����� ����� ������", new (sickPercent: 0.5f), new(), false),
        new GenericSmallEvent("������ ������", "����� ������ ������, � ��� � ����.. �� � ��� � ��� (����� ���) ���������� ������", new (), new(sickPercent: 0.05f), false, true),
        new GenericSmallEvent("����� � ����������", "������ ����� �� ����� � ����������, � ���� ���������� ����� ���� ������� ������, �� ��� �������� ������������ ����� (������ ������������ ���)", new (maxPeople:1), new(maxPeople:-1, sickPercent: 0.05f), true, true),
        new GenericSmallEvent("����!", "������� ���������.. �����", new (happinessPercent: -0.2f), new(availablePeople:-4), false, true),
        new GenericSmallEvent("�� 3 ����� ����", "������ ������ ���������� ������, ������� ��� ����� ��� �����, ����� ������", new (happinessPercent: 0.3f), new(availablePeople:2), false, true),
        new GenericSmallEvent("����� ������!", "��� ��������������� ����� ����� ����� ��� ����� �������, �� ��� ��������� ������� :( ...������ �� ����������...", new (), new(), true, true),
        new GenericSmallEvent("�� ��� ���������: ����", "������ ������� ������ ������� ���� ������ �����������", new(happinessPercent: 0.2f), new(availablePeople: -3, happinessPercent: 0.05f), true),
        new GenericSmallEvent("�� ��� ���������: ����", "������ ������� ������ ������� ���� ������ �����������", new(happinessPercent: 0.2f), new(availablePeople: -3, happinessPercent: 0.05f), true),
        new GenericSmallEvent("�� ��� ���������: ����", "������ ������� ������ ������� ���� ������ �����������", new(happinessPercent: 0.2f), new(availablePeople: -3, happinessPercent: 0.05f), true),
        new GenericSmallEvent("������� ������!", "������ ����� ���� ���������� � ���������� �� ����������� �� ������� ���", new(food: 30), new(food: -30, happinessPercent: 0.25f), true),
        new GenericSmallEvent("� � ��� ����", "���������� �������� ������. ��� ���� ������� ���!", new(), new(maxPeople: 2), true),
        new GenericSmallEvent("������!", "�� ����� ���������� ���������� ����������� ���� ���������� ������� ����������. ��������� ��������!", new(happinessPercent: 0.7f), new(happinessPercent: -0.1f), true),
        new GenericSmallEvent("������� ������", "���� ���������� � ������ � ����� ������� ��������� �������", new(rawfood: 15), new(rawfood: -15), true),
        new GenericSmallEvent("������ �� ��� ����", "��� ������ ����� ������ ���� � ������ � ����� ������������� ��������� ��������������", new(sickPercent: 0.5f), new(metal: -10, sickPercent: -0.08f, availablePeople:-1, happinessPercent: 0.05f), true),
        new GenericSmallEvent("����������������� �������", "��� ������������ ������� ��������� ��� �������� ��������� ������ ��� �������� ���������", new(rawfood: 5), new(rawfood: -5, happinessPercent: -0.03f), false),
        new GenericSmallEvent("��! �������!", "����� ������� ������� ��� ���� ����������", new(food: 10), new(food: -10, happinessPercent: -0.03f), false),
        new GenericSmallEvent("�������� �� ����", "������� ����� �� ������ ����� ��������� �����", new(), new(happinessPercent: -0.05f), false),
        new GenericSmallEvent("���� ��������", "� ���������� ������� ���� ��������! ��� ���������� �� ����������� �� ����", new(food: 10), new(rawfood: -5, happinessPercent: 0.05f), true),
        new GenericSmallEvent("������� ������������", "������ ���������������� � �������� �������, ����� ��� �� ������ ����������", new(rawfood: 10), new(rawfood: -10, food: 4, happinessPercent: 0.04f), false),
        new GenericSmallEvent("����� ������� ����", "������ ������� �� ���������-��� ������", new(), new(happinessPercent: 0.02f), false),
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
