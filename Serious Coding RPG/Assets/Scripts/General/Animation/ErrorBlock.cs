using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorBlock : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    List<Color> original_colors;

    int duration = 10;
    int current = 0;

    bool fading_error;
    bool fading_back;

    Color error_color = new Color(1, 0.2f, 0.2f, 0.8f);

    // Start is called before the first frame update
    void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        original_colors = new List<Color>();
        for(int i = 0; i <= sprites.Length - 1; i++) 
        {
            original_colors.Add(sprites[i].color);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fading_error)
        {
            for (int i = 0; i <= sprites.Length - 1; i++)
            {
                sprites[i].color = Color.Lerp(original_colors[i], error_color, (float)current / (float)duration);
            }
            current++;
        }
        if (fading_back)
        {
            for (int i = 0; i <= sprites.Length - 1; i++)
            {
                sprites[i].color = Color.Lerp(error_color, original_colors[i], (float)current / (float)duration);
            }
            current++;
        }
    }

    [ContextMenu("Fade to error")]
    public void StartFadeError()
    {
        fading_error = true;
        fading_back = false;
        current = 0;

    }

    [ContextMenu("Fade back")]
    public void StartFadeBack()
    {
        fading_error = false;
        fading_back = true;
        current = 0;
    }
}
