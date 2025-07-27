using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuUIController : MonoBehaviour
{
    public CanvasGroup loadingPanel; 

    public CanvasGroup blackScreen;
    // Start is called before the first frame update
    void Start()
    {
        RhythmGameLoader.Instance.fadeToBlack += FadeToBlack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadRhythmLevel(string mapName)
    {
        loadingPanel.gameObject.SetActive(true);
        loadingPanel.alpha = 0f;
        loadingPanel.DOFade(1f, 0.4f);

        RhythmGameLoader.Instance.LoadRhythmScene(mapName);
    }

    public void FadeToBlack() {
        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 0f;
        blackScreen.DOFade(1f, 0.4f);
    }
}
