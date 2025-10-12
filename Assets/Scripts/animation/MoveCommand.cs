using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimCommand : AnimationCommand
{
    public float offsetX;
    public float offsetY;
    public float duration; 

    public bool isLooped = false;
    public int loopCount = 0;
    public float loopDelay = 0;

    public MoveAnimCommand(StorySprite target, float offsetX, float offsetY, float duration) : base(target) {
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.duration = duration;
    }

    public MoveAnimCommand(StorySprite target, float offsetX, float offsetY, float duration, int loopCount) : base(target) {
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.duration = duration;

        this.isLooped = true;
        this.loopCount = loopCount;
    }

    public MoveAnimCommand(StorySprite target, float offsetX, float offsetY, float duration, int loopCount, float loopDelay) : base(target) {
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.duration = duration;

        this.isLooped = true;
        this.loopCount = loopCount;
        this.loopDelay = loopDelay;
    }

    public override void Run() {
        if (isLooped) {
            target.MoveLoop(offsetX, offsetY, duration, loopCount, loopDelay);
        } else {
            target.Move(offsetX, offsetY, duration);
        }
        
    }
}
