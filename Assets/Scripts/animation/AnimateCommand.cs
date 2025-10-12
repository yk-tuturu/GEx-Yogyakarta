using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCommand : AnimationCommand
{
    public int[] frames;
    public float frameDelay;

    public int loopCount = 1;

    public AnimateCommand(StorySprite target, int[] frames, float frameDelay) : base(target) {
        this.frames = frames;
        this.frameDelay = frameDelay;
    }

    public AnimateCommand(StorySprite target, int[] frames, float frameDelay, int loopCount) : base(target) {
        this.frames = frames;
        this.frameDelay = frameDelay;

        this.loopCount = loopCount;
    }

    public override void Run() {
        target.Animate(frames, frameDelay, loopCount);
    }
}
