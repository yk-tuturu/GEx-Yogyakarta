using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Globalization;

public class MapDataManager : MonoBehaviour
{
    public static MapDataManager Instance; 

    public Queue<HitObjectData> mapData = new Queue<HitObjectData>();
    public int totalHitObjectCount; 

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ReadMapFile(RhythmGameLoader.Instance.mapName);
        totalHitObjectCount = mapData.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadMapFile(string filename) {
        TextAsset file = (TextAsset)Resources.Load("Maps/" + filename);

        using (StringReader sr = new StringReader(file.text))
        {
            string line;
            int counter = 0;
            
            while ((line = sr.ReadLine()) != null)
            {
                HitObjectData temp = new HitObjectData();
                string[] info = line.Split(",");
                temp.id = counter;
                temp.lane = Int32.Parse(info[0]);
                temp.targetTime = float.Parse(info[1], CultureInfo.InvariantCulture);
                temp.hitsound = info[2];
                mapData.Enqueue(temp);

                counter++;
            }
        }
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
}
