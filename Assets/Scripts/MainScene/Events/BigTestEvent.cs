using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTestEvent : BigEvent
{
    public override void ButtonPressed(int buttonId)
    {
        switch (buttonId)
        {
            case 0:
                Debug.Log("������� ����� 1");
                GiveRoom();
                break;
            case 1:
                Debug.Log("2");
                break;
        }
    }
    private void GiveRoom()
    {
        //����� ������� ����.
        //����.
        //.���������
        //������ �������.
    }

    public override bool IsPossible()
    {
        return true;
    }
    public override void OnDayStart()
    {
        eventDescription = "��� ���� �� ����� 2 �����";
        buttonTexts = new string[] { "���", "����" };
        eventIconId = 0;
    }

    public BigTestEvent(string nameE, int tse)
    {
        eventName = nameE;
        timeSinceEvent = tse;
    }
}
