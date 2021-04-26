using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOffParticle : MonoBehaviour
{
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OffParticle());
    }

    IEnumerator OffParticle()
    {
        yield return new WaitForSeconds(duration);
        foreach(ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.enableEmission = false;
        }
    }
}
