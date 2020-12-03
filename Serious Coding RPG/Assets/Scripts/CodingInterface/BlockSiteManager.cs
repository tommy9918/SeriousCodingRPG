using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSiteManager : MonoBehaviour
{
    public float width;
    public float height;

    public bool horizontal;
    public bool vertical;
    public bool highlighted;

    public GameObject highlight_outline_reference;
    public GameObject highlight_outline;

    public GameObject coding_manager;
    public GameObject incoming_insertion;

    //public List<GameObject> inserted_blocks;
    public GameObject inserted_block;
    public List<GameObject> inserted_vertical_blocks;

    public enum SiteType
    {
        VARIABLE,
        VALUE,
        CONDITIONAL,
        VARIABLE_VALUE,
        CONDITONAL_VALUE,
        BLOCK,
    }
    public SiteType type;
    public string value_type;
    // Start is called before the first frame update
    void Start()
    {
        inserted_vertical_blocks = new List<GameObject>();
        AutoUpdateSize();
    }

    public SubBlockManager.BlockType getInsertedType()
    {
        return inserted_block.GetComponent<SubBlockManager>().type;
    }

    // Update is called once per frame
    void Update()
    {
        if(highlight_outline != null)
        {
            highlight_outline.GetComponent<SpriteRenderer>().size = GetComponent<SpriteRenderer>().size;
        }

        if (horizontal) SetSubBlockPositionHorizontal();
        else if (vertical) SetSubBlockPositionVertical();

    }

    void AlignBlocks()
    {
        float initial_y = -0.1f;
        for (int i = 0; i <= inserted_vertical_blocks.Count - 1; i++)
        {
            inserted_vertical_blocks[i].transform.localPosition = new Vector3(inserted_vertical_blocks[i].transform.localPosition.x, initial_y, inserted_vertical_blocks[i].transform.localPosition.z);
            initial_y -= inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y;
            initial_y -= 0.1f;
        }
    }

    public void SetSubBlockPositionHorizontal()
    {
        if (inserted_block != null)
        {
            Vector2 block_size = inserted_block.GetComponent<SpriteRenderer>().size;
            GetComponent<SpriteRenderer>().size = new Vector2(block_size.x + 0.2f, block_size.y + 0.2f);
        }
    }

    public void SetSubBlockPositionVertical()
    {
        if (inserted_vertical_blocks != null && inserted_vertical_blocks.Count >= 1)
        {
            float max_x_size = -1f;
            float max_y_size = -1f;
            float total_y_offset = 0;
            for (int i = 0; i <= inserted_vertical_blocks.Count - 1; i++)
            {
                total_y_offset -= 0.1f;
                inserted_vertical_blocks[i].transform.localPosition = new Vector3(0.1f, total_y_offset, inserted_vertical_blocks[i].transform.localPosition.z);
                total_y_offset -= inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y;
                if (inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.x > max_x_size)
                {
                    max_x_size = inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.x;
                }
                if (inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y > max_y_size)
                {
                    max_y_size = inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y;
                }

            }
            GetComponent<SpriteRenderer>().size = new Vector2(max_x_size + 0.2f, -total_y_offset + 0.1f);
        }
    }

    public void AutoUpdateSize()
    {
        width = GetComponent<SpriteRenderer>().size.x;
        height = GetComponent<SpriteRenderer>().size.y;
    }

    

    //[ContextMenu("resize")]
    public void Resize(float x, float y)
    {
        //float x1 = 5f;
        //float y1 = 10f;
        Vector2 original_size = GetComponent<SpriteRenderer>().size;
        GetComponent<BlockResizeAnimator>().StartAnimate(original_size, new Vector2(x, y));
        highlight_outline.GetComponent<BlockResizeAnimator>().StartAnimate(original_size, new Vector2(x, y));
    }

    public void Resize(Vector2 new_size)
    {
        Vector2 original_size = GetComponent<SpriteRenderer>().size;
        GetComponent<BlockResizeAnimator>().StartAnimate(original_size, new_size);
        highlight_outline.GetComponent<BlockResizeAnimator>().StartAnimate(original_size, new_size);
    }

    [ContextMenu("highlight")]
    public void Highlight()
    {
        if (!highlighted)
        {
            highlight_outline = Instantiate(highlight_outline_reference, transform);
            highlight_outline.transform.localPosition = new Vector3(0, 0, -0.01f);
            highlight_outline.GetComponent<FadeControl>().StartFadeIn();
            highlighted = true;

            GameObject insertion = coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block;
            Resize(insertion.GetComponent<SpriteRenderer>().size+ new Vector2(0.2f, 0.2f));

        }
    }

    [ContextMenu("dehighlight")]
    public void Dehighlight()
    {
        if (highlighted)
        {
            highlighted = false;
            highlight_outline.GetComponent<FadeControl>().StartFadeOut();
            if(inserted_block == null) Resize(width, height);
            Destroy(highlight_outline, 0.6f);
        }
    }

    

    void OnTouchStay()
    {
        if (coding_manager && inserted_block == null)
        {
            if (coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block != null && coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block != transform.parent.gameObject)
            {
                if (incoming_insertion == null)
                {
                    incoming_insertion = coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block;
                    if (BlockMatch(incoming_insertion))
                    {
                        //Debug.Log("match!");
                        Highlight();
                    }
                    else
                    {
                        //Debug.Log("not match!");
                        incoming_insertion.GetComponent<ErrorBlock>().StartFadeError();
                    }
                }
            }
        }
    }

    void OnTouchExit()
    {
        Dehighlight();
        if (incoming_insertion != null)
        {
            if (BlockMatch(incoming_insertion) == false)
            {
                incoming_insertion.GetComponent<ErrorBlock>().StartFadeBack();
            }
            incoming_insertion = null;
        }
    }

    void OnTouchUp()
    {
        if (highlighted)
        {
            
            //GameObject insertion = coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block;
            incoming_insertion.transform.parent = gameObject.transform;
            //incoming_insertion.transform.localPosition = new Vector3(0.1f, -0.1f, -0.03f);
            incoming_insertion.transform.localPosition = new Vector3(incoming_insertion.transform.localPosition.x, incoming_insertion.transform.localPosition.y, -0.03f);
            incoming_insertion.GetComponent<MoveTo>().startPosition = incoming_insertion.transform.localPosition;
            incoming_insertion.GetComponent<MoveTo>().destination = new Vector3(0.1f, -0.1f, -0.03f);
            incoming_insertion.GetComponent<MoveTo>().ReplayMotion();

            if(horizontal) inserted_block = incoming_insertion;
            else if (vertical) inserted_vertical_blocks.Add(incoming_insertion);

            if (horizontal) SetSubBlockPositionHorizontal();
            else if (vertical) SetSubBlockPositionVertical();

            Dehighlight();

        }
    }

    

    public bool BlockMatch(GameObject block_inserted)
    {
        if(type == SiteType.BLOCK)
        {
            if (block_inserted.GetComponent<BlockManager>() == null)
            {
                return false;
            }
            else return true;
        }
        else if(type != SiteType.BLOCK && block_inserted.GetComponent<BlockManager>() != null)
        {
            return false;
        }
        else
        {
            SubBlockManager.BlockType inserted_type = block_inserted.GetComponent<SubBlockManager>().type;

            if (inserted_type == SubBlockManager.BlockType.CONDITIONAL && type == SiteType.CONDITIONAL)
            {
                return true;
            }

            else if (inserted_type == SubBlockManager.BlockType.VALUE && type == SiteType.VALUE)
            {
                
                if (block_inserted.GetComponent<SubBlockManager>().value_type.Equals(value_type))
                {
                    return true;
                }
                else return false;
            }

            else if (inserted_type == SubBlockManager.BlockType.VARIABLE && type == SiteType.VARIABLE)
            {
                if (block_inserted.GetComponent<SubBlockManager>().value_type.Equals(value_type))
                {
                    return true;
                }
                else return false;
            }

            else if(type == SiteType.VARIABLE_VALUE)
            {
                if(inserted_type == SubBlockManager.BlockType.VARIABLE || inserted_type == SubBlockManager.BlockType.VALUE)
                {
                    if (block_inserted.GetComponent<SubBlockManager>().value_type.Equals(value_type) || value_type == "" || block_inserted.GetComponent<SubBlockManager>().value_type == "")
                    {
                        return true;
                    }
                    else return false;
                }
            }

            else if(type == SiteType.CONDITONAL_VALUE)
            {
                SiteType correct_type = transform.parent.gameObject.GetComponent<SubBlockManager>().getCorrectSiteType(gameObject);
                switch (correct_type)
                {
                    case SiteType.CONDITIONAL:
                        if (inserted_type == SubBlockManager.BlockType.CONDITIONAL)
                        {
                            return true;
                        }
                        else return false;
                        

                    case SiteType.CONDITONAL_VALUE:
                        if (inserted_type == SubBlockManager.BlockType.CONDITIONAL || 
                            inserted_type == SubBlockManager.BlockType.VARIABLE ||
                            inserted_type == SubBlockManager.BlockType.VALUE)
                        {
                            return true;
                        }
                        else return false;
                        

                    case SiteType.VARIABLE_VALUE:
                        if (inserted_type == SubBlockManager.BlockType.VARIABLE ||
                            inserted_type == SubBlockManager.BlockType.VALUE)
                        {
                            string correct_value_type = transform.parent.gameObject.GetComponent<SubBlockManager>().getCorrectValueType(gameObject);
                            //Debug.Log(correct_value_type);
                            if (correct_value_type.Equals("all") || correct_value_type.Equals("") || incoming_insertion.GetComponent<SubBlockManager>().value_type == "")
                            {
                                return true;
                            }
                            else if (correct_value_type.Equals(incoming_insertion.GetComponent<SubBlockManager>().value_type))
                            {
                                return true;
                            }
                            else return false;
                        }
                        else return false;
                        
                }
                //return false;
            }
            
            return false;
        }
    }
}
