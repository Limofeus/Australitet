using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
public class GenericEventResourseClass
{
    public int availablePeople;
    public int maxPeople;
    public float happinessPercent;
    public float hungryPercent;
    public float sickPercent;

    public GenericEventResourseClass(int availablePeople, int maxPeople, float happinessPercent, float hungryPercent, float sickPercent)
    {
        this.availablePeople = availablePeople;
        this.maxPeople = maxPeople;
        this.happinessPercent = happinessPercent;
        this.hungryPercent = hungryPercent;
        this.sickPercent = sickPercent;
    }
}

public class GenericSmallEvent : SmallEvent
{
    public GenericEventResourseClass resTestClass;
    public GenericEventResourseClass resAddClass;
    public bool oneTimeEvent = false;
    public bool unplayed = true;
    public bool memes;
    const bool memess = false;
    public override bool IsPossible()
    {
        bool isPossible = true;
        if (Totalres.people.Available < resTestClass.availablePeople) isPossible = false;
        if (resTestClass.maxPeople < 0 && Totalres.people.Max > -(resTestClass.maxPeople)) isPossible = false;
        if (resTestClass.maxPeople > 0 && Totalres.people.Max < resTestClass.maxPeople) isPossible = false;
        if (resTestClass.happinessPercent < 0 && (Totalres.people.Happy / Totalres.people.Max) > -(resTestClass.happinessPercent)) isPossible = false;
        if (resTestClass.happinessPercent > 0 && (Totalres.people.Happy / Totalres.people.Max) < resTestClass.happinessPercent) isPossible = false;
        if (resTestClass.hungryPercent < 0 && (Totalres.people.Hungry / Totalres.people.Max) > -(resTestClass.hungryPercent)) isPossible = false;
        if (resTestClass.hungryPercent > 0 && (Totalres.people.Hungry / Totalres.people.Max) < resTestClass.hungryPercent) isPossible = false;
        if (resTestClass.sickPercent < 0 && (Totalres.people.Sick / Totalres.people.Max) > -(resTestClass.sickPercent)) isPossible = false;
        if (resTestClass.sickPercent > 0 && (Totalres.people.Sick / Totalres.people.Max) < resTestClass.sickPercent) isPossible = false;
        if (!memess)
        {
            if (memes)
                return false;
        }
        return (isPossible && unplayed);
    }

    public override void OnDayStart()
    {
        if(oneTimeEvent)
            unplayed = false;
        Totalres.people.Available += resAddClass.availablePeople;
        Totalres.people.Max += resAddClass.maxPeople;
        Totalres.people.Happy += Mathf.FloorToInt(resAddClass.happinessPercent * Totalres.people.Max);
        Totalres.people.Hungry += Mathf.FloorToInt(resAddClass.hungryPercent * Totalres.people.Max);
        Totalres.people.Sick += Mathf.FloorToInt(resAddClass.sickPercent * Totalres.people.Max);
    }

    public GenericSmallEvent(string eventName, string eventDescription, GenericEventResourseClass resTestClass, GenericEventResourseClass resAddClass, bool oneTimeEvent, bool memes = false)
    {
        this.oneTimeEvent = oneTimeEvent;
        this.resTestClass = resTestClass;
        this.resAddClass = resAddClass;
        this.memes = memes;
    }
}
