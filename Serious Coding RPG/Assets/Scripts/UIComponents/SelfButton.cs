using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfButton : MonoBehaviour
{
    bool pressed;
    public string ButtonUse;

    void OnTouchDown()
    {
        pressed = true;
    }

    void OnTouchExit()
    {
        pressed = false;
    }

    void OnTouchUp()
    {
        if (pressed)
        {
            transform.SendMessage(ButtonUse, options: SendMessageOptions.DontRequireReceiver);
        }
        pressed = false;
    }
}
