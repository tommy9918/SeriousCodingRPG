using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellRepair : MonoBehaviour
{
    public GameObject repairing_spell;
    public GameObject block_parent;
    public List<GameObject> block_list;
    public GameObject repair_drag_button_ref;
    public List<GameObject> repair_drag_button_list;
    public ScrollRect scroll;

    public int start_index;
    public int difficulty;

    public GameObject scrolling_block;
    
    void RepairSuccess()
    {
        BattleManager.Instance.repairing_spell.GetComponent<SpellManager>().RepairSuccess();
        GetComponent<Dim>().RemoveDim();
        gameObject.SetActive(false);
        foreach (Transform child in block_parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void RepairFail()
    {
        BattleManager.Instance.repairing_spell.GetComponent<SpellManager>().RepairFail();
        foreach (Transform child in block_parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void Update()
    {
        if (scrolling_block != null)
        {
            int scrolling_index = block_list.IndexOf(scrolling_block);
            //Debug.Log(start_index);
            //Debug.Log(scrolling_index);
            for (int i = start_index; i <= start_index + difficulty - 1; i++)
            {
                bool need_animate = false;
                if (!block_list[i].GetComponent<MoveTo>().moving)
                {
                    //Debug.Log("Here1");
                    if (block_list[i].transform.localPosition.y < scrolling_block.transform.localPosition.y && i < scrolling_index)
                    {
                        //Debug.Log("Here2");
                        SwapBlockInList(i, scrolling_index);
                        need_animate = true;
                        block_list[scrolling_index].GetComponent<MoveTo>().startPosition = block_list[scrolling_index].transform.localPosition;
                        Vector3 new_pos = block_list[scrolling_index].transform.localPosition;
                        new_pos.y = GetYPos(scrolling_index);
                        block_list[scrolling_index].GetComponent<MoveTo>().destination = new_pos;
                    }
                    else if (block_list[i].transform.localPosition.y > scrolling_block.transform.localPosition.y && i > scrolling_index)
                    {
                        //Debug.Log("Here3");
                        SwapBlockInList(i, scrolling_index);
                        need_animate = true;
                        block_list[scrolling_index].GetComponent<MoveTo>().startPosition = block_list[scrolling_index].transform.localPosition;
                        Vector3 new_pos = block_list[scrolling_index].transform.localPosition;
                        new_pos.y = GetYPos(scrolling_index);
                        block_list[scrolling_index].GetComponent<MoveTo>().destination = new_pos;
                    }
                    if (need_animate)
                    {
                        //Debug.Log(block_list[scrolling_index].GetComponent<MoveTo>().startPosition);
                        //Debug.Log(block_list[scrolling_index].GetComponent<MoveTo>().destination);
                        block_list[scrolling_index].GetComponent<MoveTo>().ReplayMotion();
                    }
                }
                else
                {
                    if (block_list[i].GetComponent<MoveTo>().destination.y < scrolling_block.transform.localPosition.y && i < scrolling_index)
                    {
                        //Debug.Log("Here4");
                        SwapBlockInList(i, scrolling_index);
                        need_animate = true;
                        block_list[scrolling_index].GetComponent<MoveTo>().startPosition = block_list[scrolling_index].transform.localPosition;
                        Vector3 new_pos = block_list[scrolling_index].transform.localPosition;
                        new_pos.y = GetYPos(scrolling_index);
                        block_list[scrolling_index].GetComponent<MoveTo>().destination = new_pos;
                    }
                    else if (block_list[i].GetComponent<MoveTo>().destination.y > scrolling_block.transform.localPosition.y && i > scrolling_index)
                    {
                        //Debug.Log("Here5");
                        SwapBlockInList(i, scrolling_index);
                        need_animate = true;
                        block_list[scrolling_index].GetComponent<MoveTo>().startPosition = block_list[scrolling_index].transform.localPosition;
                        Vector3 new_pos = block_list[scrolling_index].transform.localPosition;
                        new_pos.y = GetYPos(scrolling_index);
                        block_list[scrolling_index].GetComponent<MoveTo>().destination = new_pos;
                    }
                    if (need_animate)
                    {
                        //Debug.Log(block_list[i].GetComponent<MoveTo>().startPosition);
                        //Debug.Log(block_list[i].GetComponent<MoveTo>().destination);
                        block_list[scrolling_index].GetComponent<MoveTo>().ReplayMotion();
                    }
                }

            }
        }
    }

    public void StopScrolling()
    {
        if (scrolling_block != null)
        {
            int index = block_list.IndexOf(scrolling_block);
            scrolling_block.GetComponent<MoveTo>().startPosition = scrolling_block.transform.localPosition;
            scrolling_block.GetComponent<MoveTo>().destination = scrolling_block.transform.localPosition;
            scrolling_block.GetComponent<MoveTo>().destination.y = GetYPos(index);
            scrolling_block.GetComponent<MoveTo>().ReplayMotion();
            scrolling_block = null;
        }
    }

    void SwapBlockInList(int a, int b)
    {
        GameObject a_block = block_list[a];
        block_list[a] = block_list[b];
        block_list[b] = a_block;
    }

    public void InitializeRepairScreen(int start_index, int difficulty)
    {
        float initial_y = -0.3f;
        this.start_index = start_index;
        this.difficulty = difficulty;
        repair_drag_button_list = new List<GameObject>();
        for (int i = 0; i <= block_list.Count - 1; i++)
        {
            //Debug.Log(coding_blocks[i].GetComponent<SpriteRenderer>().size.y);
            
            
            block_list[i].transform.localPosition = new Vector3(block_list[i].transform.localPosition.x, initial_y, block_list[i].transform.localPosition.z);
            initial_y -= block_list[i].GetComponent<SpriteRenderer>().size.y * block_list[i].transform.localScale.y;
            initial_y -= 0.3f;
            if(i >= start_index && i <= start_index + difficulty - 1)
            {
                block_list[i].GetComponent<LongPressDrag>().repairing = true;
                GameObject drag = Instantiate(repair_drag_button_ref, block_parent.transform);
                drag.transform.localPosition = new Vector3(drag.transform.localPosition.x, block_list[i].transform.localPosition.y - 0.38f, drag.transform.localPosition.z);
                drag.GetComponent<RepairDragButton>().block_attached = block_list[i];
                drag.GetComponent<RepairDragButton>().scroll = scroll;
                drag.GetComponent<RepairDragButton>().repair_manager = this;
                repair_drag_button_list.Add(drag);
            }
            
        }
        //return initial_y;

        GetComponent<FadeControl>().StartFadeIn();
        GetComponent<ScaleChange>().StartAnimate();
        GetComponent<Dim>().StartDim();
    }

    

    float GetYPos(int line_num)
    {
        float initial_y = -0.3f;
        for (int i = 0; i <= block_list.Count - 1; i++)
        {
            if (i == line_num) return initial_y;
            initial_y -= block_list[i].GetComponent<SpriteRenderer>().size.y * block_list[i].transform.localScale.y;
            initial_y -= 0.3f;          
        }
        return 0;
    }

    public void AttemptRepair()
    {
        List<CommandBlock> commandBlock = new List<CommandBlock>();
        for (int i = 0; i <= block_list.Count - 1; i++)
        {
            commandBlock.Add(new CommandBlock(block_list[i].GetComponent<BlockManager>()));
        }
        if (GameManager.Instance.RepairedSkillValid(commandBlock, repairing_spell.GetComponent<SpellManager>().spell.spell_id))
        {
            Debug.Log("Success");
            RepairSuccess();
        }
        else
        {
            Debug.Log("Fail");
            RepairFail();
        }
    }
}
