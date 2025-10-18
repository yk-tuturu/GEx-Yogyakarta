using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosCommand : AnimationCommand
{
    public float x;
    public float y;
    public float z;
    
    public SetPosCommand(StorySprite target, float x, float y, float z) : base(target) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override void Run() {
        target.transform.position = new Vector3(x, y, z);
    }
}
