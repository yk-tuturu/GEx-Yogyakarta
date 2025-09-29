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

    public event Action<string> onExitEnd;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeSprite(int index) {
        if (index < 0 || index >= sprites.Length) {
            Debug.Log("Index out of bounds");
            return;
        }

        sprRen.sprite = sprites[index];
    }

    public void Flip(bool flip) {
        if (flip && !isFlipped) {
            transform.DORotate(new Vector3(0f, 180f, 0f), 0.2f);
        } else if (!flip && isFlipped) {
            transform.DORotate(new Vector3(0f, 0f, 0f), 0.2f);
        }
    }

    public void SetSprite(int index) {
        sprRen.sprite = sprites[index];
    }

    public void Move(Vector3 pos) {
        transform.DOMove(pos, 0.4f);
    }

    public void InitSprite(SpriteData data) {
        Color c = sprRen.color;
        c.a = 0f; 
        sprRen.color = c;

        if (data.flipped) {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isFlipped = true;
        }

        sprRen.sprite = sprites[data.spriteIndex];
    }

    public void EnterLeft(Vector3 pos) {
        transform.position = new Vector3(pos.x - 1, pos.y, pos.z);
        transform.DOMove(pos, 0.4f);
        sprRen.DOFade(1f, 0.2f);
    }

    public void EnterRight(Vector3 pos) {
        transform.position = new Vector3(pos.x + 1, pos.y, pos.z);
        transform.DOMove(pos, 0.4f);
        sprRen.DOFade(1f, 0.2f);
    }

    public void ExitRight() {
        Vector3 pos = transform.position;
        transform.DOMove(new Vector3(13f, pos.y, pos.z), 0.4f).OnComplete(()=> {
            onExitEnd?.Invoke(characterName);
        });
        
    }

    public void ExitLeft() {
        Vector3 pos = transform.position;
        transform.DOMove(new Vector3(-13f, pos.y, pos.z), 0.4f).OnComplete(()=> {
            onExitEnd?.Invoke(characterName);
        });
    }
}
