﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public float width;
    public float height;  

    public bool dragging;
    public bool pressed;
    Vector3 finger_start_position;
    Vector3 finger_current_position;

    public GameObject ScrollTarget;
    
    public List<GameObject> block_sites;
    public int block_line_number;

    public bool horizontal;
    public bool vertical;

    public enum BlockType
    {
        ASSIGN,
        INPUT,
        OUTPUT,
        IF,
        JUMP
    }
    public BlockType type;
    
    // Start is called before the first frame update
    void Start()
    {
        InititiateBlockSize();
    }

    public void InititiateBlockSize()
    {
        if (horizontal) SetSubBlockPositionHorizontal();
        else if (vertical) SetSubBlockPositionVertical();

        width = GetComponent<SpriteRenderer>().size.x;
        height = GetComponent<SpriteRenderer>().size.y;

        
    }

    // Update is called once per frame
    void Update()
    {
        AutoUpdateSize();
        if(horizontal) SetSubBlockPositionHorizontal();
        else if(vertical) SetSubBlockPositionVertical();
    }

    public void SetSubBlockPositionHorizontal()
    {
        if(block_sites != null)
        {
            float max_x_size = -1f;
            float max_y_size = -1f;
            float total_x_offset = 0;
            for(int i = 0; i <= block_sites.Count - 1; i++)
            {
                total_x_offset += 0.1f;
                block_sites[i].transform.localPosition = new Vector3(total_x_offset, -0.1f, block_sites[i].transform.localPosition.z);
                total_x_offset += block_sites[i].GetComponent<SpriteRenderer>().size.x;
                if (block_sites[i].GetComponent<SpriteRenderer>().size.x > max_x_size)
                {
                    max_x_size = block_sites[i].GetComponent<SpriteRenderer>().size.x;
                }
                if (block_sites[i].GetComponent<SpriteRenderer>().size.y > max_y_size)
                {
                    max_y_size = block_sites[i].GetComponent<SpriteRenderer>().size.y;
                }

            }
            GetComponent<SpriteRenderer>().size = new Vector2(total_x_offset + 0.1f, max_y_size + 0.2f);
        }
    }

    public void SetSubBlockPositionVertical()
    {
        if (block_sites != null)
        {
            float max_x_size = -1f;
            float max_y_size = -1f;
            float total_y_offset = 0;
            for (int i = 0; i <= block_sites.Count - 1; i++)
            {
                total_y_offset -= 0.1f;
                block_sites[i].transform.localPosition = new Vector3(0.1f, total_y_offset, block_sites[i].transform.localPosition.z);
                total_y_offset -= block_sites[i].GetComponent<SpriteRenderer>().size.y;
                if (block_sites[i].GetComponent<SpriteRenderer>().size.x > max_x_size)
                {
                    max_x_size = block_sites[i].GetComponent<SpriteRenderer>().size.x;
                }
                if (block_sites[i].GetComponent<SpriteRenderer>().size.y > max_y_size)
                {
                    max_y_size = block_sites[i].GetComponent<SpriteRenderer>().size.y;
                }

            }
            GetComponent<SpriteRenderer>().size = new Vector2(max_x_size + 0.2f, -total_y_offset + 0.1f);
        }
    }

    public Vector2 GetBlockSize()
    {
        Vector2 size = new Vector2();
        return size;
    }

    public void SetBlock()
    {
        switch (type)
        {
            case BlockType.ASSIGN:
                break;
            case BlockType.INPUT:
                break;
            case BlockType.OUTPUT:
                break;
            case BlockType.IF:
                break;
            case BlockType.JUMP:
                break;
        }
    }

    public void UpdateSize(float x, float y)
    {
        width = x;
        height = y;
    }

    public void AutoUpdateSize()
    {
        width = GetComponent<SpriteRenderer>().size.x;
        height = GetComponent<SpriteRenderer>().size.y;
    }

    public void Resize(float x, float y)
    {
        Vector2 original_size = GetComponent<SpriteRenderer>().size;
        GetComponent<BlockResizeAnimator>().StartAnimate(original_size, new Vector2(x, y));
    }

    

    public void OnTouchDown()
    {

    }

    public void SetScrollTarget()
    {
        ScrollTarget = transform.parent.gameObject;
    }
}