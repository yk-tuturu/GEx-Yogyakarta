using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhythmGameLoader : MonoBehaviourSingleton<RhythmGameLoader>
{
    public string mapName; 
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
        SceneManager.LoadScene("rhythm");
    }
}
