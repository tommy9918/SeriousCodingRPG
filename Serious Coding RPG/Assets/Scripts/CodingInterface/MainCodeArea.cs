using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCodeArea : MonoBehaviour
{
    public GameObject coding_manager;
    public GameObject fade_indicator;
    public GameObject block_parent;
    public GameObject outline_reference;
    public List<GameObject> summoned_outline;
    public List<GameObject> coding_blocks;
    public int current_line_number;
    bool highlighted;


    public Vector3 block_destination;
    //GameObject outline_indicator;

    // Start is called before the first frame update
    void Start()
    {
        coding_blocks = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (highlighted)
        {
            GetComponent<Highlight>().Focus();
        }
        else
        {
            GetComponent<Highlight>().Defocus();
        }
    }

    void OnTouchStay()
    {
        if(coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block != null && 
            coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block.GetComponent<BlockManager>() != null)
        {
            highlighted = true;
            float finger_y = FingerPos().y;
            int line_num = InsertLinePosition(finger_y);
            if (current_line_number == 0 || line_num != current_line_number)
            {
                foreach(GameObject outline in summoned_outline)
                {
                    outline.GetComponent<FadeControl>().StartFadeOut();
                    Destroy(outline, 0.5f);
                }
                summoned_outline.Clear();
                current_line_number = line_num;
                Vector2 dragging_block_size = coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block.GetComponent<SpriteRenderer>().size;
                GameObject temp = Instantiate(outline_reference, GetBlockPosition(line_num), Quaternion.identity, block_parent.transform);
                temp.transform.localPosition = GetBlockPosition(line_num);
                block_destination = GetBlockPosition(line_num);
                temp.GetComponent<SpriteRenderer>().size = dragging_block_size;
                temp.GetComponent<FadeControl>().StartFadeIn();
                summoned_outline.Add(temp);
                AnimateBlocks(current_line_number, dragging_block_size.y);
            }

        }
    }

    Vector3 GetBlockPosition(int line_number)
    {
        float total_y_offset = -0.3f;
        for (int i = 0; i <= line_number - 2; i++)
        {          
            total_y_offset -= coding_blocks[i].GetComponent<SpriteRenderer>().size.y;
            total_y_offset -= 0.3f;

        }
        return new Vector3(-3.8f, total_y_offset, -0.1f);
    }

    int InsertLinePosition(float y_pos)
    {
        if (coding_blocks.Count == 0) return 1;
        //Debug.Log(y_pos);
        //Debug.Log(block_parent.transform.localPosition.y);
        float offset = 4.6692f + (block_parent.transform.localPosition.y - 6.1946f);
        float initial_y = offset;
        for(int i=0; i<=coding_blocks.Count-1; i++)
        {
            //if(i < coding_blocks.Count - 1)
            //{
            //    if (y_pos <= coding_blocks[i].transform.position.y && y_pos > coding_blocks[i+1].transform.position.y)
            //    {
            //        return i + 1;
            //    }
            //}
            //else if (i == coding_blocks.Count - 1)
            //{
            //    float length = coding_blocks[i].GetComponent<SpriteRenderer>().size.y;

            //    if (y_pos <= coding_blocks[i].transform.position.y && y_pos > coding_blocks[i].transform.position.y - length)
            //    {
            //        return i + 1;
            //    }
            //}
            float length = coding_blocks[i].GetComponent<SpriteRenderer>().size.y;
            if(y_pos <= initial_y && y_pos > initial_y - length - 0.3f)
            {
                return i + 1;
            }
            initial_y -= 0.3f;
            initial_y -= length;


        }
        return coding_blocks.Count + 1;
    }



    void OnTouchExit()
    {
        if (highlighted)
        {
            highlighted = false;
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

    void OnTouchUp()
    {
        if (highlighted)
        {
            highlighted = false;
            foreach (GameObject outline in summoned_outline)
            {
                outline.GetComponent<FadeControl>().StartFadeOut();
                Destroy(outline, 0.5f);
            }
            summoned_outline.Clear();
            //current_line_number = 0;

            //put block in empty slot
            GameObject inserted_block = coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block;
            //inserted_block.GetComponent<SetMaskInteration>().SetInteraction("inside");
            coding_blocks.Insert(current_line_number - 1, inserted_block);
            inserted_block.transform.parent = block_parent.transform;
            inserted_block.transform.localPosition = new Vector3(inserted_block.transform.localPosition.x, inserted_block.transform.localPosition.y, -0.1f);
            Debug.Log(inserted_block.transform.localPosition);
            inserted_block.GetComponent<MoveTo>().startPosition = inserted_block.transform.localPosition;
            inserted_block.GetComponent<MoveTo>().destination = block_destination;
            inserted_block.GetComponent<MoveTo>().ReplayMotion();

            current_line_number = 0;
        }
    }

    void AnimateBlocks(int line_number, float block_size)
    {
        float initial_y = -0.3f;
        for(int i = 0; i <= line_number - 2; i++){
            coding_blocks[i].GetComponent<MoveTo>().startPosition = coding_blocks[i].transform.localPosition;
            coding_blocks[i].GetComponent<MoveTo>().destination = new Vector3(coding_blocks[i].transform.localPosition.x, initial_y, coding_blocks[i].transform.localPosition.z);
            coding_blocks[i].GetComponent<MoveTo>().ReplayMotion();
            initial_y -= coding_blocks[i].GetComponent<SpriteRenderer>().size.y;
            initial_y -= 0.3f;
        }

        initial_y -= block_size;
        if(block_size > 0) initial_y -= 0.3f;

        for (int i=line_number-1; i<= coding_blocks.Count-1; i++)
        {
            coding_blocks[i].GetComponent<MoveTo>().startPosition = coding_blocks[i].transform.localPosition;
            coding_blocks[i].GetComponent<MoveTo>().destination = new Vector3(coding_blocks[i].transform.localPosition.x, initial_y, coding_blocks[i].transform.localPosition.z);
            coding_blocks[i].GetComponent<MoveTo>().ReplayMotion();
            initial_y -= coding_blocks[i].GetComponent<SpriteRenderer>().size.y;
            initial_y -= 0.3f;
        }
    }

    void RepositionBlocks()
    {
        float initial_y = -0.3f;
        for (int i = 0; i <= coding_blocks.Count - 1; i++)
        {
            coding_blocks[i].GetComponent<MoveTo>().startPosition = coding_blocks[i].transform.localPosition;
            coding_blocks[i].GetComponent<MoveTo>().destination = new Vector3(coding_blocks[i].transform.localPosition.x, initial_y, coding_blocks[i].transform.localPosition.z);
            coding_blocks[i].GetComponent<MoveTo>().ReplayMotion();
            initial_y -= coding_blocks[i].GetComponent<SpriteRenderer>().size.y;
            initial_y -= 0.3f;
        }
    }

    Vector2 FingerPos()
    {
        float distance = 0f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        return new Vector2(rayPoint.x, rayPoint.y);
    }
}
