using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : Room
{
    int Capacity = 50;
    int Resources = 10;

    public override void OnTheEndOfDay()
    {
        if(IsActive) GardenWork();
    }
    
    private void GardenWork()
    {
        Totalres.rawFood = AddRes(Totalres.rawFood, 5);
    }
    public int AddRes(int res, int num)
    {
        if (res + num < Capacity)
            res += num;
        else res = Capacity;

        return res;
    }
}
