using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCommand : AnimationCommand
{
    public int index; 

    public ChangeCommand(StorySprite target, int index) : base(target) {
        this.index = index;
    }

    public override void Run() {
        target.ChangeSprite(index);
    }
}
