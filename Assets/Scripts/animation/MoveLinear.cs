using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLinear : AnimationCommand
{
    public float offsetX;
    public float offsetY;
    public float duration; 

    public MoveLinear(StorySprite target, float offsetX, float offsetY, float duration) : base(target) {
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.duration = duration;
    }

    public override void Run() {
        target.MoveLinear(offsetX, offsetY, duration);
    }
}
