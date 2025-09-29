using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;  
using System.IO;  
using System.Text;

public class Dialogue_trigger : MonoBehaviour
{
    public List<Dialogue> story = new List<Dialogue>();
    public DialogueManager dialogueManager;

    void Start()
    {
        if (StoryLoader.Instance) {
            TriggerDialogue(StoryLoader.Instance.storyFile);
        } else {
            Debug.Log("story loader not found");
            TriggerDialogue("teststory");
        }
    }

    public void TriggerDialogue(string filename)
    {
        ReadFile(filename);

        // fix this later
        UnityEvent onComplete = new UnityEvent();

        dialogueManager.StartDialogue(story);
        Debug.Log("dialogueTriggered " + filename);
        story = new List<Dialogue>();
    }

    public void ReadFile(string filename)
    {
        TextAsset file = (TextAsset)Resources.Load("Story/" + filename);

        using (StringReader sr = new StringReader(file.text))
        {
            string line;
            int counter = 1;
            Dialogue temp = new Dialogue();

            while ((line = sr.ReadLine()) != null)
            {
                if (counter == 1)
                {
                    temp.speaker = line;
                } 
                else if (counter == 2) {
                    if (line == "") {
                        counter++;
                        temp.moveData = new MoveCommand[0];
                        continue;
                    }

                    string[] moveStrings = line.Split('|');
                    temp.moveData = new MoveCommand[moveStrings.Length];

                    for (int i = 0; i < moveStrings.Length; i++) {
                        MoveCommand move = new MoveCommand();
                        string[] moveArray = moveStrings[i].Split(' ');

                        move.command = moveArray[0];
                        move.spriteTarget = moveArray[1];
                        string[] args = new string[moveArray.Length - 2];
                        Array.Copy(moveArray, 2, args, 0, moveArray.Length - 2);
                        move.args = args;

                        temp.moveData[i] = move;
                    }
                }
                else if (counter == 3) {
                    if (line == "") {
                        counter++;
                        temp.spriteData = new SpriteData[0];
                        continue;
                    }

                    string[] spriteStrings = line.Split('|');
                    temp.spriteData = new SpriteData[spriteStrings.Length];

                    for (int i = 0; i < spriteStrings.Length; i++) {
                        string[] parts = spriteStrings[i].Split(':');
                        SpriteData sprite = new SpriteData();

                        sprite.spriteTarget = parts[0];
                        sprite.spriteIndex = int.Parse(parts[1]);
                        sprite.flipped = parts[2]=="true";

                        temp.spriteData[i] = sprite;

                    }
                }
                else if (counter == 4)
                {
                    temp.sentence = line;
                    story.Add(temp);
                    temp = new Dialogue();
                    counter = 0;
                    
                }
                counter++;
            }
        }

        // string path = "Assets/Resources/Story/" + filename + ".txt";
        // using (FileStream fs = File.OpenRead(path))  
        // {  
        //     string[] readText = File.ReadAllLines(path);
        //     int counter = 1;
        //     Dialogue temp = new Dialogue();
        //     foreach (string s in readText)
        //     {
        //         if (counter == 1)
        //         {
        //             temp.speaker = s;
        //         }
        //         else if (counter == 2)
        //         {
        //             temp.sentence = s;
                    
        //         }
        //         else if (counter == 3)
        //         {
        //             temp.commands = s.Split(", ");
        //             story.Add(temp);
        //             temp = new Dialogue();
        //             counter = 0;
        //         }
        //         counter++;
        //     }
        // }  
    }
}
