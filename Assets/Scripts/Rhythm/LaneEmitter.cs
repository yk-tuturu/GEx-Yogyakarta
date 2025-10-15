using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneEmitter : MonoBehaviour
{
    public ParticleSystem ps1;
    public ParticleSystem ps2;

    public int burstCount = 6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Emit() {
        ps1.Emit(burstCount);
        ps2.Emit(burstCount);
    }
}
