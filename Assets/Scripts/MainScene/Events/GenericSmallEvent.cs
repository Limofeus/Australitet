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
    public int food;
    public int rawfood;
    public int metal;

    public GenericEventResourseClass(int availablePeople = 0, int maxPeople = 0, float happinessPercent = 0, float hungryPercent = 0, float sickPercent = 0, int food = 0, int rawfood = 0, int metal = 0)
    {
        this.availablePeople = availablePeople;
        this.maxPeople = maxPeople;
        this.happinessPercent = happinessPercent;
        this.hungryPercent = hungryPercent;
        this.sickPercent = sickPercent;
        this.food = food;
        this.rawfood = rawfood;
        this.metal = metal;
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

        if (resTestClass.food > 0 && (resTestClass.food > Totalres.food.CurrentValue)) isPossible = false;
        if (resTestClass.rawfood > 0 && (resTestClass.rawfood > Totalres.rawFood.CurrentValue)) isPossible = false;
        if (resTestClass.metal > 0 && (resTestClass.metal > Totalres.metal.CurrentValue)) isPossible = false;

        if (resTestClass.food < 0 && (-resTestClass.food < Totalres.food.CurrentValue)) isPossible = false;
        if (resTestClass.rawfood < 0 && (-resTestClass.rawfood < Totalres.rawFood.CurrentValue)) isPossible = false;
        if (resTestClass.metal < 0 && (-resTestClass.metal < Totalres.metal.CurrentValue)) isPossible = false;

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
        if(resAddClass.availablePeople > 0)
        {
            Totalres.people.Available += resAddClass.availablePeople;
        }
        else
        {
            Totalres.people.SetTimeout(-resAddClass.availablePeople);
        }
        Totalres.people.Max += resAddClass.maxPeople;
        Totalres.people.Available += resAddClass.maxPeople;
        Totalres.people.Happy += Mathf.Max(Mathf.FloorToInt(resAddClass.happinessPercent * Totalres.people.Max), 1);
        Totalres.people.Hungry += Mathf.Max(Mathf.FloorToInt(resAddClass.hungryPercent * Totalres.people.Max), 1);
        Totalres.people.Sick += Mathf.Max(Mathf.FloorToInt(resAddClass.sickPercent * Totalres.people.Max),1);
        Totalres.food.CurrentValue += resAddClass.food;
        Totalres.rawFood.CurrentValue += resAddClass.rawfood;
        Totalres.metal.CurrentValue += resAddClass.metal;
    }

    public GenericSmallEvent(string eventName, string eventDescription, GenericEventResourseClass resTestClass, GenericEventResourseClass resAddClass, bool oneTimeEvent, bool memes = false)
    {
        this.oneTimeEvent = oneTimeEvent;
        this.resTestClass = resTestClass;
        this.resAddClass = resAddClass;
        this.memes = memes;
        this.eventName = eventName;
        this.eventDescription = eventDescription;
    }
}
