using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

public class Rotate : MonoBehaviour
{
    public bool affectedByTime;

    public float speed;
    public float radius;
    public Vector2 centre;
    public bool constant; //constant orbiting mode
    public bool random;  //random mode
    public bool specific; //specific mode
    public float specificDegree; //for specific mode

    public float waitTime; //for random
    float waitcount = 0;
    public float moveTime; //for random
    private float randomDegreeDelta;
    public float nowDegree = 0f;

    Motion motion;

    // Start is called before the first frame update
    void Start()
    {

        if (random)
        {
            randomDegreeDelta = Random.value * 720 - 360;
            specificDegree = nowDegree + randomDegreeDelta;
            moveTime = Mathf.Abs(randomDegreeDelta * 0.235f);
            motion = new Motion(moveTime, nowDegree, specificDegree);
        }
        else if (specific)
        {
            if (moveTime == 0)
            {
                moveTime = Mathf.Abs(specificDegree * 0.235f);
            }
            motion = new Motion(moveTime, nowDegree, specificDegree);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log((affectedByTime && Time.timeScale != 0) || !affectedByTime);
        if ((affectedByTime && Time.timeScale != 0) || !affectedByTime)
        {
            if (constant)
            {
                nowDegree = (nowDegree + speed) % 360;
                //Debug.Log(Time.timeScale);
                transform.eulerAngles = new Vector3(0, 0, nowDegree);
            }
            else if (random)
            {
                if (motion.current <= moveTime)
                {
                    nowDegree = motion.UpdateValue() % 360;
                    transform.eulerAngles = new Vector3(0, 0, nowDegree);
                }
                else
                {
                    waitcount++;
                    if (waitcount >= waitTime)
                    {
                        waitcount = 0;
                        randomDegreeDelta = Random.value * 720 - 360;
                        specificDegree = nowDegree + randomDegreeDelta;
                        moveTime = Mathf.Abs(randomDegreeDelta * 0.5f);
                        motion.initializeMotion(moveTime, nowDegree, specificDegree);
                    }
                }
            }
            else if (specific)
            {
                if (motion.current <= moveTime)
                {
                    nowDegree = motion.UpdateValue() % 360;
                    transform.eulerAngles = new Vector3(0, 0, nowDegree);
                }
                else
                {
                    specific = false;
                }
            }
        }

    }

    [ContextMenu("StartAnimateSpecific")]
    public void StartAnimateSpecific()
    {
        specific = true;
        if (moveTime == 0)
        {
            moveTime = Mathf.Abs(specificDegree * 0.235f);
        }
        nowDegree = 0;
        motion = new Motion(moveTime, nowDegree, specificDegree);
    }
}
