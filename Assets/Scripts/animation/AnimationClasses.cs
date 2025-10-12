using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationCommand {
    public StorySprite target;

    public AnimationCommand(StorySprite target) {
        this.target = target;
    }

    public abstract void Run();
}

