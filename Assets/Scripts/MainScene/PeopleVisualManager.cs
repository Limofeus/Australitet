using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleVisualManager : MonoBehaviour
{
    public static PeopleVisualManager Singleton;
    public float startMikrochels;
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
        for(int i = 0; i < startMikrochels; i++)
        {
            var mikrochelToAdd = Instantiate(mikrochelPref, Vector3.zero, Quaternion.identity).GetComponent<Mikrochel>();
            mikrochelToAdd.UpdateChel();
            mikrochels.Add(mikrochelToAdd);
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
            UpcheckRooms();
            upcheckRooms = false;
        }
    }
}
