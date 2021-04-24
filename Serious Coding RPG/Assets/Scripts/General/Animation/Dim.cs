using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dim : MonoBehaviour
{
    public GameObject dim_ref;
    public GameObject instance;

    private void Awake()
    {
        //dim_ref = Resources.Load("Prefab/Main/dark_bg") as GameObject;
    }

    public void StartDim()
    {
        dim_ref = Resources.Load("Prefab/Main/dark_bg") as GameObject;
        instance = Instantiate(dim_ref, transform.position - (Vector3.back * 0.1f), Quaternion.identity);
        instance.GetComponent<FadeControl>().StartFadeIn();
    }

    public void RemoveDim()
    {
        instance.GetComponent<FadeControl>().StartFadeOut();
        Destroy(instance, 0.5f);
    }
}
