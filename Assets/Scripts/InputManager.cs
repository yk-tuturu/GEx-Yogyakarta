using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public delegate void RhythmLaneInput(int id);
    public event RhythmLaneInput OnRhythmLaneInput;
    public event RhythmLaneInput OnRhythmLaneRelease;

    public delegate void DirectionInput(Vector2 dir);
    public delegate void KeyInput();
    public event DirectionInput OnDirectionInput;
    public event KeyInput OnEscPressed;
    public event KeyInput OnEnterPressed; 

    Dictionary<KeyCode, int> keyToLane = new Dictionary<KeyCode, int>
    {
        { KeybindSettings.LANE_0, 0 },
        { KeybindSettings.LANE_1, 1 },
        { KeybindSettings.LANE_2, 2 },
        { KeybindSettings.LANE_3, 3 },
        { KeybindSettings.LANE_4, 4 },
        { KeybindSettings.LANE_5, 5 },
        { KeybindSettings.LANE_6, 6 }
    };

    void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var kvp in keyToLane)
        {
            if (Input.GetKeyDown(kvp.Key))
            {
                OnRhythmLaneInput?.Invoke(kvp.Value);
            }

            if (Input.GetKeyUp(kvp.Key)) {
                OnRhythmLaneRelease?.Invoke(kvp.Value);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            OnEscPressed?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            OnEnterPressed?.Invoke();
        }

        Vector2 dir = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            dir.y = 1;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            dir.y = -1;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            dir.x = 1;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            dir.x = -1;

        if (dir.x != 0 || dir.y != 0) {
            OnDirectionInput?.Invoke(dir);
        }
    }

    public void CheckRhythmHold(int lane_id) {

    }
}
