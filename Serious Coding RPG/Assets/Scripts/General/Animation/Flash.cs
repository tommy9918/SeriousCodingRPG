using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    public float alpha = 0f;

    // Update is called once per frame
    void Update()
    {
        Color c = GetComponent<Image>().color;
        if (alpha > 0f)
        {           
            GetComponent<Image>().color = new Color(c.r, c.g, c.b, alpha);
            alpha -= 1f / 20f;
        }
        else
        {
            alpha = 0f;
            GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0f);
        }
    }

    [ContextMenu("StartFlash")]
    public void StartFlash()
    {
        alpha = 1f;
    }
}
