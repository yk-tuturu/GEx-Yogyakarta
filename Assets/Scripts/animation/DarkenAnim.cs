using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenAnim : AnimationCommand
{
    public float val;
    public float duration; 

    public DarkenAnim(StorySprite target, float val, float duration) : base(target) {
        this.val = val;
        this.duration = duration;
    }

    public override void Run() {
        target.Darken(val, duration);
    }
}
