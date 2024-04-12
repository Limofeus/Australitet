using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen :  Room
{
    int Capacity = 50;
    int Resources = 10;

    public override void OnTheEndOfDay()
    {
        if(IsActive) KitchenWork();
    }
    
    private void KitchenWork()
    {
        Totalres.hungry = (int)(Totalres.people - Totalres.food);

        if (Totalres.hungry < 0) Totalres.hungry = 0;

        //_cookedfood.ReduceResources(_Totalres.People - _Totalres.Hungry);

        int Hunger = Random.Range(3, 6);
        if (Totalres.hungry > Totalres.people * 0.5f)
        {
            Totalres.hungry = RedRes(Totalres.hungry, (int)(Totalres.people * 0.01f * Hunger));
            Totalres.people = RedRes(Totalres.people, (int)(Totalres.people * 0.01f * Hunger));
        }

        if (Totalres.food + 3 <= Totalres.maxFood)
        {
            if (Totalres.rawFood >= 5)
            {
                Totalres.food = AddRes(Totalres.food, 3);
                Totalres.rawFood = RedRes(Totalres.rawFood, 5);
            }
        }
        
        Totalres.rawFood = AddRes(Totalres.rawFood, 5);
    }
    public int AddRes(int res, int num)
    {
        if (res + num < Capacity)
            res += num;
        else res = Capacity;

        return res;
    }
    public int RedRes(int res, int num)
    {
        if (res >= num) res -= num;
        else res = 0;

        return res;
    }
}
