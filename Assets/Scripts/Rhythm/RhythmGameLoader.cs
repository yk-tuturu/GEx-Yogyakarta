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

    void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Scene currentScene = SceneManager.GetActiveScene();
        // if (currentScene.name == "rhythm") {
        //     GameObject noteSpawnerObject = GameObject.FindWithTag("NoteSpawner");
        //     NoteSpawner ns = noteSpawnerObject.GetComponent<NoteSpawner>();
        //     ns.ReadMapFile(mapName);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadRhythmScene(string mapName) {
        this.mapName = mapName;
        StartCoroutine(LoadAssetsAndScene());
    }

    IEnumerator LoadAssetsAndScene() {
        AsyncOperationHandle<IList<AudioClip>> handle =
            Addressables.LoadAssetsAsync<AudioClip>(mapName, clip => {
                hitsounds.Add(clip);
            });
        
        yield return handle; // Wait for loading to finish

        var BGMHandle = Addressables.LoadAssetAsync<AudioClip>($"Assets/Sounds/BGM/{mapName}.wav");
        yield return BGMHandle;

        if (BGMHandle.Status == AsyncOperationStatus.Succeeded)
        {
            bgm = BGMHandle.Result;
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
}
