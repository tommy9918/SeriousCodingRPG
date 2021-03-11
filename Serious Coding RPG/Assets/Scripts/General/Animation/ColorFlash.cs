using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFlash : MonoBehaviour
{
    public Color color;
    public int duration;
    public int current;
    public bool animating;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animating)
        {
            if(current <= duration)
            {
                GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1, 1, 1, 1), color, (float)current / (float)duration);
            }
            else if(current <= duration * 2)
            {
                GetComponent<SpriteRenderer>().color = Color.Lerp(color, new Color(1, 1, 1, 1), (float)(current - duration) / (float)duration);
            }
            else
            {
                animating = false;
            }
            current++;
        }
    }

    public void Flash(Color flash_color)
    {
        color = flash_color;
        current = 0;
        animating = true;
    }

    [ContextMenu("TestFlash")]
    public void TestFlash()
    {
        color = new Color(0,1,0,1);
        current = 0;
        animating = true;
    }
}
