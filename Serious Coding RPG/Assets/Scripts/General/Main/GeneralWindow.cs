using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<FadeControl>().StartFadeIn();
        GetComponent<ScaleChange>().StartAnimate();
        GetComponent<Dim>().StartDim();
    }

    public void CloseWindow()
    {
        GetComponent<FadeControl>().StartFadeOut();
        GetComponent<ScaleChange>().StartAnimateReverse();
        GetComponent<Dim>().RemoveDim();
        Destroy(gameObject, 0.5f);
    }
}
