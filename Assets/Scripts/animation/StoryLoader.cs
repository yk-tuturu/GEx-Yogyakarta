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


    void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadStoryScene(string storyFile) {
        this.storyFile = storyFile;
        SceneManager.LoadScene("story");
    }

}
