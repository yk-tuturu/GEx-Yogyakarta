using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBounce : AnimationCommand
{
    public float offsetX;
    public float offsetY;
    public float duration; 

    public MoveBounce(StorySprite target, float offsetX, float offsetY, float duration) : base(target) {
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.duration = duration;
    }

    public override void Run() {
        target.MoveBounce(offsetX, offsetY, duration);
    }
}
