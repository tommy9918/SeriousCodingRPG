using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public float speed;
    public float max_alpha;
    public float min_alpha;
    public float current_alpha;
    public Color original_color;
    // Start is called before the first frame update
    void Start()
    {
        original_color = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(original_color.r, original_color.g, original_color.b, 0f);
        current_alpha = min_alpha;
    }


    public void Focus()
    {
        GetComponent<SpriteRenderer>().color = new Color(original_color.r, original_color.g, original_color.b, current_alpha);
        current_alpha += speed;
        if(current_alpha >= max_alpha)
        {
            current_alpha = max_alpha;
        }
    }

    public void Defocus()
    {
        GetComponent<SpriteRenderer>().color = new Color(original_color.r, original_color.g, original_color.b, current_alpha);
        current_alpha -= speed;
        if (current_alpha <= min_alpha)
        {
            current_alpha = min_alpha;
        }
    }
}
