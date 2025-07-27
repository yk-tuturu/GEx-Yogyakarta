using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;
    // Start is called before the first frame update
    public float songLength;

    public float beforeSamples = -1f;

    public int loopCount = 0;

    public float songStartDelay = 3f;

    public bool songStarted = false;
    public bool songEnded = false;

    public event Action onSongEnded;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = RhythmGameLoader.Instance.bgm;

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        songLength = musicSource.clip.length;
        songPosition = -songStartDelay;

        StartCoroutine(DelayStartSong());
    }

    // Update is called once per frame
    void Update()
    {
        if (songEnded) return; 

        if (!songStarted) {
            songPosition += Time.deltaTime; 
        } else {
            //determine how many seconds since the song started
            songPosition = (float)musicSource.timeSamples / musicSource.clip.frequency;
            float sample = musicSource.timeSamples;
            if (beforeSamples > sample) {
                songEnded = true;
                onSongEnded?.Invoke();
            } else if (beforeSamples < sample){
                beforeSamples = sample - 1f;
            }
        }

        
        //determine how many beats since the song started
        //songPositionInBeats = songPosition / secPerBeat;
    }

    IEnumerator DelayStartSong() {
        yield return new WaitForSeconds(songStartDelay);

        //Start the music
        musicSource.Play();
        songStarted = true;
    }

    public float GetSongPos() {
        return songPosition;
    }

    public float GetSongPosInBeats() {
        return (float)GetSongPos() / secPerBeat;
    }
}
