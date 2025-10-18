using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCommand : AnimationCommand
{
    public HideCommand(StorySprite target) : base(target) {

    }

    public override void Run() {
        target.gameObject.SetActive(false);
    }
}
