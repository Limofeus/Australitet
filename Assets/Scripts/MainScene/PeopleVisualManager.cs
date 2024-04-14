using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleVisualManager : MonoBehaviour
{
    public static PeopleVisualManager Singleton;
    public int targetChelickCount;
    public GameObject mikrochelPref;
    List<Mikrochel> mikrochels = new List<Mikrochel>();
    public List<Vector2Int> roomCoords = new List<Vector2Int>();
    public List<Vector2Int> stairsCoords = new List<Vector2Int>();
    public bool upcheckRooms = false;

    private void Awake()
    {
        Singleton = this;
    }
    void Start()
    {
        for(int i = 0; i < targetChelickCount; i++)
        {
            var mikrochelToAdd = Instantiate(mikrochelPref, Vector3.zero, Quaternion.identity).GetComponent<Mikrochel>();
            mikrochelToAdd.UpdateChel();
            mikrochels.Add(mikrochelToAdd);
        }
    }

    public void SetCheliks()
    {
        UpcheckRooms();

        targetChelickCount = Math.Min(roomCoords.Count * 10, Totalres.people.Max);

        if(targetChelickCount < mikrochels.Count)
        {
            Mikrochel remMikro = mikrochels[0];
            mikrochels.Remove(remMikro);
            Destroy(remMikro.gameObject);
        }
        for (int i = 0; i < targetChelickCount; i++)
        {
            Mikrochel thisMickrochel;
            if(i >= mikrochels.Count)
            {
                var mikrochelToAdd = Instantiate(mikrochelPref, Vector3.zero, Quaternion.identity).GetComponent<Mikrochel>();
                mikrochels.Add(mikrochelToAdd);
                thisMickrochel = mikrochelToAdd;
            }
            else
            {
                thisMickrochel = mikrochels[i];
            }
            if(roomCoords.Count > 0)
            {
                thisMickrochel.coords = roomCoords[UnityEngine.Random.Range(0, roomCoords.Count)];
            }
            thisMickrochel.UpdateChel();
        }
    }
    public void UpcheckRooms()
    {
        roomCoords.Clear();
        stairsCoords.Clear();
        foreach(var room in BuildGrid.Singleton.rooms)
        {
            roomCoords.Add(room.roomCoords);
            if(room is Stairs)
            {
                stairsCoords.Add(room.roomCoords);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (upcheckRooms)
        {
            SetCheliks();
            upcheckRooms = false;
        }
    }
}
