using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class VolumeSlider : MonoBehaviour
{
    public int volume;
    public Slider slider;
    public TextMeshProUGUI volumeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(int value) {
        volume = value;
        slider.value = value;
        volumeText.text = value.ToString() + "%";
        AudioManager.Instance.SetVolume(volume);
    }

    public void OnSliderChange() {
        volume = (int) Math.Floor(slider.value);
        SetValue(volume);
    }

    public void OnPointerUp() {
        PlayerPrefManager.Instance.SetVolume(volume);
    }
}
