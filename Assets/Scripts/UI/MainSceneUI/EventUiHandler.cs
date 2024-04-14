using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventUiHandler : MonoBehaviour
{
    public static EventUiHandler Singleton;
    [SerializeField] private GameObject smallEventWindow;
    [SerializeField] private TextMeshProUGUI smallEventNameText;
    [SerializeField] private TextMeshProUGUI smallEventDescText;
    [SerializeField] private GameObject bigEventWindow;
    public Image bigEventIcon;
    public TextMeshProUGUI bigEventNameText;
    public TextMeshProUGUI bigEventDescText;
    public GameObject bigEventButtonPrefab;
    public Transform bigEventButtonHolder;
    public BigEvent currentBigEvent;

    private List<BigEventButton> bigEventButtons = new List<BigEventButton>();

    private void Awake()
    {
        Singleton = this;
    }

    public void HideEventWindows()
    {
        smallEventWindow.SetActive(false);
        bigEventWindow.SetActive(false);
    }

    public void SetSmallEvent(string name, string desc)
    {
        smallEventNameText.text = name;
        smallEventDescText.text = desc;
        smallEventWindow.SetActive(true);
    }

    public void SetBigEvent(BigEvent bigEvent)
    {
        if(bigEvent.eventIconId >= 0)
        {
            bigEventIcon.sprite = EventManager.Singleton.eventIcons[bigEvent.eventIconId];
        }
        bigEventNameText.text = bigEvent.eventName;
        bigEventDescText.text = bigEvent.eventDescription;

        foreach(var bb in bigEventButtons)
        {
            Destroy(bb.gameObject);
        }
        bigEventButtons.Clear();

        for(int i = 0; i < bigEvent.buttonTexts.Length; i++)
        {
            BigEventButton bigEventButton = Instantiate(bigEventButtonPrefab, bigEventButtonHolder).GetComponent<BigEventButton>();
            bigEventButtons.Add(bigEventButton);
            bigEventButton.Init(this, i, bigEvent.buttonTexts[i]);
        }

        currentBigEvent = bigEvent;
        bigEventWindow.SetActive(true);
    }

    public void CastEventChoise(int choiceid)
    {
        currentBigEvent.ButtonPressed(choiceid);
        bigEventWindow.SetActive(false);
    }
}
