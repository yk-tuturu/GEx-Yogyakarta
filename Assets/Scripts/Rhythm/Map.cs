using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HitObjectData
{
    public int id;
    public float targetTime;
    public int lane; 
    public string hitsound;
}



[System.Serializable]
public class Map
{
    public List<HitObjectData> data;
}
