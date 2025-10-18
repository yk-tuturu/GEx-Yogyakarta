using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBGM : MonoBehaviour
{
    public AudioClip[] bgmList;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, bgmList.Length);
        source.clip = bgmList[index];
        source.Play();
    }

    public void FadeOut(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration));
    }

    IEnumerator FadeOutCoroutine(float fadeDuration)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        source.volume = 0;
        source.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
