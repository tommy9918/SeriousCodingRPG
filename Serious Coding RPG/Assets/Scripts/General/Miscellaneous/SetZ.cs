using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZ : MonoBehaviour
{

    public float original_z;
    Vector2 limit = new Vector2(-100, 100);

    void Start()
    {
        original_z = transform.position.z;
        //Debug.Log(squish(-100));
        float y = transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y, original_z + squish(y));
    }

    
    float squish(float real)
    {
        float final_max_range = 0.5f;
        float percent = (real - limit.x) / (limit.y - limit.x) - 0.5f;
        return final_max_range * percent;
    }
}
