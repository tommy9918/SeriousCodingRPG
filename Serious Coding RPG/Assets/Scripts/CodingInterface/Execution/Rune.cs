using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public int rune_index;
    public SpriteRenderer rune_sprite;
    public Color right_color;
    public Color wrong_color;

    public void SetRight(int index)
    {
        rune_index = index;
        foreach(SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = new Color(right_color.r, right_color.g, right_color.b, sprite.color.a);
        }
        GetComponent<SetParticleColor>().Set(right_color);
        foreach(AnimationRoutine ar in GetComponentsInChildren<AnimationRoutine>())
        {
            ar.StartAnimation();
        }
    }

    public void SetWrong(int index)
    {
        rune_index = index;
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = new Color(wrong_color.r, wrong_color.g, wrong_color.b, sprite.color.a);
        }
        GetComponent<SetParticleColor>().Set(wrong_color);
        foreach (AnimationRoutine ar in GetComponentsInChildren<AnimationRoutine>())
        {
            ar.StartAnimation();
        }
    }

    void OnTouchUp()
    {
        ChooseDebugTestCase();
    }

    public void ChooseDebugTestCase()
    {
        transform.parent.gameObject.GetComponent<CodingInterfaceManager>().SwitchDebugSpace(rune_index);
    }
}
