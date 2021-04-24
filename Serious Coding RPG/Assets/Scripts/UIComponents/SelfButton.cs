using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfButton : MonoBehaviour
{
    bool pressed;
    public string ButtonUse;
    public int duration;
    int current = 0;

    void OnTouchDown()
    {
        pressed = true;
    }

    private void Update()
    {
        if(duration != 0 && pressed)
        {
            current++;
            if(current == duration)
            {
                transform.SendMessage(ButtonUse, options: SendMessageOptions.DontRequireReceiver);
                current = 0;
            }
        }
    }

    void OnTouchExit()
    {
        pressed = false;
        current = 0;
    }

    void OnTouchUp()
    {
        if (pressed && duration == 0)
        {
            transform.SendMessage(ButtonUse, options: SendMessageOptions.DontRequireReceiver);
        }
        pressed = false;
        current = 0;
    }
}
