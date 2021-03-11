using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelection : MonoBehaviour
{
    public GameObject[] all_blocks_reference;
    public GameObject[] main_blocks_reference;
    public GameObject[] calculation_blocks_reference;
    public GameObject[] variable_blocks_reference;
    public GameObject[] conditional_blocks_reference;
    public GameObject[] custom_blocks_list;
    public GameObject skill_block_reference;
    public GameObject selection_outline;
    public string type_state;
    public List<GameObject> summoned_blocks;

    public GameObject scroll_list;
    

    private void OnEnable()
    {
        summoned_blocks = new List<GameObject>();
        //SummonAllBlockAsChild();
        SwitchMain();
        
        //GetComponent<FadeControl>().GetAllComponenets();
        StartAnimate();
        
    }

    private void OnDisable()
    {
        DeleteAllBLocks();
    }

    void DeleteAllBLocks()
    {
        foreach(GameObject blk in summoned_blocks)
        {
            if (blk.transform.parent == scroll_list.transform)
            {
                Destroy(blk);
            }
        }
        summoned_blocks.Clear();
    }

    public void FadeAllBlocks()
    {
        foreach (GameObject blk in summoned_blocks)
        {
            if (blk.transform.parent == scroll_list.transform)
            {
                blk.GetComponent<FadeControl>().StartFadeOut();
            }
        }
        //summoned_blocks.Clear();
    }

    //void SummonAllBlockAsChild()
    //{
    //    float total_y_offset = 0f;
    //    for(int i = 0; i <= all_blocks_reference.Length - 1; i++)
    //    {
    //        total_y_offset -= 0.3f;
    //        GameObject temp = Instantiate(all_blocks_reference[i], scroll_list.transform);
    //        temp.transform.localPosition = new Vector3(-3.17f, total_y_offset, -0.01f);

    //        if(temp.GetComponent<BlockManager>() != null)
    //        {
    //            temp.GetComponent<BlockManager>().InititiateBlockSize();
    //        }
    //        else if(temp.GetComponent<SubBlockManager>() != null)
    //        {
    //            temp.GetComponent<SubBlockManager>().InititiateBlockSize();
    //        }
    //        temp.GetComponent<LongPressDrag>().coding_manager = transform.parent.gameObject;

    //        total_y_offset -= temp.GetComponent<SpriteRenderer>().size.y;

    //        summoned_blocks.Add(temp);
    //    }

    //}



    public void SwitchMain()
    {
        if (type_state != "main" || summoned_blocks.Count == 0)
        {
            RemoveAllSummonedBlocks();
            type_state = "main";
            MoveSelectionOutline(-3.97f);
            StartCoroutine(SummonBlocksSequence(main_blocks_reference));
        }
    }

    public void SwitchVariable()
    {
        if (type_state != "variable")
        {
            RemoveAllSummonedBlocks();
            type_state = "variable";
            MoveSelectionOutline(-1.95f);
            StartCoroutine(SummonBlocksSequence(variable_blocks_reference));
        }
    }

    public void SwitchConditional()
    {
        if (type_state != "conditional")
        {
            RemoveAllSummonedBlocks();
            type_state = "conditional";
            MoveSelectionOutline(1.95f);
            StartCoroutine(SummonBlocksSequence(conditional_blocks_reference));
        }
    }

    public void SwitchCalculation()
    {
        if (type_state != "calculation")
        {
            RemoveAllSummonedBlocks();
            type_state = "calculation";
            MoveSelectionOutline(0f);
            StartCoroutine(SummonBlocksSequence(calculation_blocks_reference));
        }
    }

    public void SwitchCustom()
    {
        if (type_state != "custom")
        {
            RemoveAllSummonedBlocks();
            type_state = "custom";
            MoveSelectionOutline(3.97f);
            StartCoroutine(SummonCustomBlocksSequence());
        }
    }

    public void MoveSelectionOutline(float x_coor)
    {
        selection_outline.GetComponent<MoveTo>().startPosition = selection_outline.transform.localPosition;
        selection_outline.GetComponent<MoveTo>().destination = new Vector3(x_coor, selection_outline.transform.localPosition.y, selection_outline.transform.localPosition.z);
        selection_outline.GetComponent<MoveTo>().ReplayMotion();
    }

    public void RemoveAllSummonedBlocks()
    {
        foreach(GameObject blks in summoned_blocks)
        {
            blks.GetComponent<ScaleChange>().StartAnimateReverse();
            Destroy(blks, 0.5f);
            
        }
        summoned_blocks.Clear();
    }

    IEnumerator SummonCustomBlocksSequence()
    {
        float total_y_offset = 0f;

        for (int i = 0; i <= Player.Instance.data.skills.Count - 1; i++)
        {
            total_y_offset -= 0.3f;
            GameObject temp = Instantiate(skill_block_reference, scroll_list.transform);
            temp.GetComponent<SkillBlockInit>().skill = Player.Instance.data.skills[i];
            temp.GetComponent<SkillBlockInit>().InitializeSkillBlock();
            temp.GetComponent<SubBlockManager>().SetSkillBlockPosition();
            temp.transform.localPosition = new Vector3(-3.17f, total_y_offset, -0.01f);

            if (temp.GetComponent<BlockManager>() != null)
            {
                temp.GetComponent<BlockManager>().InititiateBlockSize();
            }
            else if (temp.GetComponent<SubBlockManager>() != null)
            {
                temp.GetComponent<SubBlockManager>().InititiateBlockSize();
            }
            temp.GetComponent<LongPressDrag>().coding_manager = transform.parent.gameObject;
            temp.GetComponent<ScaleChange>().StartAnimate();
            total_y_offset -= temp.GetComponent<SpriteRenderer>().size.y;

            summoned_blocks.Add(temp);

            temp.GetComponent<SetMaskInteration>().InitializeSpritesArray();
            temp.GetComponent<SetMaskInteration>().SetMask("Default", 0);
            temp.GetComponent<SetMaskInteration>().SetInteraction("inside");


            yield return new WaitForSeconds(0.05f);
        }
        //GetComponent<FadeControl>().GetAllComponenets();
    }



    IEnumerator SummonBlocksSequence(GameObject[] blocks)
    {
        float total_y_offset = 0f;
        for (int i = 0; i <= blocks.Length - 1; i++)
        {
            total_y_offset -= 0.3f;
            GameObject temp = Instantiate(blocks[i], scroll_list.transform);
            temp.transform.localPosition = new Vector3(-3.17f, total_y_offset, -0.01f);

            if (temp.GetComponent<BlockManager>() != null)
            {
                temp.GetComponent<BlockManager>().InititiateBlockSize();
            }
            else if (temp.GetComponent<SubBlockManager>() != null)
            {
                temp.GetComponent<SubBlockManager>().InititiateBlockSize();
            }
            temp.GetComponent<LongPressDrag>().coding_manager = transform.parent.gameObject;
            temp.GetComponent<ScaleChange>().StartAnimate();
            total_y_offset -= temp.GetComponent<SpriteRenderer>().size.y;

            summoned_blocks.Add(temp);

            temp.GetComponent<SetMaskInteration>().InitializeSpritesArray();
            temp.GetComponent<SetMaskInteration>().SetMask("Default", 0);
            temp.GetComponent<SetMaskInteration>().SetInteraction("inside");


            yield return new WaitForSeconds(0.05f);
        }
        //GetComponent<FadeControl>().GetAllComponenets();
    }

    //IEnumerator AnimationCoroutineDemo(GameObject[] obj)
    //{
    //    //make a series of gameobjects fade in one by one with interval of 0.1 second
    //    for(int i = 0; i <= obj.Length - 1; i++)
    //    {
    //        obj[i].GetComponent<FadeControl>().StartFadeIn(); 
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}


    // Start is called before the first frame update
    void Start()
    {
        //StartAnimate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimate()
    {
        GetComponent<FadeControl>().StartFadeIn();
        GetComponent<ScaleChange>().StartAnimate();
    }

    
}
