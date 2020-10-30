using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    GameObject NPC;
    bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        NPC = gameObject.transform.parent.gameObject;
    }

    void OnTouchDown()
    {
        pressed = true;
    }

    void OnTouchUp()
    {
        if (pressed)
        {
            pressed = false;
            Debug.Log("Dialogue Start!");
            NPC.GetComponent<NPCManager>().StartDialogue();
        }
    }
}
