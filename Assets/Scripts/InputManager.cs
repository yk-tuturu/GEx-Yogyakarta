using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviourSingleton<InputManager>
{
    public delegate void RhythmLaneInput(int id);
    public event RhythmLaneInput OnRhythmLaneInput;
    public event RhythmLaneInput OnRhythmLaneRelease;

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

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void CheckRhythmHold(int lane_id) {

    }
}
