using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node up; 
    public Node left;
    public Node right;
    public Node down;

    public string key;
    public bool unlocked = false;

    public GameObject grayNode;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
