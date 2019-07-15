using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private string _name;
    private int _score;

    [SerializeField] private Text[] _texts;
    
    private void OnDisable()
    {
        _name = "";
        foreach (var t in _texts)
        {
            _name += t.text;
        }

        Saver.instance.PlayerName = _name;
        Saver.instance.SaveUser();
    }
}
