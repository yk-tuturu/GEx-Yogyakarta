using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCommand : AnimationCommand
{
    public float targetX;
    public float targetY; 

    public float offsetX = 2;
    public bool direction; // true = right, false = left
    public float duration = 0.5f;

    public EnterCommand(StorySprite sprite, float targetX, float targetY, bool direction, float duration) : base(sprite) {
        this.targetX = targetX;
        this.targetY = targetY;
        this.direction = direction;
        this.duration = duration;
    }

    public override void Run() {
        if (!target.gameObject.activeInHierarchy) {
            target.gameObject.SetActive(true);
        }
        target.Enter(targetX, targetY, offsetX, direction, duration);
    }
}
