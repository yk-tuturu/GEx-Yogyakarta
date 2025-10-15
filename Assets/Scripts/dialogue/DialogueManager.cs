using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using System.IO;  
using System.Text;

public class DialogueManager : MonoBehaviour
{
    public Queue<Dialogue> sentences = new Queue<Dialogue>();
    public bool currentlyInDialogue = false;
    public bool currentlyTyping = false;

    public float textDelay = 0.02f;

    public Dialogue currentLine;

    //public TextMeshProUGUI speakerText;
    public TextMeshProUGUI speechText;
    public GameObject dialoguePanel;

    public SpriteManager spriteManager;
    //public CanvasGroup loadingPanel;

    public event Action OnDialogueEnd;

    public string filename;

    public GameObject[] instruments;

    void Awake()
    {
        sentences = new Queue<Dialogue>();
    }

    void Start() {
        // get filename from mapdata
        filename = MapDataManager.Instance.storyFilename;
        if (filename == "saron") {
            instruments[0].SetActive(true);
        } else if (filename == "bonang") {
            instruments[1].SetActive(true);
        } else if (filename == "kendang") {
            instruments[2].SetActive(true);
        }
    }

    void Update()
    {
        if (Input.anyKeyDown && currentlyInDialogue)
        {
            if (currentlyTyping)
            {
                StopAllCoroutines();
                speechText.text = currentLine.sentence;
                currentlyTyping = false;
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }

    public void ReadFile(string filename)
    {
        TextAsset file = (TextAsset)Resources.Load("Story/" + filename);

        using (StringReader sr = new StringReader(file.text))
        {
            string line;
            Dialogue temp = new Dialogue();

            while ((line = sr.ReadLine()) != null)
            {
                temp.speaker = "";
                temp.sentence = line;
                sentences.Enqueue(temp);
                temp = new Dialogue();
            }
        }
    }

    public void StartDialogue()
    {
        sentences.Clear();
        dialoguePanel.SetActive(true);
        currentlyInDialogue = true;

        ReadFile(filename);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = sentences.Dequeue();
        //speakerText.text = currentLine.speaker;
        //spriteManager.Process(currentLine.moveData, currentLine.spriteData);

        currentlyInDialogue = true;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        // play text audio here
        speechText.text = "";
        currentlyTyping = true;
        foreach(char letter in sentence.ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(textDelay);
        }
        currentlyTyping = false;
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");
        currentlyInDialogue = false;

        //speakerText.text = "";
        speechText.text = "";

        OnDialogueEnd?.Invoke();
    }
}
