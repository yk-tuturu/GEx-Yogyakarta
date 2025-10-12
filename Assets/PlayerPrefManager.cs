using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{
    public static PlayerPrefManager Instance;

    void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (!PlayerPrefs.HasKey("volume")) {
            PlayerPrefs.SetInt("volume", 100);
        } 

        if (!PlayerPrefs.HasKey("scroll")) {
            PlayerPrefs.SetInt("scroll", 15);
        }

        if (!PlayerPrefs.HasKey("offset")) {
            PlayerPrefs.SetInt("offset", -3);
        }
    }

    public int GetVolume() {
        return PlayerPrefs.GetInt("volume");
    }

    public int GetScroll() {
        return PlayerPrefs.GetInt("scroll");
    }

    public int GetOffset() {
        return PlayerPrefs.GetInt("offset");
    }

    public void SetVolume(int volume) {
        PlayerPrefs.SetInt("volume", volume);
    }

    public void SetScroll(int scroll) {
        PlayerPrefs.SetInt("scroll", scroll);
    }

    public void SetOffset(int offset) {
        PlayerPrefs.SetInt("offset", offset);
    }
}
