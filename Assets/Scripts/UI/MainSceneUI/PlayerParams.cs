using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerParams : MonoBehaviour
{
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

    private void Start()
    {
        AddText();
    }

    public void OnNewDay()
    {
        _sickPeopleFraction = (int)(Totalres.GetSickyPeopleFraction() * 100);
        _hungryPeopleFraction = (int)(Totalres.GetHungryPeopleFraction() * 100);
        _dayNum++;
        AddText();
        
    }

    private void AddText()
    {
        DayCounter.text = "Day " + _dayNum;
        PeopleCounter.text = Totalres.people.Available + " / " + Totalres.people.max;
        FoodCounter.text = Totalres.food.currentValue + " / " + Totalres.food.maxValue;
        RawFoodCounter.text = Totalres.rawFood.currentValue + " / " + Totalres.rawFood.maxValue;
        SickPeopleCounter.text = _sickPeopleFraction + "%";
        HungryPeopleCounter.text = _hungryPeopleFraction + "%";
        //MaterialsCounter.text = Totalres.materials.currentValue + " / " + Totalres.materials.maxValue;
    }
}
