using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource source;

    public List<AudioClip> soundList;

    public Dictionary<string, AudioClip> soundDict = new Dictionary<string, AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) {
            Instance = this;
        }
            
        else {
            Destroy(gameObject);
        }
            
        
        foreach (var clip in soundList) {
            soundDict[clip.name] = clip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOneShot(string clipName) {
        if (!soundDict.ContainsKey(clipName)) {
            Debug.Log("audio clip not found!");
            return;
        }

        source.PlayOneShot(soundDict[clipName]);
    }
}
