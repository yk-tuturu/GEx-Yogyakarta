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
    // Start is called before the first frame update
    void Start()
    {
        NodeData data = NodeInfoParser.Instance.GetNodeInfo(key);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
