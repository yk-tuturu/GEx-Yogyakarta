using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class RhythmUIManager : MonoBehaviour
{
    public RectTransform titlePanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public CanvasGroup cg; 
    public CanvasGroup blackScreen;

    public RectTransform scorePanel;

    public float offset = 800f;
    
    // Start is called before the first frame update
    void Start()
    {
        titleText.text = MapDataManager.Instance.title;
        subtitleText.text = MapDataManager.Instance.subtitle;
        
        AnimateTitleCard();
        RhythmGameLoader.Instance.scoreScreenTransition += ScoreScreenTransition;
    }

    void OnDestroy() {
        RhythmGameLoader.Instance.scoreScreenTransition -= ScoreScreenTransition;
    }

    public void AnimateTitleCard() {
        Vector2 originalPos = titlePanel.anchoredPosition;
        titlePanel.anchoredPosition = new Vector2(originalPos.x, originalPos.y + offset);
        cg.alpha = 0f;

        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 1f;

        Sequence seq = DOTween.Sequence();
        seq.Append(blackScreen.DOFade(0f, 0.4f));
        seq.Append(titlePanel.DOAnchorPosY(originalPos.y, 1.2f)
             .SetEase(Ease.OutElastic, 1.1f, 0.7f));
        seq.Join(cg.DOFade(1f, 0.5f));
        seq.AppendInterval(1f);
        seq.Append(titlePanel.DOAnchorPosY(originalPos.y + offset, 1f).SetEase(Ease.InCubic));
        seq.Join(cg.DOFade(0f, 0.7f));

        seq.Play();
    }

    public void ScoreScreenTransition() {
        RhythmGameLoader.Instance.scoreScreenTransition -= ScoreScreenTransition;
        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 0f;
        blackScreen.DOFade(1f, 0.5f);
    }
}
