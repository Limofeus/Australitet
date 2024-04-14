using System;

public abstract class ActivatedRoom : Room
{
    public override void OnTheStartOfDay()
    {
        base.OnTheStartOfDay();

        if (Totalres.people.Available < 0)
            ClearPeople();
    }

    protected abstract void ClearPeople();
}
