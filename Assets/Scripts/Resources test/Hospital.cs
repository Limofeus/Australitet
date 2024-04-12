using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : Room
{
    int Capacity = 10;

    protected override void RoomWork()
    {
        var cureCoef = Random.Range(5, 7) * 0.01f + Totalres.numOfHospitals;
        
        //cureness
        Totalres.RedRes(ref Totalres.sickPeople, Totalres.sickPeople.currentValue * (int)cureCoef);
    }
}



