using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParticleColor : MonoBehaviour
{
    public void Set(Color color)
    {
        ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem p in ps)
        {
            p.startColor = color;
        }
    }
}
