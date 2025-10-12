using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementLine : MonoBehaviour
{
    public List<Queue<Note>> lanes = new List<Queue<Note>>();
    public int laneCount = 7;

    public LaneUIManager laneUiManager;
    public bool auto = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < laneCount; i++) {
            lanes.Add(new Queue<Note>());
        }

        InputManager.Instance.OnRhythmLaneInput += OnLaneInput;
    }

    void OnDestroy() {
        InputManager.Instance.OnRhythmLaneInput -= OnLaneInput;
    }

    public void AddToLane(Note note) {
        lanes[note.lane_id].Enqueue(note);
        note.OnDespawnEvent += OnDespawnNote;

        if (auto) {
            note.OnAutoHit += OnAutoHit;
        }
    }

    void OnAutoHit(Note note) {
        note.OnDespawnEvent -= OnDespawnNote;
        note.OnAutoHit -= OnAutoHit;
        int hitValue = note.GetScore();
        int input_id = note.lane_id;
        bool isLastNote = note.id == MapDataManager.Instance.totalHitObjectCount - 1;

        Queue<Note> queue = lanes[input_id];
        queue.Dequeue();
        Destroy(note.gameObject);

        ScoreManager.Instance.UpdateComboAndScore(hitValue, isLastNote);
        laneUiManager.UpdateComboCounter();
        laneUiManager.UpdateJudgementDisplay();
        laneUiManager.UpdateScoreDisplay();
        laneUiManager.UpdateAccuracyDisplay();

        if (hitValue >= 0) {
            laneUiManager.LaneEmit(input_id);
        }
    }

    void OnLaneInput(int input_id) {
        if (PauseManager.Instance.isPaused) {
            return;
        }
        
        Queue<Note> queue = lanes[input_id];
        laneUiManager.TriggerLaneHighlight(input_id);

        if (queue.Count == 0) return;

        Note nextNote = queue.Peek();
        if (nextNote.canHit) {
            nextNote.OnDespawnEvent -= OnDespawnNote;
            int hitValue = nextNote.GetScore();
            bool isLastNote = nextNote.id == MapDataManager.Instance.totalHitObjectCount - 1;
            queue.Dequeue();
            Destroy(nextNote.gameObject);

            ScoreManager.Instance.UpdateComboAndScore(hitValue, isLastNote);
            laneUiManager.UpdateComboCounter();
            laneUiManager.UpdateJudgementDisplay();
            laneUiManager.UpdateScoreDisplay();
            laneUiManager.UpdateAccuracyDisplay();

            if (hitValue >= 0) {
                laneUiManager.LaneEmit(input_id);
            }
            
        }
    }

    void OnDespawnNote(Note note) {
        note.OnDespawnEvent -= OnDespawnNote;
        bool isLastNote = note.id == MapDataManager.Instance.totalHitObjectCount - 1;
        lanes[note.lane_id].Dequeue();
        Destroy(note.gameObject);

        Debug.Log("missed and despawned");

        ScoreManager.Instance.UpdateComboAndScore(0, isLastNote);
        laneUiManager.UpdateComboCounter();
        laneUiManager.UpdateJudgementDisplay();
        laneUiManager.UpdateAccuracyDisplay();

        AudioManager.Instance.PlayOneShot("combobreak");
    }
}
