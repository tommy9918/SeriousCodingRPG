using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectsZ : MonoBehaviour
{
    public float original_z;
    Vector2 limit = new Vector2(-100, 100);

    [ContextMenu("SetZOrder")]
    public void SetZOrder()
    {
        foreach(Transform child in transform)
        {
            original_z = -1f;
            //Debug.Log(squish(-100));
            float y = child.position.y;
            child.position = new Vector3(child.position.x, child.position.y, original_z + squish(y));
        }
    }

    float squish(float real)
    {
        float final_max_range = 0.5f;
        float percent = (real - limit.x) / (limit.y - limit.x) - 0.5f;
        return final_max_range * percent;
    }
}
