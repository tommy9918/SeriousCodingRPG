using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildButton : MonoBehaviour
{
    public GameObject parent;
    public string ButtonUse;
    bool pressed;

    // Start is called before the first frame update
    void Awake()
    {
        parent = transform.parent.gameObject;
    }

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
            parent.SendMessage(ButtonUse, options: SendMessageOptions.DontRequireReceiver);
        }
        pressed = false;
    }


}
