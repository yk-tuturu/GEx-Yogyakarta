using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeData { 
    public string key; 
    public string type;
    public string title;
    public string subtitle;
    public string description;
}

[System.Serializable]
public class NodeDataArray {
    public NodeData[] nodes;
}

public class NodeInfoParser : MonoBehaviour
{
    public static NodeInfoParser Instance;
    public Dictionary<string, NodeData> dict = new Dictionary<string, NodeData>();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this; 

        TextAsset jsonFile = Resources.Load<TextAsset>("nodes"); 
        NodeDataArray data = JsonUtility.FromJson<NodeDataArray>(jsonFile.text);

        foreach (NodeData node in data.nodes) {
            dict.Add(node.key, node);
        }
    }

    public NodeData GetNodeInfo(string key) {
        if (dict.ContainsKey(key)) {
            return dict[key];
        }

        return null;
    }
}
