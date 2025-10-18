using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutMusic : AnimationCommand
{
    public RandomBGM bgm;
    public float duration;

    public FadeOutMusic(RandomBGM bgm, float duration) : base(null) {
        this.bgm = bgm;
        this.duration = duration;
    }

    public override void Run() {
       bgm.FadeOut(duration);
    }
}
