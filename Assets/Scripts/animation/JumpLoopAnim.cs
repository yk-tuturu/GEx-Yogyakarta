using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLoopAnim : AnimationCommand
{
    public float jumpForce;
    public float offsetX; 
    public float offsetY;
    public float duration;
    public int jumpCount;
    public float loopDelay;

    public JumpLoopAnim(StorySprite target, float jumpForce, float offsetX, float offsetY, float duration, int jumpCount, float loopDelay) : base(target) {
        this.jumpForce = jumpForce;
        this.offsetX = offsetX;
        this.offsetY = offsetY;
        this.duration = duration;
        this.jumpCount = jumpCount;
        this.loopDelay = loopDelay;
    }

    public override void Run() {
        target.JumpLoop(jumpForce, offsetX, offsetY, duration, jumpCount, loopDelay);
    }
}
