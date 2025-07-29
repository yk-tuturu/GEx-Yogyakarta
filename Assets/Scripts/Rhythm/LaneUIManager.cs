using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;

public class LaneUIManager : MonoBehaviour
{
    public Transform laneHighlightParent;
    public List<SpriteRenderer> laneHighlights;
    public bool[] laneIsFadingIn = Enumerable.Repeat(true, 7).ToArray();
    public bool[] laneIsBeingHeld = Enumerable.Repeat(true, 7).ToArray();

    public TextMeshProUGUI comboCounter;
    private float comboCounterOgY;
    private float comboCounterMaxY;
    public float comboMoveDist;
    public Image judgement;
    public float judgementAnimScale;

    public Sprite perfectSprite;
    public Sprite goodSprite;
    public Sprite missSprite;

    public Transform laneEmitterParent;
    public List<LaneEmitter> laneEmitters;

    public TextMeshProUGUI scoreText;
    public int targetScore;
    public int currScore;
    public int scoreStepSize; 

    public TextMeshProUGUI accText;
    public float targetAcc = 100f;
    public float currAcc = 100f;
    public float accStepSize;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in laneHighlightParent) {
            SpriteRenderer highlight = child.GetComponent<SpriteRenderer>();
            laneHighlights.Add(highlight);
            
            Color color = highlight.color; 
            color.a = 0;
            highlight.color = color;
        }

        foreach (Transform child in laneEmitterParent) {
            LaneEmitter emitter = child.GetComponent<LaneEmitter>();
            laneEmitters.Add(emitter);
        }

        InputManager.Instance.OnRhythmLaneRelease += HandleLaneRelease;

        comboCounterOgY = comboCounter.rectTransform.anchoredPosition.y;
        comboCounterMaxY = comboCounter.rectTransform.anchoredPosition.y - comboMoveDist;
        comboCounter.text = "";

        judgement.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currScore < targetScore) {
            currScore = Mathf.Min(currScore + scoreStepSize, targetScore);
        }

        scoreText.text = currScore.ToString("0000000");

        if (Mathf.Abs(targetAcc - currAcc) > Mathf.Abs(accStepSize)) {
            currAcc = currAcc + accStepSize;
        } else {
            currAcc = targetAcc;
        }

        accText.text = currAcc.ToString("F2") + "%";
    }

    public void TriggerLaneHighlight(int lane) {
        SpriteRenderer target = laneHighlights[lane];
        laneIsFadingIn[lane] = true;
        laneIsBeingHeld[lane] = true;

        DOTween.Kill(target);
        target.DOFade(1f, 0.06f).OnComplete(() => {
            laneIsFadingIn[lane] = false;
            if (!laneIsBeingHeld[lane]) {
                target.DOFade(0f, 0.25f);
            }
        });

    }

    void HandleLaneRelease(int lane) {
        laneIsBeingHeld[lane] = false;

        if (!laneIsFadingIn[lane]) {
            SpriteRenderer target = laneHighlights[lane];
            target.DOFade(0f, 0.25f);
        }
    }

    public void UpdateComboCounter() {
        int combo = ScoreManager.Instance.combo;

        if (combo == 0) {
            comboCounter.text = "";
        } else {
            comboCounter.text = combo.ToString();
        }

        RectTransform rt = comboCounter.rectTransform;
        DOTween.Kill(rt);
        Sequence seq = DOTween.Sequence();
        seq.Append(rt.DOAnchorPosY(comboCounterMaxY, 0.06f));
        seq.Append(rt.DOAnchorPosY(comboCounterOgY, 0.25f));
        seq.Play();
    }

    public void UpdateJudgementDisplay() {
        int hitValue = ScoreManager.Instance.lastHitValue;

        if (!judgement.gameObject.activeSelf) {
            judgement.gameObject.SetActive(true);
        }

        if (hitValue == 300) {
            judgement.sprite = perfectSprite;
        } else if (hitValue == 100) {
            judgement.sprite = goodSprite;
        } else {
            judgement.sprite = missSprite;
        }

        DOTween.Kill(judgement.transform);

        Sequence seq = DOTween.Sequence();
        seq.Append(judgement.transform.DOScale(judgementAnimScale, 0.06f));
        seq.Append(judgement.transform.DOScale(1f, 0.25f));
        seq.Play();
    }

    public void LaneEmit(int lane) {
        laneEmitters[lane].Emit();
    }

    public void UpdateScoreDisplay() {
        targetScore = (int) Mathf.Round(ScoreManager.Instance.score);

        int lastScoreAdded = (int) Mathf.Round(ScoreManager.Instance.lastScoreAdded);

        currScore += (int)Mathf.Floor(lastScoreAdded * 0.7f);

        int diff = targetScore - currScore; 

        scoreStepSize = (int)Mathf.Floor(diff / 50f);
    }

    public void UpdateAccuracyDisplay() {
        targetAcc = ScoreManager.Instance.accuracy;
        
        float diff = targetAcc - currAcc;

        currAcc += diff * 0.7f;

        accStepSize = (targetAcc - currAcc) / 30f;
    }
}
