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

    public float perfectWindow = 0.05f; //50ms perfect 
    public float goodWindow = 0.12f; // 120ms good
    public float missWindow = 0.2f;
    public float offset = 0f;

    public float judgementY; 
    public float despawnY;

    public bool canHit = false;

    public delegate void OnDespawn(Note note);
    public event OnDespawn OnDespawnEvent;

    // Start is called before the first frame update
    void Start()
    {
        float songPos = MusicManager.Instance.GetSongPos();
        float currY = transform.position.y;

        float speed = (currY - judgementY) / (targetTime - songPos);

        float moveTime = (currY - despawnY) / speed;

        transform.DOMoveY(despawnY, moveTime).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        float songPos = MusicManager.Instance.GetSongPos();

        if (!canHit && songPos > targetTime - missWindow) {
            canHit = true; 
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
        Debug.Log(hitError);
        

        if (hitError <= perfectWindow) {
            Debug.Log("perfect");
            AudioManager.Instance.PlayOneShot(hitsound);
            return 300;
        } else if (hitError <= goodWindow) {
            Debug.Log("good");
            AudioManager.Instance.PlayOneShot(hitsound);
            return 100;
        } else {
            Debug.Log("miss");
            AudioManager.Instance.PlayOneShot("combobreak");
            return 0;
        }
    }

    void OnDestroy() {
        DOTween.Kill(transform);
    }
}
