using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public bool isPaused = false;

    public event Action onPause;
    public event Action onUnpause;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnEscPressed += OnEscPressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        InputManager.Instance.OnEscPressed -= OnEscPressed;
    }

    void OnEscPressed() {
        if (!MusicManager.Instance.songStarted) {
            return;
        }
        if (isPaused) {
            isPaused = false;
            onUnpause?.Invoke();
        } else {
            isPaused = true;
            onPause?.Invoke();
        }
    }

    public void Pause() {
        isPaused = true;
        onPause?.Invoke();
    }

    public void Unpause() {
        isPaused = false;
        onUnpause?.Invoke();
    }
}
