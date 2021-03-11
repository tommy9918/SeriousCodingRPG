using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    public bool affectedByTime;

    public bool XYSeparate;
    public float finalScale;
    public float startScale;
    Motion motion;
    public float moveTime;
    public bool auto_start;


    // Start is called before the first frame update
    void OnEnable()
    {
        if (auto_start) StartAnimate();
    }

    // Update is called once per frame
    void Update()
    {
        if (affectedByTime && Time.timeScale != 0 || !affectedByTime)
        {
            if (motion != null && motion.current <= moveTime)
            {
                float newScale = motion.UpdateValue();
                transform.localScale = new Vector3(newScale, newScale, newScale);
            }
        }
    }

    public void StartAnimate()
    {
        motion = new Motion(moveTime, startScale, finalScale);
        transform.localScale = new Vector3(startScale, startScale, startScale);
    }

    public void StartAnimateReverse()
    {
        motion = new Motion(moveTime, finalScale, startScale);
        transform.localScale = new Vector3(finalScale, finalScale, finalScale);
    }
}
