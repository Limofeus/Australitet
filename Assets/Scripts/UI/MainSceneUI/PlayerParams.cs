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
    private int _sickPeopleFraction;
    private int _hungryPeopleFraction;

    private void Awake()
    {
        Singleton = this;
        Totalres.people.Timeout += 0;
    }

    private void Start()
    {
        AddText();
    }

    public void UpdateParams()
    {
        ClacFraction();
        AddText();
    }

    public void OnNewDay()
    {
        ClacFraction();
        _dayNum++;
        AddText();
        
    }

    public void ClacFraction()
    {
        _sickPeopleFraction = (int)(Totalres.GetSickyPeopleFraction() * 100);
        _hungryPeopleFraction = (int)(Totalres.GetHungryPeopleFraction() * 100);
    }
    

    private void AddText()
    {
        Debug.Log(Totalres.people);
        DayCounter.text = "Day " + _dayNum;
        PeopleCounter.text = Totalres.people?.Available + " / " + Totalres.people?.Max;
        FoodCounter.text = Totalres.food?.CurrentValue + " / " + Totalres.food?.MaxValue;
        RawFoodCounter.text = Totalres.rawFood?.CurrentValue + " / " + Totalres.rawFood?.MaxValue;
        SickPeopleCounter.text = _sickPeopleFraction + "%";
        HungryPeopleCounter.text = _hungryPeopleFraction + "%";
        //MaterialsCounter.text = Totalres.materials.currentValue + " / " + Totalres.materials.maxValue;
    }
}
