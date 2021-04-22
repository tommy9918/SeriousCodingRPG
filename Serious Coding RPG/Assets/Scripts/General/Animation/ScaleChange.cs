using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    public bool affectedByTime;

    public bool XYSeparate;
    public float finalScale;
    public float startScale;
    public Vector2 startXY;
    public Vector2 finalXY;
    Motion motion;
    Motion motion2;
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
            if (!XYSeparate)
            {
                if (motion != null && motion.current <= moveTime)
                {
                    float newScale = motion.UpdateValue();
                    transform.localScale = new Vector3(newScale, newScale, newScale);
                }
            }
            else if (XYSeparate)
            {
                if (motion != null && motion2 != null && motion.current <= moveTime)
                {
                    float newScaleX = motion.UpdateValue();
                    float newScaleY = motion2.UpdateValue();
                    transform.localScale = new Vector3(newScaleX, newScaleY, 1f);
                }
            }
        }
    }

    public void StartAnimate()
    {
        if (!XYSeparate)
        {
            motion = new Motion(moveTime, startScale, finalScale);
            transform.localScale = new Vector3(startScale, startScale, startScale);
        }
        else if (XYSeparate)
        {
            motion = new Motion(moveTime, startXY.x, finalXY.x);
            motion2 = new Motion(moveTime, startXY.y, finalXY.y);
            transform.localScale = new Vector3(startXY.x, startXY.y, 1f);
        }

    }

    public void StartAnimateReverse()
    {
        if (!XYSeparate)
        {
            motion = new Motion(moveTime, finalScale, startScale);
            transform.localScale = new Vector3(finalScale, finalScale, finalScale);
        }
        else if (XYSeparate)
        {
            motion = new Motion(moveTime, finalXY.x, startXY.x);
            motion2 = new Motion(moveTime, finalXY.y, startXY.y);
            transform.localScale = new Vector3(finalXY.x, finalXY.y, 1f);
        }
    }
}
