using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBg : MonoBehaviour
{
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, 3)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
