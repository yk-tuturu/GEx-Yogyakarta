using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

// this will load the next rhythm scene and act as a holder for all the audio clips 
public class StoryLoader : MonoBehaviour
{
    public static StoryLoader Instance; 
    public string storyFile;
    public string title;
    public string subtitle;


    void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadStoryScene(string storyFile, string title, string subtitle) {
        this.storyFile = storyFile;
        this.title = title;
        this.subtitle = subtitle;
        SceneManager.LoadScene("story");
    }

}
