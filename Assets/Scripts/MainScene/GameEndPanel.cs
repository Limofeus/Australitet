using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _gameEndName;
    [SerializeField] private TMP_Text _gameEndDescription;

    public void Init(string name, string description)
    {
        _gameEndName.text = name;
        _gameEndDescription.text = description; 
    }
}
