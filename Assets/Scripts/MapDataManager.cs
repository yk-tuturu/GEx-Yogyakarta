using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Globalization;
using UnityEngine.SceneManagement;

public class MapDataManager : MonoBehaviour
{
    public static MapDataManager Instance; 

    public Queue<HitObjectData> mapData = new Queue<HitObjectData>();
    public int totalHitObjectCount; 

    public string title;
    public string subtitle;
    public string songFilename;
    public int delay;

    public Dictionary<string, string> generalInfo = new Dictionary<string, string>();

    public float timingOffset = -3f;

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

    void Start() {
        timingOffset = PlayerPrefManager.Instance.GetOffset();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "score" || scene.name == "menu") {
            Reset();
        }
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ReadMapFile(string filename) {
        timingOffset = PlayerPrefManager.Instance.GetOffset();
        TextAsset file = (TextAsset)Resources.Load("Maps/" + filename);

        using (StringReader sr = new StringReader(file.text))
        {
            string line;
            int counter = 0;
            string section = "";
            
            while ((line = sr.ReadLine()) != null)
            {
                if (line[0] == '[') {
                    section = line.Substring(1, line.Length - 2);
                    Debug.Log(section);
                    continue; 
                }

                if (section == "General") {
                    string[] info = line.Split(":");
                    generalInfo[info[0]] = info[1];
                } 
                
                else if (section == "HitObject") {
                    HitObjectData temp = new HitObjectData();
                    string[] info = line.Split(",");
                    temp.id = counter;
                    temp.lane = Int32.Parse(info[0]);
                    temp.targetTime = float.Parse(info[1], CultureInfo.InvariantCulture) - (timingOffset / 1000);
                    temp.hitsound = info[2];
                    mapData.Enqueue(temp);

                    counter++;
                }
                
            }
        }

        totalHitObjectCount = mapData.Count;

        title = generalInfo["Title"];
        subtitle = generalInfo["Subtitle"];
        songFilename = generalInfo["Audio"];
        delay = int.Parse(generalInfo["Delay"]);
    }

    public bool IsMapEmpty() {
        return mapData.Count == 0;
    }

    public HitObjectData PollNextNote() {
        if (mapData.Count == 0) return null;

        return mapData.Peek();
    }

    public HitObjectData Dequeue() {
        return mapData.Dequeue();
    }

    public void Reset() {
        mapData = new Queue<HitObjectData>();
        generalInfo.Clear();
        totalHitObjectCount = 0;

        title = "";
        subtitle = "";
        songFilename = "";

        Debug.Log("data reset!");
    }
}
