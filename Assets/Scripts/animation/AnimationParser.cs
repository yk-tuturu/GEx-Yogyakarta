using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using System.IO;  
using System.Text;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AnimationParser : MonoBehaviour
{
    public string filename;

    public List<AnimationScene> scenes = new List<AnimationScene>(); 

    public SpriteManager spriteManager; 

    public CanvasGroup blackScreen;
    public RectTransform titlePanel;
    public TextMeshProUGUI titleText;
    public CanvasGroup cg; 
    
    // Start is called before the first frame update
    void Start()
    {
        filename = StoryLoader.Instance.storyFile;
        ReadFile();
        PlayAnim();
    }

    public void ReadFile() {
        TextAsset file = (TextAsset)Resources.Load("Story/" + filename);

        using (StringReader sr = new StringReader(file.text))
        {
            string line;
            int counter = 0;

            InitSprites(sr.ReadLine()); 

            AnimationCommand[] tempAnim = new AnimationCommand[0];
            
            while ((line = sr.ReadLine()) != null)
            {
                if (counter % 2 == 0) {
                    tempAnim = ParseAnim(line);
                } else if (counter % 2 == 1) {
                    float duration = Util.ParseFloat(line);
                    scenes.Add(new AnimationScene(tempAnim, duration));
                }

                counter++;
            }
        }
    }

    public void InitSprites(string data) {
        string[] sprites = data.Split(",");

        foreach (string sprite in sprites) {
            string[] parts = sprite.Split(":");
            float x = Util.ParseFloat(parts[1]);
            float y = Util.ParseFloat(parts[2]);
            int index = Util.ParseInt(parts[3]);
            bool flip = Util.ParseBoolean(parts[4]);
            bool active = Util.ParseBoolean(parts[5]); 

            spriteManager.AddSprite(parts[0], x, y, index, flip, active);
        }
    }

    public AnimationCommand[] ParseAnim(string data) {
        string[] anims = data.Split('|');
        AnimationCommand[] result = new AnimationCommand[anims.Length];

        for (int i = 0; i < anims.Length; i++) {
            string[] parts = anims[i].Split(",");
            string command = parts[0];
            if (command == "fadeToBlack") {
                float duration = Util.ParseFloat(parts[1]);
                result[i] = new FadeToBlackCommand(blackScreen, duration);
                continue;
            }


            StorySprite target = spriteManager.GetSprite(parts[1]);
            
            if (command == "move") {
                float offsetX = Util.ParseFloat(parts[2]);
                float offsetY = Util.ParseFloat(parts[3]);
                float duration = Util.ParseFloat(parts[4]);

                if (parts.Length == 6) {
                    int loopCount = Util.ParseInt(parts[5]);
                    result[i] = new MoveAnimCommand(target, offsetX, offsetY, duration, loopCount);
                } else if (parts.Length == 7) {
                    int loopCount = Util.ParseInt(parts[5]);
                    float loopDelay = Util.ParseFloat(parts[6]);
                    result[i] = new MoveAnimCommand(target, offsetX, offsetY, duration, loopCount, loopDelay);
                } else {
                    result[i] = new MoveAnimCommand(target, offsetX, offsetY, duration);
                }   
            } 
            else if (command == "change") {
                int index = Util.ParseInt(parts[2]);
                result[i] = new ChangeCommand(target, index);
            } 
            else if (command == "enter") {
                float targetX = Util.ParseFloat(parts[2]);
                float targetY = Util.ParseFloat(parts[3]);
                bool direction = Util.ParseBoolean(parts[4]);
                float duration = Util.ParseFloat(parts[5]);
                result[i] = new EnterCommand(target, targetX, targetY, direction, duration);
            } 
            else if (command == "jump") {
                float jumpForce = Util.ParseFloat(parts[2]);
                float offsetX = Util.ParseFloat(parts[3]);
                float offsetY = Util.ParseFloat(parts[4]);
                float duration = Util.ParseFloat(parts[5]);
                result[i] = new JumpCommand(target, jumpForce, offsetX, offsetY, duration);
            } else if (command == "animate") {
                int[] frames = parts[2]
                    .Split(':')  
                    .Select(int.Parse)
                    .ToArray();
                
                float frameDelay = Util.ParseFloat(parts[3]);
                
                if (parts.Length == 5) {
                    int loopCount = Util.ParseInt(parts[4]);
                    result[i] = new AnimateCommand(target, frames, frameDelay, loopCount);
                } else {
                    result[i] = new AnimateCommand(target, frames, frameDelay);
                }
            } else if (command == "flip") {
                bool flip = Util.ParseBoolean(parts[2]);
                if (parts.Length == 4) {
                    float duration = Util.ParseFloat(parts[3]);
                    result[i] = new FlipCommand(target, flip, duration);
                } else {
                    result[i] = new FlipCommand(target, flip);
                }
            } else if (command == "exit") {
                bool direction = Util.ParseBoolean(parts[2]);
                float duration = Util.ParseFloat(parts[3]);

                result[i] = new ExitCommand(target, direction, duration);
            } 
        }

        return result;
    }

    public void PlayAnim() {
        StartCoroutine(Play());
    }

    public void AnimateTitleCard() {
        float offset = 800f;
        Vector2 originalPos = titlePanel.anchoredPosition;
        titlePanel.anchoredPosition = new Vector2(originalPos.x, originalPos.y + offset);
        cg.alpha = 0f;

        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 1f;

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.7f);
        seq.Append(titlePanel.DOAnchorPosY(originalPos.y, 1.2f)
             .SetEase(Ease.OutElastic, 1.1f, 0.7f));
        seq.Join(cg.DOFade(1f, 0.5f));
        seq.AppendInterval(1.3f);
        seq.Append(titlePanel.DOAnchorPosY(originalPos.y + offset, 1f).SetEase(Ease.InCubic));
        seq.Join(cg.DOFade(0f, 0.7f));
        seq.Append(blackScreen.DOFade(0f, 0.4f));

        seq.Play();
    }

    IEnumerator Play() {
        AnimateTitleCard();
        yield return new WaitForSeconds(4.4f);

        foreach(AnimationScene scene in scenes) {
            foreach(AnimationCommand command in scene.commands) {
                command.Run();
            }

            yield return new WaitForSeconds(scene.duration);
        }

        SceneManager.LoadScene("menu");
    }
}
