using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerParams : MonoBehaviour
{
    public static PlayerParams Singleton;
    
    public TextMeshProUGUI DayCounter;
    public TextMeshProUGUI PeopleCounter;
    public TextMeshProUGUI SickPeopleCounter;
    public TextMeshProUGUI HungryPeopleCounter;
    public TextMeshProUGUI FoodCounter;
    public TextMeshProUGUI RawFoodCounter;
    public TextMeshProUGUI MaterialsCounter;
    
    private int _dayNum = 1;

    private void Awake()
    {
        Singleton = this;
        Totalres.people.SetTimeout(0);
    }

    private void Start()
    {
        AddText();
    }

    public void UpdateParams()
    {
        AddText();
    }

    public void OnNewDay()
    {
        _dayNum++;
        AddText();

        EventManager.Singleton.CastRandEvent();

        foreach (var room in BuildGrid.Singleton.rooms)
        {
            room.OnTheEndOfDay();
        }
        Totalres.OnTheEndOfDay();
    }

    private void AddText()
    {
        DayCounter.text = "Δενό " + _dayNum;
        PeopleCounter.text = Totalres.people?.Available + " / " + Totalres.people?.Max;
        FoodCounter.text = Totalres.food?.CurrentValue + " / " + Totalres.food?.MaxValue;
        RawFoodCounter.text = Totalres.rawFood?.CurrentValue + " / " + Totalres.rawFood?.MaxValue;
        SickPeopleCounter.text = (int)(Totalres.GetSickyPeopleFraction() * 100) + "%";
        HungryPeopleCounter.text = (int)(Totalres.GetHungryPeopleFraction() * 100) + "%";
        //MaterialsCounter.text = Totalres.materials.currentValue + " / " + Totalres.materials.maxValue;
    }
}
