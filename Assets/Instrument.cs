using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    public GameObject glowPrefab;

    public Transform[] positions;
    // Start is called before the first frame update
    public void Play(int index) {
        Debug.Log("instrument played");
        Instantiate(glowPrefab, positions[index].position, Quaternion.identity);
    }
}
