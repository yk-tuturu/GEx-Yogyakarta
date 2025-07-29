using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

// this will load the next rhythm scene and act as a holder for all the audio clips 
public class RhythmGameLoader : MonoBehaviour
{
    public static RhythmGameLoader Instance; 
    public string mapName; 

    public List<AudioClip> hitsounds; 
    public AudioClip bgm; 

    public event Action fadeToBlack;
    public event Action scoreScreenTransition;

    public AsyncOperationHandle<IList<AudioClip>> hitsoundHandle;
    public AsyncOperationHandle<AudioClip> bgmHandle;


    void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "rhythm") {
            MusicManager.Instance.onSongEnded += OnSongEnded;
        }
    }

    public void LoadRhythmScene(string mapName) {
        this.mapName = mapName;
        StartCoroutine(LoadRhythmAssetsAndScene());
    }

    public void OnSongEnded() {
        MusicManager.Instance.onSongEnded -= OnSongEnded;
        StartCoroutine(LoadScoreScreen());
    }

    IEnumerator LoadRhythmAssetsAndScene() {
        MapDataManager.Instance.ReadMapFile(mapName);

        hitsoundHandle = Addressables.LoadAssetsAsync<AudioClip>(mapName, clip => {
            hitsounds.Add(clip);
        });
        
        yield return hitsoundHandle; // Wait for loading to finish

        var songFilename = MapDataManager.Instance.songFilename;
        bgmHandle = Addressables.LoadAssetAsync<AudioClip>($"Assets/Sounds/BGM/{songFilename}");
        yield return bgmHandle;

        if (bgmHandle.Status == AsyncOperationStatus.Succeeded)
        {
            bgm = bgmHandle.Result;
        }

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync("rhythm");
        asyncOp.allowSceneActivation = false;  

        while (!asyncOp.isDone)
        {
            if (asyncOp.progress >= 0.9f)
            {
                fadeToBlack?.Invoke();

                yield return new WaitForSeconds(0.6f);

                asyncOp.allowSceneActivation = true;  
            }

            yield return null;
        }
    }

    IEnumerator LoadScoreScreen() {
        scoreScreenTransition?.Invoke();

        MapDataManager.Instance.Reset();

        yield return new WaitForSeconds(1.3f);

        if (hitsoundHandle.IsValid())
            Addressables.Release(hitsoundHandle);  // Releases all hitsounds

        if (bgmHandle.IsValid())
            Addressables.Release(bgmHandle);       // Releases the BGM clip

        hitsounds.Clear();
        bgm = null;

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync("score");
        asyncOp.allowSceneActivation = false;  

        while (!asyncOp.isDone)
        {
            if (asyncOp.progress >= 0.9f)
            {
                asyncOp.allowSceneActivation = true;  
            }

            yield return null;
        }
    }
}
