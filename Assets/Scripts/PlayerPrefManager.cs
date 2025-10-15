using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerPrefManager : MonoBehaviour
{
    public static PlayerPrefManager Instance;

    public string[] levels = {
        "saron",
        "bonang",
        "kendang",
        "story1",
        "story2",
        "story3",
        "story4",
        "story5",
        "story6",
        "story7"
    };

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

        foreach(string key in levels) {
            if (!PlayerPrefs.HasKey(key)) {
                PlayerPrefs.SetInt(key, 0);
            }
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

    public void GetLevelCompletion(string key) {
        if (!levels.Contains(key)) {
            Debug.Log("level key not found!");
        }
        PlayerPrefs.GetInt(key);
    }

    public void SetLevelCompleted(string key) {
        if (!levels.Contains(key)) {
            Debug.Log("level key not found!");
        }
        PlayerPrefs.SetInt(key, 1);
    }

    public void ResetSave() {
       foreach(string key in levels) {
            if (!PlayerPrefs.HasKey(key)) {
                PlayerPrefs.SetInt(key, 0);
            }
        } 
    }

    public void UnlockAll() {
        foreach(string key in levels) {
            if (!PlayerPrefs.HasKey(key)) {
                PlayerPrefs.SetInt(key, 1);
            }
        } 
    }
}
