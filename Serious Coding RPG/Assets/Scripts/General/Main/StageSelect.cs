using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    public GameObject routes;
    public GameObject areas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeStageSelect()
    {
        routes.SetActive(true);
        areas.SetActive(false);
        GetComponent<FadeControl>().StartFadeIn();
        GetComponent<ScaleChange>().StartAnimate();
        GetComponent<Dim>().StartDim();
    }

    public void CloseWindow()
    {
        GetComponent<FadeControl>().StartFadeOut();
        GetComponent<ScaleChange>().StartAnimateReverse();
        GetComponent<Dim>().RemoveDim();
        Destroy(gameObject, 0.6f);
    }

    public void SwitchToRoutes()
    {
        routes.SetActive(true);
        areas.SetActive(false);
    }

    public void SwitchToAreas()
    {
        routes.SetActive(false);
        areas.SetActive(true);
    }
}
