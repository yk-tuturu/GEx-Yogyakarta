using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource source;

    public List<AudioClip> commonSoundList;
    
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
            
        foreach (var clip in commonSoundList) {
            soundDict[clip.name] = clip;
        }

        if (SceneManager.GetActiveScene().name == "rhythm") {
            foreach (var clip in RhythmGameLoader.Instance.hitsounds) {
                soundDict[clip.name] = clip;
            }
        }

        source.volume = PlayerPrefManager.Instance.GetVolume() / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOneShot(string clipName, bool pitchVariation = false, float volume = 1f) {
        if (!soundDict.ContainsKey(clipName)) {
            Debug.Log("audio clip not found!");
            return;
        }

        if (pitchVariation) {
            source.pitch = Random.Range(0.9f, 1.1f);
        } else {
            source.pitch = 1f;
        }

        source.volume = volume;

        source.PlayOneShot(soundDict[clipName]);
    }

    public void SetVolume(int value) {
        source.volume = value / 100f;
    }
}
