using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        AudioManager.Instance.PlayOneShot("settingOpen");
    }

    public void Close() {
        this.gameObject.SetActive(false);
        AudioManager.Instance.PlayOneShot("settingClose");
    }

    public void GetValues() {
        int volume = PlayerPrefManager.Instance.GetVolume();
        int scroll = PlayerPrefManager.Instance.GetScroll();
        int offset = PlayerPrefManager.Instance.GetOffset();

        volumeSlider.SetValue(volume);
        scrollPanel.SetValue(scroll);
        offsetPanel.SetValue(offset);
    }

    public void UnlockAll() {
        PlayerPrefManager.Instance.UnlockAll();
        SceneManager.LoadScene("menu");
    }

    public void ResetSave() {
        PlayerPrefManager.Instance.ResetSave();
        SceneManager.LoadScene("menu");
    }
}
