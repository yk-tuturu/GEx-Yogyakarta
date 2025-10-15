using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OffsetPanel : MonoBehaviour
{
    public int offset;
    public TextMeshProUGUI offsetText; 

    public void SetValue(int value) {
        offsetText.text = value.ToString();
        offset = value;
    }

    public void Add() {
        offset = Math.Min(100, offset+1);
        offsetText.text = offset.ToString();
        PlayerPrefManager.Instance.SetOffset(offset);
    }

    public void Minus() {
        offset = Math.Max(-100, offset-1);
        offsetText.text = offset.ToString();
        PlayerPrefManager.Instance.SetOffset(offset);
    }
}
