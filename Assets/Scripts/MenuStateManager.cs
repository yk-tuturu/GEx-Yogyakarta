using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MenuState {
    Navigation,
    Panel,
    Settings
}

public class MenuStateManager : MonoBehaviour
{
    public static MenuStateManager Instance;

    public MenuState currentState;
    public Node currentNode;

    public delegate void StateChange(MenuState state);
    public event StateChange OnEnterState; 
    public event StateChange OnExitState;

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = MenuState.Navigation;
        OnEnterState?.Invoke(currentState);
    }

    public void ChangeState(MenuState state) {
        OnExitState?.Invoke(currentState);
        currentState = state;
        OnEnterState?.Invoke(state);
    }
}
