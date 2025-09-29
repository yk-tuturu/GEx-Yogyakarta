using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public List<GameObject> allSprites; 

    private Dictionary<string, GameObject> spriteDict = new Dictionary<string, GameObject>();

    private Dictionary<string, StorySprite> activeSprite = new Dictionary<string, StorySprite>();

    public Transform[] points;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (GameObject sprite in allSprites) {
            spriteDict.Add(sprite.gameObject.name, sprite);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Process(MoveCommand[] moveData, SpriteData[] spriteData) {
        Dictionary<string, SpriteData> spriteInfoDict = new Dictionary<string, SpriteData>();
        foreach (SpriteData spr in spriteData) {
            spriteInfoDict.Add(spr.spriteTarget, spr);
        }

        foreach(MoveCommand move in moveData) {
            string command = move.command;
            
            
            if (command == "enterleft") {
                string name = move.spriteTarget;

                Debug.Log(command);
                GameObject newSprite = Instantiate(spriteDict[name]);
                
                StorySprite spriteScript = newSprite.GetComponent<StorySprite>();
                spriteScript.InitSprite(spriteInfoDict[name]);
                
                
                int pointIndex = int.Parse(move.args[0]);
                spriteScript.EnterLeft(points[pointIndex].position);

                activeSprite.Add(name, spriteScript);
            } else if (command == "enterright") {
                string name = move.spriteTarget;
                GameObject newSprite = Instantiate(spriteDict[name]);
                StorySprite spriteScript = newSprite.GetComponent<StorySprite>();
                spriteScript.InitSprite(spriteInfoDict[name]);
                
                int pointIndex = int.Parse(move.args[0]);
                spriteScript.EnterRight(points[pointIndex].position);

                activeSprite.Add(name, spriteScript);
            } else if (command == "move") {
                string name = move.spriteTarget; 

                int pointIndex = int.Parse(move.args[0]);
                activeSprite[name].Move(points[pointIndex].position);
            } else if (command == "exitright") {
                string name = move.spriteTarget; 

                StorySprite sprite = activeSprite[name];
                sprite.onExitEnd += onExited;
                activeSprite[name].ExitRight();
            }
        }

        foreach(SpriteData data in spriteData) {
            string name = data.spriteTarget;

            if (activeSprite.ContainsKey(name)) {
                StorySprite sprite = activeSprite[name];

                sprite.Flip(data.flipped);
                sprite.SetSprite(data.spriteIndex);
            }
            
        }
    }

    public void onExited(string name) {
        StorySprite sprite = activeSprite[name];
        sprite.onExitEnd -= onExited;

        activeSprite.Remove(name);
        Destroy(sprite.gameObject);
    }

    public string GetCommand(string str) {
        return str.Split(" ")[0];
    }

    public string GetSpriteTarget(string str) {
        return str.Split(" ")[1];
    }
}
