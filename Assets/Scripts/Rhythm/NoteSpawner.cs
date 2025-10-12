using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Globalization;

public class NoteSpawner : MonoBehaviour
{
    public Queue<HitObjectData> mapData = new Queue<HitObjectData>();
    public float scrollSpeed; 
    public float spawnOffset; 

    public GameObject notePrefab;

    public JudgementLine judgementLine; 
    public Transform positionMarkersParent;

    public List<Transform> positionMarkers;

    public float timingOffset;

    // Start is called before the first frame update
    void Start()
    {
        scrollSpeed = PlayerPrefManager.Instance.GetScroll();

        foreach (Transform child in positionMarkersParent) {
            positionMarkers.Add(child);
        }

        spawnOffset = 10000f / (scrollSpeed * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        if (MapDataManager.Instance.IsMapEmpty()) return; 

        HitObjectData nextNote = MapDataManager.Instance.PollNextNote();
        float songPos = MusicManager.Instance.GetSongPos();

        while (nextNote.targetTime - songPos < spawnOffset) {
            MapDataManager.Instance.Dequeue();
            SpawnNote(nextNote.id, nextNote.lane, nextNote.targetTime, nextNote.hitsound);

            if (MapDataManager.Instance.IsMapEmpty()) {
                break;
            }
            nextNote = MapDataManager.Instance.PollNextNote();
        } 
    }

    void SpawnNote(int id, int lane_id, float timing, string hitsound) {
        Vector3 notePosition = positionMarkers[lane_id].position;

        GameObject note = Instantiate(notePrefab, notePosition, Quaternion.identity);
        Note noteScript = note.GetComponent<Note>();
        noteScript.id = id;
        noteScript.targetTime = timing; 
        noteScript.lane_id = lane_id;
        noteScript.hitsound = hitsound;

        judgementLine.AddToLane(noteScript);
    }

    // public void ReadMapFile(string filename) {
    //     TextAsset file = (TextAsset)Resources.Load("Maps/" + filename);

    //     using (StringReader sr = new StringReader(file.text))
    //     {
    //         string line;
            
    //         while ((line = sr.ReadLine()) != null)
    //         {
    //             HitObjectData temp = new HitObjectData();
    //             string[] info = line.Split(",");
    //             temp.lane = Int32.Parse(info[0]);
    //             temp.targetTime = float.Parse(info[1], CultureInfo.InvariantCulture);
    //             temp.hitsound = info[2];
    //             mapData.Enqueue(temp);
    //         }
    //     }
    // }
}
