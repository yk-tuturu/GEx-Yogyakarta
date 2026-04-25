using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Node up; 
    public Node left;
    public Node right;
    public Node down;

    public string key;
    public bool unlocked = false;

    public GameObject grayNode;

    public event Action<Node> moveToNode;
    public bool enableClick = true;
    // Start is called before the first frame update
    void Start()
    {
        NodeData data = NodeInfoParser.Instance.GetNodeInfo(key);

        bool canUnlock = true;

        foreach (string prereq in data.prereq) {
            if (!PlayerPrefManager.Instance.GetLevelCompletion(prereq)) {
                canUnlock = false;
                break;
            }
        }

        if (canUnlock) {
            unlocked = true;
            grayNode.SetActive(false);
        } else {
            grayNode.SetActive(true);
        }

        MenuStateManager.Instance.OnExitState += ExitState;
    }

    void OnMouseDown()
    {
        if (!enableClick) {
            return;
        }

        MenuStateManager msm = MenuStateManager.Instance;

        if (msm.currentNode == this) {
            EventSystem.current.SetSelectedGameObject(null);
            // Also consume the current mouse click
            Input.ResetInputAxes();

            msm.ChangeState(MenuState.Panel);
        } else {
            msm.currentNode = this;
            moveToNode?.Invoke(this);
        }
    }

    void ExitState(MenuState state) {
        MenuStateManager msm = MenuStateManager.Instance;

        if (state == MenuState.Panel && msm.currentNode == this) {
            enableClick = false;
            StartCoroutine(DelayEnableClick());
        } 
    }

    IEnumerator DelayEnableClick() {
        yield return new WaitForSeconds(0.4f);
        enableClick = true;
    }
}
