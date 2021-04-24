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

    public GameObject outline_reference;
    public List<GameObject> summoned_outline;
    public int current_line_number;

    public Vector3 block_destination;
    public bool animating;

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
        //inserted_vertical_blocks = new List<GameObject>();
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
        else if (vertical && !highlighted && !animating) SetSubBlockPositionVertical();
        

       

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

    public void DeleteBlock()
    {
        Vector2 s = GetComponent<SpriteRenderer>().size;
        GetComponent<BlockResizeAnimator>().StartAnimate(s, new Vector2(width, height));
    }

    public IEnumerator DeleteVerticalBlock(GameObject blk)
    {
        //Debug.Log(inserted_vertical_blocks.Count);
        if(inserted_vertical_blocks.Count == 1)
        {
            //Debug.Log("Here2");
            Vector2 s = GetComponent<SpriteRenderer>().size;
            GetComponent<BlockResizeAnimator>().StartAnimate(s, new Vector2(width, height));
            yield return new WaitForSeconds(0.5f);
            inserted_vertical_blocks.Remove(blk);
            Destroy(blk);
        }
        else
        {
            //Debug.Log("Here3");
            yield return new WaitForSeconds(0.5f);
            inserted_vertical_blocks.Remove(blk);
            Destroy(blk);
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
                if (inserted_vertical_blocks[i].transform.localScale.y <= 0.95f) total_y_offset += 0.1f;
                inserted_vertical_blocks[i].transform.localPosition = new Vector3(0.1f, total_y_offset, inserted_vertical_blocks[i].transform.localPosition.z);
                total_y_offset -= inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y * inserted_vertical_blocks[i].transform.localScale.y;
                if (inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.x * inserted_vertical_blocks[i].transform.localScale.x > max_x_size)
                {
                    max_x_size = inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.x * inserted_vertical_blocks[i].transform.localScale.x;
                }
                if (inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y * inserted_vertical_blocks[i].transform.localScale.y > max_y_size)
                {
                    max_y_size = inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y * inserted_vertical_blocks[i].transform.localScale.y;
                }

            }
            GetComponent<SpriteRenderer>().size = new Vector2(max_x_size + 1f, -total_y_offset + 0.1f);
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

            if (horizontal)
            {
                Resize(insertion.GetComponent<SpriteRenderer>().size + new Vector2(0.2f, 0.2f));
            }
            else if (vertical)
            {
                Resize(FullExtendLength());
                
            }

        }
    }

    Vector2 FullExtendLength()
    {
        float initial_y = 0.1f;
        float max_x = -1f;
        for (int i = 0; i <= inserted_vertical_blocks.Count - 1; i++)
        {          
            initial_y += inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y;
            initial_y += 0.1f;
            if (inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.x > max_x)
            {
                max_x = inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.x;
            }
        }
        if (highlighted)
        {
            initial_y += incoming_insertion.GetComponent<SpriteRenderer>().size.y;
            if (incoming_insertion.GetComponent<SpriteRenderer>().size.x > max_x)
            {
                max_x = incoming_insertion.GetComponent<SpriteRenderer>().size.x;
            }
            initial_y += 0.1f;
        }
        return new Vector2(max_x + 1f, initial_y);

    }

    int InsertLinePosition(float y_pos)
    {
        //Debug.Log("calculating");
        if (inserted_vertical_blocks.Count == 0) return 1;
        //float offset = 4.6692f + (transform.localPosition.y - 6.1946f);
        float offset = transform.position.y;
        float initial_y = offset;
        for (int i = 0; i <= inserted_vertical_blocks.Count - 1; i++)
        {
            float length = inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y;
            if (y_pos <= initial_y && y_pos > initial_y - length - 0.1f)
            {
                return i + 1;
            }
            initial_y -= 0.1f;
            initial_y -= length;

        }
        return inserted_vertical_blocks.Count + 1;
    }

    void AnimateBlocks(int line_number, float block_size)
    {
        float initial_y = -0.1f;
        for (int i = 0; i <= line_number - 2; i++)
        {
            inserted_vertical_blocks[i].GetComponent<MoveTo>().startPosition = inserted_vertical_blocks[i].transform.localPosition;
            inserted_vertical_blocks[i].GetComponent<MoveTo>().destination = new Vector3(inserted_vertical_blocks[i].transform.localPosition.x, initial_y, inserted_vertical_blocks[i].transform.localPosition.z);
            inserted_vertical_blocks[i].GetComponent<MoveTo>().ReplayMotion();
            initial_y -= inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y;
            initial_y -= 0.1f;
        }

        initial_y -= block_size;
        if (block_size > 0) initial_y -= 0.1f;

        for (int i = line_number - 1; i <= inserted_vertical_blocks.Count - 1; i++)
        {
            inserted_vertical_blocks[i].GetComponent<MoveTo>().startPosition = inserted_vertical_blocks[i].transform.localPosition;
            inserted_vertical_blocks[i].GetComponent<MoveTo>().destination = new Vector3(inserted_vertical_blocks[i].transform.localPosition.x, initial_y, inserted_vertical_blocks[i].transform.localPosition.z);
            inserted_vertical_blocks[i].GetComponent<MoveTo>().ReplayMotion();
            initial_y -= inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y;
            initial_y -= 0.1f;
        }
    }

    Vector3 GetBlockPosition(int line_number)
    {
        float total_y_offset = -0.1f;
        for (int i = 0; i <= line_number - 2; i++)
        {
            total_y_offset -= inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y;
            total_y_offset -= 0.1f;

        }
        return new Vector3(0.1f, total_y_offset, -0.1f);
    }

    Vector2 FingerPos()
    {
        float distance = 0f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        return new Vector2(rayPoint.x, rayPoint.y);
    }

    [ContextMenu("dehighlight")]
    public void Dehighlight()
    {
        if (highlighted)
        {
            highlighted = false;
            highlight_outline.GetComponent<FadeControl>().StartFadeOut();
            if(inserted_block == null && horizontal) Resize(width, height);
            if (vertical)
            {
                if(inserted_vertical_blocks.Count == 0)
                {
                    Resize(width, height);
                }
                else
                {
                    //incoming_insertion = null;
                    Resize(FullExtendLength());
                }
            }
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
                        incoming_insertion.GetComponent<LongPressDrag>().accepted = true;                      
                    }
                    else
                    {
                        //Debug.Log("not match!");
                        incoming_insertion.GetComponent<ErrorBlock>().StartFadeError();
                    }
                }
                if (vertical && BlockMatch(incoming_insertion))
                {
                    float finger_y = FingerPos().y;
                    int line_num = InsertLinePosition(finger_y);
                    if (current_line_number == 0 || line_num != current_line_number)
                    {
                        foreach (GameObject outline in summoned_outline)
                        {
                            outline.GetComponent<FadeControl>().StartFadeOut();
                            Destroy(outline, 0.5f);
                        }
                        summoned_outline.Clear();
                        current_line_number = line_num;
                        Vector2 dragging_block_size = coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block.GetComponent<SpriteRenderer>().size;
                        GameObject temp = Instantiate(outline_reference, GetBlockPosition(line_num), Quaternion.identity, gameObject.transform);
                        temp.transform.localPosition = GetBlockPosition(line_num);
                        block_destination = GetBlockPosition(line_num);
                        temp.GetComponent<SpriteRenderer>().size = dragging_block_size;
                        temp.GetComponent<FadeControl>().StartFadeIn();
                        summoned_outline.Add(temp);
                        AnimateBlocks(current_line_number, dragging_block_size.y);
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
            //incoming_insertion.GetComponent<LongPressDrag>().accepted = false;
            if (BlockMatch(incoming_insertion) == false)
            {
                incoming_insertion.GetComponent<ErrorBlock>().StartFadeBack();
            }
            incoming_insertion = null;

            if (vertical)
            {
                foreach (GameObject outline in summoned_outline)
                {
                    outline.GetComponent<FadeControl>().StartFadeOut();
                    Destroy(outline, 0.5f);
                }
                summoned_outline.Clear();
                current_line_number = 0;
                RepositionBlocks();
            }
        }
    }

    void RepositionBlocks()
    {
        //Debug.Log("Repositioning");
        float initial_y = -0.1f;
        for (int i = 0; i <= inserted_vertical_blocks.Count - 1; i++)
        {
            inserted_vertical_blocks[i].GetComponent<MoveTo>().startPosition = inserted_vertical_blocks[i].transform.localPosition;
            inserted_vertical_blocks[i].GetComponent<MoveTo>().destination = new Vector3(inserted_vertical_blocks[i].transform.localPosition.x, initial_y, inserted_vertical_blocks[i].transform.localPosition.z);
            inserted_vertical_blocks[i].GetComponent<MoveTo>().ReplayMotion();
            initial_y -= inserted_vertical_blocks[i].GetComponent<SpriteRenderer>().size.y;
            initial_y -= 0.1f;
        }
    }

    void OnTouchUp()
    {
        if (highlighted)
        {
            
            //GameObject insertion = coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block;
            incoming_insertion.transform.parent = gameObject.transform;
            //incoming_insertion.GetComponent<LongPressDrag>().accepted = true;
            //incoming_insertion.transform.localPosition = new Vector3(0.1f, -0.1f, -0.03f);
            incoming_insertion.transform.localPosition = new Vector3(incoming_insertion.transform.localPosition.x, incoming_insertion.transform.localPosition.y, -0.03f);
            incoming_insertion.GetComponent<MoveTo>().startPosition = incoming_insertion.transform.localPosition;

            if (horizontal)
            {
                incoming_insertion.GetComponent<MoveTo>().destination = new Vector3(0.1f, -0.1f, -0.03f);
            }
            if (vertical)
            {
                incoming_insertion.GetComponent<MoveTo>().destination = new Vector3(block_destination.x, block_destination.y, -0.03f);
            }
            incoming_insertion.GetComponent<MoveTo>().ReplayMotion();

            incoming_insertion.GetComponent<LongPressDrag>().accepted = true;

            if (horizontal) inserted_block = incoming_insertion;
            else if (vertical) inserted_vertical_blocks.Insert(current_line_number - 1, incoming_insertion);

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
                if(block_inserted.GetComponent<SubBlockManager>().block_type == "at")
                {
                    if(transform.parent.GetComponent<SubBlockManager>() != null && transform.parent.GetComponent<SubBlockManager>().block_type == "at")
                    {
                        return false;
                    }
                }
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
