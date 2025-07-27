using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; 

    public float score = 0f;
    public float maxScore = 1000000f;
    public float accuracy = 100f;

    public int combo = 0;
    public int maxCombo = 0;
    
    public bool fullCombo = true;
    public bool allPerfect = true;

    public int perfectCount = 0;
    public int goodCount = 0;
    public int missCount = 0;

    public int lastHitValue = 0;
    public float lastScoreAdded = 0;

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
        if (scene.name != "rhythm" && scene.name != "score") {
            Instance = null;
            Destroy(this.gameObject);
        }
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void UpdateComboAndScore(int hitValue, bool isLastNote) {
        // combo
        if (hitValue == 0) {
            combo = 0;
            fullCombo = false;
            allPerfect = false;
            missCount++;
        } else if (hitValue == 100) {
            allPerfect = false;
            combo++;
            goodCount++;
        } else {
            combo++;
            perfectCount++;
        }
        maxCombo = Mathf.Max(combo, maxCombo);

        // score
        int totalNote = MapDataManager.Instance.totalHitObjectCount;
        float scoreToAdd = (maxScore / totalNote) * (hitValue / 300f);
        score += scoreToAdd;

        if (isLastNote && allPerfect) {
            score = maxScore;
        }

        lastHitValue = hitValue;
        lastScoreAdded = scoreToAdd;

        // acc
        accuracy = ((300f * perfectCount) + (100f * goodCount)) / (300f * (perfectCount + goodCount + missCount)) * 100f;
    }
}
