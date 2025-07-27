using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementLine : MonoBehaviour
{
    public List<Queue<Note>> lanes = new List<Queue<Note>>();
    public int laneCount = 7;

    public LaneUIManager laneUiManager;

    public int combo; 
    public int score;

    public bool allPerfect = true;
    public bool fullCombo = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < laneCount; i++) {
            lanes.Add(new Queue<Note>());
        }

        InputManager.Instance.OnRhythmLaneInput += OnLaneInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToLane(Note note) {
        lanes[note.lane_id].Enqueue(note);
        note.OnDespawnEvent += OnDespawnNote;
    }

    void OnLaneInput(int input_id) {
        Queue<Note> queue = lanes[input_id];
        laneUiManager.TriggerLaneHighlight(input_id);

        if (queue.Count == 0) return;

        Note nextNote = queue.Peek();
        if (nextNote.canHit) {
            nextNote.OnDespawnEvent -= OnDespawnNote;
            queue.Dequeue();
            int hitValue = nextNote.GetScore();
            Destroy(nextNote.gameObject);

            if (hitValue == 0) {
                combo = 0;
            } else {
                combo++;
            }

            UpdateScore(hitValue);

            laneUiManager.UpdateComboCounter(combo);
            laneUiManager.UpdateJudgementDisplay(hitValue);

            if (hitValue >= 0) {
                laneUiManager.LaneEmit(input_id);
            }

            if (hitValue < 300) {
                allPerfect = false;
            } 

            if (hitValue == 0) {
                fullCombo = false;
            }
            
        }
    }

    void UpdateScore(int hitValue) {
        int maxScore = 1000000;
        int totalNote = MapDataManager.Instance.totalHitObjectCount;
        float scoreToAdd = (maxScore / totalNote) * (300f / 300f);
        score += (int) Mathf.Round(scoreToAdd);

        
        // need some way of rounding up the score to 100000 if we AP
        // if (MapDataManager.Instance.IsMapEmpty() && allPerfect) {
        //     score = maxScore;
        // }
    }

    void OnDespawnNote(Note note) {
        note.OnDespawnEvent -= OnDespawnNote;
        lanes[note.lane_id].Dequeue();
        Destroy(note.gameObject);

        Debug.Log("missed and despawned");
        combo = 0;
        laneUiManager.UpdateComboCounter(combo);
        laneUiManager.UpdateJudgementDisplay(0);
        AudioManager.Instance.PlayOneShot("combobreak");
    }
}
