using UnityEngine;

public class Hospital : Room
{
    private int _hospitalCapacity = 30;

    protected override void RoomWork()
    {
        var curePeople = Mathf.Min(_hospitalCapacity, Totalres.sickPeople);

    }
}



