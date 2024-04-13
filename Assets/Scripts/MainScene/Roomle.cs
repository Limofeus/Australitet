using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Roomle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _people;
    [SerializeField] private TextMeshProUGUI _food;
    [SerializeField] private TextMeshProUGUI _day;
    private int day = 1;

    private void Start()
    {
        AddText();
    }

    public void AddText()
    {
        _people.text = Totalres.people.Available + " / " + Totalres.people.Max;
        _food.text = Totalres.food.CurrentValue + " / " + Totalres.food.MaxValue;
        _day.text = "Day " + day;
    }
    public void NewDay()
    {
        Debug.Log("another day in paradise...");
        day++;
        //Totalres.people.Available += 2;
        Totalres.food.CurrentValue += 2;
        AddText();
        Totalres.OnTheEndOfDay();
    }
}
