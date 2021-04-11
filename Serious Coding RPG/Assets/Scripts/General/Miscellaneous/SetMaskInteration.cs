using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaskInteration : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    

    public void InitializeSpritesArray()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    public void SetMask(string mask_name, int layer)
    {
        InitializeSpritesArray();
        //Debug.Log("setmask");
        foreach(SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = layer;
            //Debug.Log(sprite.sortingOrder);
            //Debug.Log(layer);
            sprite.sortingLayerName = mask_name;
        }
    }

    public void SetOrder(int layer)
    {
        InitializeSpritesArray();
        //Debug.Log("setmask");
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = layer;
            //Debug.Log(sprite.sortingOrder);
            //Debug.Log(layer);
            //sprite.sortingLayerName = mask_name;
        }
    }

    public void SetInteraction(string description)
    {
        InitializeSpritesArray();
        //Debug.Log("setinteraction");
        switch (description)
        {
            case "none":
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.maskInteraction = SpriteMaskInteraction.None;
                }
                break;
            case "inside":
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                }
                break;
            case "outside":
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                }
                break;
            

        }
        
    }
}
