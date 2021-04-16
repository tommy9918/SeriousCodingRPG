using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelClick : MonoBehaviour
{
    public Collider2D range;
    public string ButtonUse;
    bool pressed;
    bool cancel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.touchCount != 0 || Input.GetMouseButton(0)))
        {
            //Debug.Log("clicked");
            if (!range.bounds.Contains(FingerPos()))
            {
                //Debug.Log("clicked outside");
                pressed = true;
            }
            else
            {
                //Debug.Log("clicked inside");
            }
        }
        else
        {
            if (pressed && !cancel)
            {
                //Debug.Log("cancel");
                cancel = true;
                transform.SendMessage(ButtonUse, options: SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    Vector2 FingerPos()
    {
        float distance = 0f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        return new Vector2(rayPoint.x, rayPoint.y);
    }
}
