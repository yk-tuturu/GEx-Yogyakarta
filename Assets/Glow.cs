using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Glow : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer spr;
    void Start()
    {
        Debug.Log("glow spawned");
        spr = GetComponent<SpriteRenderer>();

        Sequence seq = DOTween.Sequence();
        seq.Append(spr.DOFade(1f, 0.1f));
        seq.Append(spr.DOFade(0f, 0.5f));
        seq.OnComplete(() => {
            Destroy(this.gameObject);
        });

        seq.Play();
    }

    void OnDestroy() {
        DOTween.Kill(transform);
        DOTween.Kill(spr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
