using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCommand : AnimationCommand
{
    public bool flip; 
    public float duration = 0.2f;

    public FlipCommand(StorySprite target, bool flip) : base(target) {
        this.flip = flip;
    }

    public FlipCommand(StorySprite target, bool flip, float duration) : base(target) {
        this.flip = flip;
        this.duration = duration;
    }

    public override void Run() {
        target.Flip(flip, duration);
    }
}
