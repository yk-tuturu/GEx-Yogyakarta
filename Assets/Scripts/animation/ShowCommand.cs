using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCommand : AnimationCommand
{
    public ShowCommand(StorySprite target) : base(target) {

    }

    public override void Run() {
        target.gameObject.SetActive(true);
    }
}
