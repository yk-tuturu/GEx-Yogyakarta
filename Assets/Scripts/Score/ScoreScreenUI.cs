using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreScreenUI : MonoBehaviour
{
    int score = 939456;
    int currScore = 0;
    int stepSize;

    int perfectCount = 45;
    int goodCount = 23;
    int missCount = 3;

    int combo = 45;
    float acc = 92.45f;

    float offset = 50f;

    int gradeSpriteIndex = 0;

    public TextMeshProUGUI scoreText;

    public List<RectTransform> judgementSprites;
    public List<TextMeshProUGUI> judgementCounts;

    public TextMeshProUGUI comboText;
    public TextMeshProUGUI accuracyText;

    public Image gradeImage;
    public List<Sprite> gradeSprites;

    public CanvasGroup whiteScreen;
    public CanvasGroup blackScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        FetchInfo();

        stepSize = score / 300;
        
        judgementCounts[0].text = perfectCount.ToString();
        judgementCounts[1].text = goodCount.ToString();
        judgementCounts[2].text = missCount.ToString();
        
        comboText.text = combo.ToString();
        accuracyText.text = acc.ToString("F2") + "%";

        gradeImage.sprite = gradeSprites[gradeSpriteIndex];

        StartCoroutine(ScoreAnimation());
        StartCoroutine(UIAnimation());
    }

    void OnDestroy() {
        DOTween.KillAll();
    }

    void FetchInfo() {
        if (!ScoreManager.Instance) {
            Debug.Log("cannot find score!");
            return;
        }
        var sm = ScoreManager.Instance;
        score = (int) sm.score;
        acc = sm.accuracy;

        combo = sm.maxCombo;
        
        perfectCount = sm.perfectCount;
        goodCount = sm.goodCount;
        missCount = sm.missCount;

        if (acc >= 100f) {
            gradeSpriteIndex = 0;
        } else if (acc >= 95f) {
            gradeSpriteIndex = 1;
        } else if (acc >= 90f) {
            gradeSpriteIndex = 2;
        } else if (acc >= 80f) {
            gradeSpriteIndex = 3;
        } else if (acc >= 70f) {
            gradeSpriteIndex = 4;
        } else {
            gradeSpriteIndex = 5;
        }
    }

    public void BackToMenu() {
        SceneManager.LoadScene("menu");
    }

    IEnumerator UIAnimation() {
        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 1f;

        blackScreen.DOFade(0f, 0.5f);

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(ScoreAnimation());

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < judgementSprites.Count; i++) {
            FadeAndZoomIn(judgementSprites[i]);
            yield return new WaitForSeconds(0.3f);

            FadeAndSlideIn(judgementCounts[i].rectTransform);

            yield return new WaitForSeconds(0.3f);
        }

        FadeAndSlideIn(comboText.rectTransform);
        yield return new WaitForSeconds(0.3f);
        FadeAndSlideIn(accuracyText.rectTransform);
        yield return new WaitForSeconds(0.3f);

        SlowZoomIn(gradeImage.rectTransform);
        yield return new WaitForSeconds(0.7f);
        
        Flash();
        yield return null;
    }

    IEnumerator ScoreAnimation() {
        while (currScore < score) {
            currScore = Mathf.Min(currScore + stepSize, score);
            scoreText.text = currScore.ToString("000000");
            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }

    public void FadeAndZoomIn(RectTransform rect) {
        CanvasGroup cg = rect.GetComponent<CanvasGroup>();
        cg.alpha = 0f;
        cg.DOFade(1f, 0.2f);

        rect.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        rect.DOScale(Vector3.one, 0.3f);
    }

    public void FadeAndSlideIn(RectTransform rect) {
        CanvasGroup cg = rect.GetComponent<CanvasGroup>();
        cg.alpha = 0f;
        cg.DOFade(1f, 0.2f);

        Vector3 ogPos = rect.anchoredPosition;
        rect.anchoredPosition = new Vector3(ogPos.x - offset, ogPos.y, ogPos.z);
        rect.DOAnchorPosX(ogPos.x, 0.3f);
    }

    public void SlowZoomIn(RectTransform rect) {
        CanvasGroup cg = rect.GetComponent<CanvasGroup>();
        cg.alpha = 0f;
        cg.DOFade(1f, 0.6f);

        rect.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        rect.DOScale(Vector3.one, 0.8f).SetEase(Ease.InCubic);
    }

    public void Flash() {
        whiteScreen.DOFade(0.7f, 0.3f).OnComplete(()=> {
            whiteScreen.DOFade(0f, 2f);
        });
    }
}
