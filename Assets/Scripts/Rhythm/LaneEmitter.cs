using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneEmitter : MonoBehaviour
{
    public ParticleSystem ps1;
    public ParticleSystem ps2;

    public int burstCount = 6;

    public void Emit() {
        ps1.Emit(burstCount);
        ps2.Emit(burstCount);
    }

    void OnDestroy()
    {
        ps1.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ps2.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
