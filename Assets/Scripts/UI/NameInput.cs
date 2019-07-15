using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    private string _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    [SerializeField] private char _letter;
    [SerializeField] private int _number;
    [SerializeField] private Text _text;
    
    public void ChangeLetter(bool plus)
    {
        if (plus)
        {
            if (_number < _letters.Length-1)
                _number++;
            else _number = 0;
        }
        else
        {
            if (_number > 0)
                _number--;
            else _number = _letters.Length - 1;
        }
        _letter = _letters[_number];
        _text.text = _letter.ToString();
    }
}
