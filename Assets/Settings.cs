using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public VolumeSlider volumeSlider;
    public ScrollSpeedPanel scrollPanel;
    public OffsetPanel offsetPanel;
    // Start is called before the first frame update
    void Start()
    {
        GetValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open() {
        this.gameObject.SetActive(true);
        GetValues();
    }

    public void Close() {
        this.gameObject.SetActive(false);
    }

    public void GetValues() {
        int volume = PlayerPrefManager.Instance.GetVolume();
        int scroll = PlayerPrefManager.Instance.GetScroll();
        int offset = PlayerPrefManager.Instance.GetOffset();

        volumeSlider.SetValue(volume);
        scrollPanel.SetValue(scroll);
        offsetPanel.SetValue(offset);
    }
}
