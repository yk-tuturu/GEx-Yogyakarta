using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayer : AnimationCommand
{
    public int layer;

    public SetLayer(StorySprite target, int layer) : base(target) {
        this.layer = layer;
    }

    public override void Run() {
        target.SetLayer(layer);
    }
}
