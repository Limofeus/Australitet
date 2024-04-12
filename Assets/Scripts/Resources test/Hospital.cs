using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : Room
{
    int Capacity = 10;

    protected override void RoomWork()
    {
        //if(IsActive) HospitalWork();
    }
    /*private void HospitalWork()
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
    }*/
    
    public int RedRes(int res, int num)
    {
        if (res >= num) res -= num;
        else res = 0;

        return res;
    }
}


