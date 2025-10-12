using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScrollSpeedPanel : MonoBehaviour
{
    public int scrollSpeed;

    public TextMeshProUGUI scrollText;
    
    public void SetValue(int value) {
        scrollText.text = value.ToString();
        scrollSpeed = value;
    }

    public void Add() {
        scrollSpeed = Math.Min(30, scrollSpeed+1);
        scrollText.text = scrollSpeed.ToString();
        PlayerPrefManager.Instance.SetScroll(scrollSpeed);
    }

    public void Minus() {
        scrollSpeed = Math.Max(1, scrollSpeed-1);
        scrollText.text = scrollSpeed.ToString();
        PlayerPrefManager.Instance.SetScroll(scrollSpeed);
    }
}
