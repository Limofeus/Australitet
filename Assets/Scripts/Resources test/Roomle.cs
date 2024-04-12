using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Roomle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        AddText();
    }

    public void AddText()
    {
        _text.text = "";
        _text.text += "Total Resources";
        //_text.text += "\nMaterials: " + _mine.ResourcesAmount;
        _text.text += "\nRaw food: " + Totalres.rawFood.currentValue;
        _text.text += "\nCooked food: " + Totalres.food.currentValue;
        _text.text += "\nPeople living: " + Totalres.people.max;
        //_text.text += "\nSicked: " + Totalres.sickPeople.currentValue;
        //_text.text += "\nHungered: " + Totalres.people.maxValue * Totalres.hungryPeopleFraction;
    }
    public void NewDay()
    {
        Debug.Log("another day in paradise...");

        AddText();
        Totalres.OnTheEndOfDay();

    }
}
