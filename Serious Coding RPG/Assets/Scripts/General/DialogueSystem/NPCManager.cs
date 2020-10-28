using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject speechBubble;
    public float react_radius;
    public bool canTalkTo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(transform.position, Player.Instance.character.transform.position);
        if(dist < react_radius)
        {
            SetBubbleOn();
        }
        else
        {
            SetBubbleOff();
        }
    }

    void SetBubbleOn()
    {
        if (!canTalkTo)
        {
            canTalkTo = true;
            speechBubble.GetComponent<ScaleChange>().StartAnimate();          
        }
        else
        {
            speechBubble.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void SetBubbleOff()
    {
        if (canTalkTo)
        {
            canTalkTo = false;
            speechBubble.GetComponent<BoxCollider2D>().enabled = false;
            speechBubble.GetComponent<ScaleChange>().StartAnimateReverse();
        }
    }
}
