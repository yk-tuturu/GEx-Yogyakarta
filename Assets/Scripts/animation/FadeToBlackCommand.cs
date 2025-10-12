using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeToBlackCommand : AnimationCommand
{
    public CanvasGroup blackScreen;
    public float duration;

    public FadeToBlackCommand(CanvasGroup blackScreen, float duration) : base(null) {
        this.blackScreen = blackScreen;
        this.duration = duration;
    }

    public override void Run() {
        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 0f;

        blackScreen.DOFade(1f, duration);
    }
}
