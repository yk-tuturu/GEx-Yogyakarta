using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
using System;

public class StorySprite : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer sprRen;

    public string characterName;

    public bool isFlipped = false;

    //public event Action<string> onExitEnd;

    void Start()
    {

    }

    public void Init(float x, float y, int index, bool flip) {
        transform.position = new Vector3(x, y, 0f);
        sprRen.sprite = sprites[index];

        if (flip) {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        isFlipped = flip;
    }

    public void ChangeSprite(int index) {
        if (index < 0 || index >= sprites.Length) {
            Debug.Log("Index out of bounds");
            return;
        }

        sprRen.sprite = sprites[index];
    }

    public void Flip(bool flip, float duration) {
        if (flip && !isFlipped) {
            transform.DORotate(new Vector3(0f, 180f, 0f), duration);
        } else if (!flip && isFlipped) {
            transform.DORotate(new Vector3(0f, 0f, 0f), duration);
        }

        isFlipped = flip;
    }

    public void Animate(int[] frames, float frameDelay, int loopCount) {
        StartCoroutine(AnimateCoroutine(frames, frameDelay, loopCount));
    }

    IEnumerator AnimateCoroutine(int[] frames, float frameDelay, int loopCount) {
        int counter = 0;
        int max = frames.Length * loopCount;

        while (counter < max) {
            ChangeSprite(frames[counter % frames.Length]);
            yield return new WaitForSeconds(frameDelay);

            counter++;
        }
    }

    public void Enter(float targetX, float targetY, float offsetX, bool direction, float duration) {
        if (!direction) { // if to left
            offsetX = -(Math.Abs(offsetX));
        }

        Vector3 target = new Vector3(targetX, targetY, transform.position.z);
        transform.position = new Vector3(targetX + offsetX, targetY, transform.position.z);

        Color c = sprRen.color;
        c.a = 0f; 
        sprRen.color = c;

        transform.DOMove(target, duration);
        sprRen.DOFade(1f, duration / 2f);
    }

    public void Jump(float jumpForce, float offsetX, float offsetY, float duration) {
        Vector3 target = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);

        transform.DOJump(target, jumpForce, 1, duration).SetEase(Ease.Linear);
    }

    

    public void Move(float offsetX, float offsetY, float duration) {
        Vector3 target = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);
        transform.DOMove(target, duration);
    }

    public void MoveLoop(float offsetX, float offsetY, float duration, int loopCount, float loopDelay) {
        Vector3 target = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);
        Vector3 ogPos = transform.position;
        //transform.DOMove(target, duration).SetLoops(loopCount, LoopType.Yoyo);

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMove(target, duration))
            .Append(transform.DOMove(ogPos, duration))
           .AppendInterval(loopDelay) 
           .SetLoops(loopCount, LoopType.Restart); 
        
        seq.Play();
    }

    public void Exit(bool direction, float duration) {
        Vector3 exitPoint = transform.position;

        if (direction) {
            exitPoint = new Vector3(13f, exitPoint.y, exitPoint.z);
        } else {
            exitPoint = new Vector3(-13f, exitPoint.y, exitPoint.z);
        }

        transform.DOMove(exitPoint, duration);
    }

    // public void ExitRight() {
    //     Vector3 pos = transform.position;
    //     transform.DOMove(new Vector3(13f, pos.y, pos.z), 0.4f).OnComplete(()=> {
    //         onExitEnd?.Invoke(characterName);
    //     });
        
    // }

    // public void ExitLeft() {
    //     Vector3 pos = transform.position;
    //     transform.DOMove(new Vector3(-13f, pos.y, pos.z), 0.4f).OnComplete(()=> {
    //         onExitEnd?.Invoke(characterName);
    //     });
    // }
}
