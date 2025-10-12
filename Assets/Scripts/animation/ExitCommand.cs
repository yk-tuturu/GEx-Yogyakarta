using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCommand : AnimationCommand
{
    public bool direction;
    public float duration; 

    public ExitCommand(StorySprite target, bool direction, float duration) : base(target) {
        this.direction = direction;
        this.duration = duration;
    }

    public override void Run() {
        target.Exit(direction, duration);
    }
}
