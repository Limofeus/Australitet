using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mikrochel : MonoBehaviour
{
    enum MikBehavior {RoamRoom = 0, RoamDungeon = 1, NumberOfBehs };
    MikBehavior currBeh = MikBehavior.RoamRoom;
    public Vector2Int coords;
    public Vector2 inRoomCoords;
    private Vector2 minMaxPosChangeTime = new (2f, 4f);
    private Vector2 xMinMaxRoomPos = new(1f, 7f);
    private Vector2 zMinMaxRoomPos = new(0f, 2f);
    [SerializeField] private AnimationCurve moveInterp;
    [SerializeField] private AnimationCurve scaleInterp;
    private float behChangeTimer;
    const float behChangeTime = 10f;
    const float behChangeChance = 0.5f;
    const float roomChangeTime = 0.2f;
    public bool posChanging = false;
    private float rotToSin = 0f;
    [SerializeField] private float rotToSinMod = 100f;
    [SerializeField] private float rotModif = 5f;
    public void UpdateChel()
    {
        inRoomCoords = new Vector2(Random.Range(xMinMaxRoomPos.x, xMinMaxRoomPos.y), Random.Range(zMinMaxRoomPos.x, zMinMaxRoomPos.y));
        behChangeTimer = Random.Range(0, behChangeTime);
        rotToSin = Random.Range(0f, Mathf.PI * 2);
    }

    private void Update()
    {
        if(behChangeTimer >= behChangeTime)
        {
            behChangeTimer = 0f;
            if(Random.Range(0f, 1f) < behChangeChance)
            {
                currBeh = (MikBehavior)Random.Range(0, (int)MikBehavior.NumberOfBehs);
                if(PeopleVisualManager.Singleton.stairsCoords.Count > 0)
                {
                    if (PeopleVisualManager.Singleton.stairsCoords.Contains(coords))
                    {
                        currBeh = MikBehavior.RoamDungeon;
                    }
                }
                Debug.Log($"HUMAN BEH CHANGE to >{currBeh}<");
                if(PeopleVisualManager.Singleton.roomCoords.Count < 2)
                {
                    currBeh = MikBehavior.RoamRoom;
                }
            }
        }
        behChangeTimer += Time.deltaTime;
        transform.position = new Vector3(BuildGrid.Singleton.cellWidth * coords.x + inRoomCoords.x, BuildGrid.Singleton.cellHeight * coords.y, inRoomCoords.y);
        transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(rotToSin * rotToSinMod) * rotModif);
        if (!posChanging) StartCoroutine(PosChange());
    }

    private IEnumerator PosChange()
    {
        posChanging = true;
        float posChangeTimer = 0f;
        float posChangeEndTime = Random.Range(minMaxPosChangeTime.x, minMaxPosChangeTime.y);
        Vector2 startPos = new Vector2(inRoomCoords.x, inRoomCoords.y);
        Vector2 endPos = Vector2.zero;
        Vector2 lastPos = startPos;

        switch (currBeh)
        {
            case MikBehavior.RoamRoom:
                endPos = new Vector2(Random.Range(xMinMaxRoomPos.x, xMinMaxRoomPos.y), Random.Range(zMinMaxRoomPos.x, zMinMaxRoomPos.y));
                while (posChangeTimer < posChangeEndTime)
                {
                    float posChangeProgress = Mathf.Clamp01(posChangeTimer / posChangeEndTime);
                    inRoomCoords = Vector2.Lerp(startPos, endPos, moveInterp.Evaluate(posChangeProgress));
                    posChangeTimer += Time.deltaTime;

                    rotToSin += Vector2.Distance(lastPos, inRoomCoords);

                    lastPos = inRoomCoords;
                    yield return null;
                }
                break;
            case MikBehavior.RoamDungeon:
                List<string> possibleChanges = new List<string>();

                if (PeopleVisualManager.Singleton.roomCoords.Contains(coords + Vector2Int.right))
                    possibleChanges.Add("R");
                if (PeopleVisualManager.Singleton.roomCoords.Contains(coords + Vector2Int.left))
                    possibleChanges.Add("L");
                if (PeopleVisualManager.Singleton.stairsCoords.Contains(coords))
                {
                    if (PeopleVisualManager.Singleton.stairsCoords.Contains(coords + Vector2Int.up))
                        possibleChanges.Add("U");
                    if (PeopleVisualManager.Singleton.stairsCoords.Contains(coords + Vector2Int.down))
                        possibleChanges.Add("D");
                }

                string dirname = possibleChanges[Random.Range(0, possibleChanges.Count)];

                Vector2Int nextCoord = Vector2Int.zero;
                Vector2 nextRPos = Vector2.zero;

                switch (dirname)
                {
                    case "R":
                        nextCoord = coords + Vector2Int.right;
                        endPos = new Vector2(7f, 1f);
                        nextRPos = new Vector2(1f, 1f);
                        break;
                    case "L":
                        nextCoord = coords + Vector2Int.left;
                        endPos = new Vector2(1f, 1f);
                        nextRPos = new Vector2(7f, 1f);
                        break;
                    case "U":
                        nextCoord = coords + Vector2Int.up;
                        endPos = new Vector2(4f, 3f);
                        nextRPos = new Vector2(4f, 3f);
                        break;
                    case "D":
                        nextCoord = coords + Vector2Int.down;
                        endPos = new Vector2(4f, 3f);
                        nextRPos = new Vector2(4f, 3f);
                        break;
                }

                while (posChangeTimer < posChangeEndTime)
                {
                    float posChangeProgress = Mathf.Clamp01(posChangeTimer / posChangeEndTime);
                    inRoomCoords = Vector2.Lerp(startPos, endPos, moveInterp.Evaluate(posChangeProgress));
                    posChangeTimer += Time.deltaTime;

                    rotToSin += Vector2.Distance(lastPos, inRoomCoords);

                    lastPos = inRoomCoords;
                    yield return null;
                }

                float roomChangeTimer = 0f;
                while (roomChangeTimer < roomChangeTime)
                {
                    transform.localScale = Vector3.one * scaleInterp.Evaluate(roomChangeTimer / roomChangeTime);
                    roomChangeTimer += Time.deltaTime;
                    yield return null;
                }
                coords = nextCoord;
                inRoomCoords = nextRPos;
                roomChangeTimer = roomChangeTime;
                while (roomChangeTimer > 0f)
                {
                    transform.localScale = Vector3.one * scaleInterp.Evaluate(roomChangeTimer / roomChangeTime);
                    roomChangeTimer -= Time.deltaTime;
                    yield return null;
                }
                transform.localScale = Vector3.one;
                currBeh = MikBehavior.RoamRoom;

                break;
        }

        posChanging = false;
    }
}
