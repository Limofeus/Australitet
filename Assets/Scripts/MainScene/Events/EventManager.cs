using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Sprite[] eventIcons;
    public List<BigEvent> bigEvents = new List<BigEvent>() { new BigTestEvent(), new BigTestEvent()};
    public List<SmallEvent> smallEvents;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
