using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowchart : MonoBehaviour
{
    public Dictionary<string, Node> dict = new Dictionary<string, Node>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform) {
            Node node = child.GetComponent<Node>();
            dict.Add(node.key, node);
        }
    }

    public Node GetNode(string key) {
        if (!dict.ContainsKey(key)) {
            Debug.Log("node not found");
            return null;
        }

        return dict[key];
    }
}
