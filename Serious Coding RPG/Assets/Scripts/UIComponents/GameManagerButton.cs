using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerButton : MonoBehaviour
{
    
    Vector2 start_pos;
    Vector2 end_pos;
    float start_time;
    float end_time;
    public string ButtonUse;
    bool pressed;   

    void OnTouchDown()
    {
        pressed = true;
        start_pos = FingerPos();
        start_time = Time.time;
    }

    void OnTouchExit()
    {
        pressed = false;
    }

    void OnTouchUp()
    {
        if (pressed)
        {
            end_pos = FingerPos();
            end_time = Time.time;
            if (start_pos != null && Vector2.Distance(start_pos, end_pos) <= 0.02f && end_time - start_time <= 1f)
            {
                GameManager.Instance.SendMessage(ButtonUse, options: SendMessageOptions.DontRequireReceiver);
            }
        }
        pressed = false;
    }

    Vector2 FingerPos()
    {
        float distance = 0f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        return new Vector2(rayPoint.x, rayPoint.y);
    }
}
