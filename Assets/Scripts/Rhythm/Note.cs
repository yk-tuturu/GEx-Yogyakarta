using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Note : MonoBehaviour
{
    public int id; 
    public float targetTime; 
    public int lane_id;
    public string hitsound;
    public int instrumentIndex;

    public float perfectWindow = 0.05f; //50ms perfect 
    public float goodWindow = 0.12f; // 120ms good
    public float missWindow = 0.2f;
    public float offset = 0f;

    public float judgementY; 
    public float despawnY;

    public bool canHit = false;

    public bool isPaused = false;

    public delegate void OnDespawn(Note note);
    public event OnDespawn OnDespawnEvent;

    // buncha debug stuff
    public delegate void AutoHit(Note note);
    public event AutoHit OnAutoHit;

    private Tween moveTween;

    // Start is called before the first frame update
    void Start()
    {
        float songPos = MusicManager.Instance.GetSongPos();
        float currY = transform.position.y;

        float speed = (currY - judgementY) / (targetTime - songPos);

        float moveTime = (currY - despawnY) / speed;

        moveTween = transform.DOMoveY(despawnY, moveTime).SetEase(Ease.Linear);

        PauseManager.Instance.onPause += OnPause;
        PauseManager.Instance.onUnpause += OnUnpause;
    }

    // Update is called once per frame
    void Update()
    {
        float songPos = MusicManager.Instance.GetSongPos();

        if (!canHit && songPos > targetTime - missWindow) {
            canHit = true; 
        }

        if (songPos >= targetTime) {
            OnAutoHit?.Invoke(this);
        }

        // if the hit window is exceeded, fire the event to despawn this note
        if (songPos > targetTime + goodWindow) {
            OnDespawnEvent?.Invoke(this);
        }
    }

    public int GetScore() {
        if (!canHit) {
            Debug.Log("Get score called when note cannot be hit. This should not happen");
            return 0;
        }

        float songPos = MusicManager.Instance.GetSongPos();
        float hitError = Mathf.Abs(targetTime - songPos);
        

        if (hitError <= perfectWindow) {
            AudioManager.Instance.PlayOneShot(hitsound);
            return 300;
        } else if (hitError <= goodWindow) {
            AudioManager.Instance.PlayOneShot(hitsound);
            return 100;
        } else {
            AudioManager.Instance.PlayOneShot("combobreak");
            return 0;
        }
    }

    void OnDestroy() {
        DOTween.Kill(transform);
    }

    void OnPause() {
        isPaused = true;
        moveTween.Pause();
    }

    void OnUnpause() {
        isPaused = false;
        moveTween.Play();
    }
}
