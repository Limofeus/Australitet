/*
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Roomle : MonoBehaviour
{
    public void AddText()
    {
        _text.text = "";
        _text.text += "Total Resources";
        _text.text += "\nMaterials: " + _mine.ResourcesAmount;
        _text.text += "\nRaw food: " + _rawfood.ResourcesAmount;
        _text.text += "\nCooked food: " + _cookedfood.ResourcesAmount;
        _text.text += "\nPeople living: " + _people.ResourcesAmount;
        _text.text += "\nSinked: " + _hospital.ResourcesAmount;
        _text.text += "\nHungered: " + _cookedfood.Hungry;
    }
    public void NewDay()
    {
        Debug.Log("another day in paradise...");
        
        //mine mechanic
        _mine.AddResources(10);
        
        //food mechanic
        KitchenWork();

        //hospital mechanic
        HospitalWork();

        AddText();

    }
    private void HospitalWork()
    {
        float hospitalCapacity = _hospital.NumOfBuildings * 10;
        int cureCoef = Random.Range(5, 7);
        int sinkCoef = Random.Range(2, 5);
        int deathCoef = Random.Range(2, 4);

        //sikness
        if (_hospital.ResourcesAmount < _people.ResourcesAmount - _people.ResourcesAmount * 0.01f * sinkCoef)
            _hospital.ResourcesAmount += ((int)(_people.ResourcesAmount * 0.01f * sinkCoef));
            
        //cureness
        _hospital.ReduceResources((int)(_hospital.ResourcesAmount * 0.01f * cureCoef + _hospital.NumOfBuildings));

        //death
        if (_hospital.ResourcesAmount > hospitalCapacity)
        {
            _people.ReduceResources((int)(_hospital.ResourcesAmount * 0.01f * deathCoef));
            _hospital.ReduceResources((int)(_hospital.ResourcesAmount * 0.01f * deathCoef));
        }
    }
}
*/
