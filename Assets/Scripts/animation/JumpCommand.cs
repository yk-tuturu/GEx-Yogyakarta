using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : AnimationCommand
{
    public float jumpForce;
    public float offsetX; 
    public float offsetY;
    public float duration;

    public JumpCommand(StorySprite target, float jumpForce, float offsetX, float offsetY, float duration) : base(target) {
        this.jumpForce = jumpForce;
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.duration = duration;
    }

    public override void Run() {
        target.Jump(jumpForce, offsetX, offsetY, duration);
    }
}
