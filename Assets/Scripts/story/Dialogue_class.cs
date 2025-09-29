using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string speaker;
    public MoveCommand[] moveData;
    public SpriteData[] spriteData;
    public string sentence;
}

public class MoveCommand {
    public string command;
    public string spriteTarget;
    public string[] args;
}

public class SpriteData {
    public string spriteTarget;
    public int spriteIndex;
    public bool flipped;
}
