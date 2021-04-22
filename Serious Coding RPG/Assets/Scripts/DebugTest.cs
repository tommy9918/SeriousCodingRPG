using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    public GameObject skill_detail;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTouchUp()
    {
        if (skill_detail.active == false)
        {
            skill_detail.SetActive(true);
        }
        else
        {
            skill_detail.SetActive(false);
        }
    }
}
