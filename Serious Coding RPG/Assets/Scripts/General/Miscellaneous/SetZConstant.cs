﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZConstant : MonoBehaviour
{
    public float original_z;
    Vector2 limit = new Vector2(-100, 100);
    public bool constant;

    void Start()
    {
        original_z = transform.position.z;
        //Debug.Log(squish(-100));
        float y = transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y, original_z + squish(y));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateZ();

    }

    float squish(float real)
    {
        float final_max_range = 0.5f;
        float percent = (real - limit.x) / (limit.y - limit.x) - 0.5f;
        return final_max_range * percent;
    }

    [ContextMenu("SetZ")]
    public void UpdateZ()
    {
        float y = transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y, original_z + squish(y));
    }
}
