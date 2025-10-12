using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationScene {
    public AnimationCommand[] commands;
    public float duration;

    public AnimationScene(AnimationCommand[] commands, float duration) {
        this.commands = commands;
        this.duration = duration;
    }

    public void Play() {
        foreach(AnimationCommand anim in commands) {
            anim.Run();
        }
    }
}
