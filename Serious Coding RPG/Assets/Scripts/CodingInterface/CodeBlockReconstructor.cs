using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeBlockReconstructor : MonoBehaviour
{
    public GameObject code_block_parent;
    public GameObject[] all_blocks_reference;
    public GameObject main_code_area;

    [ContextMenu("TestLoadSum")]
    public void TestLoadSum()
    {
        RebuildBlocks(Player.Instance.data.skills[0].GetOriginalCommandBlockList());
    }

    public void RebuildBlocks(List<CommandBlock> command_blocks)
    {
        for(int i = 0; i <= command_blocks.Count - 1; i++)
        {
            GameObject temp = FillCommandBlock(command_blocks[i], code_block_parent);
            main_code_area.GetComponent<MainCodeArea>().coding_blocks.Add(temp);
            main_code_area.GetComponent<MainCodeArea>().UpdateLineNumberList(true);
            temp.transform.localPosition = new Vector3(0.95f, -0.3f, -0.1f);
            temp.GetComponent<BlockManager>().InititiateBlockSize();
            SetCodingManager(temp);
            //temp.GetComponent<SetMaskInteration>().SetMask("Default", 1);
            temp.GetComponent<SetMaskInteration>().SetInteraction("inside");
        }
        float y_length = -main_code_area.GetComponent<MainCodeArea>().AlignBlocks();
        //Debug.Log(y_length);
        code_block_parent.GetComponent<RectTransform>().sizeDelta = new Vector2(code_block_parent.GetComponent<RectTransform>().sizeDelta.x, y_length + 5f);

    }

    public void RebuildBlocksRepair(List<CommandBlock> command_blocks)
    {
        
        float initial_y = -0.3f;
        float initial_x = 0.95f;
        for (int i = 0; i <= command_blocks.Count - 1; i++)
        {
            GameObject temp = FillCommandBlock(command_blocks[i], code_block_parent);
            //main_code_area.GetComponent<MainCodeArea>().coding_blocks.Add(temp);
            //main_code_area.GetComponent<MainCodeArea>().UpdateLineNumberList(true);
            temp.transform.localPosition = new Vector3(initial_x, initial_y, -0.1f);
            temp.GetComponent<BlockManager>().InititiateBlockSize();
            //SetCodingManager(temp);
            //temp.GetComponent<SetMaskInteration>().SetMask("Default", 1);
            temp.GetComponent<SetMaskInteration>().SetInteraction("inside");
            //temp.GetComponent<SetMaskInteration>().SetOrder(1);
            initial_y -= (temp.GetComponent<SpriteRenderer>().size.y + 0.2f);
        }
        //float y_length = -main_code_area.GetComponent<MainCodeArea>().AlignBlocks();
        //Debug.Log(y_length);
        code_block_parent.GetComponent<RectTransform>().sizeDelta = new Vector2(code_block_parent.GetComponent<RectTransform>().sizeDelta.x, -initial_y + 5f);
    }

    public GameObject FillCommandBlock(CommandBlock blk, GameObject parent)
    {
        //GameObject temp = Instantiate(all_blocks_reference[TypeToIndex(blk.command)]);
        GameObject temp = Instantiate(TypeToGameObject(blk.command));
        temp.transform.parent = parent.transform;
        temp.transform.localPosition = new Vector3(0.1f, -0.1f, -0.03f);
        List<BlockSiteManager> value_slots = new List<BlockSiteManager>();
        List<BlockSiteManager> command_slots = new List<BlockSiteManager>();
        GetAllValueSlots(value_slots, temp);
        GetAllCommandSlots(command_slots, temp);
        if (blk.command_blocks1 != null && blk.command_blocks1.Count > 0)
        {
            for(int i = 0; i <= blk.command_blocks1.Count - 1; i++)
            {
                //GameObject sub_command_blk = Instantiate(all_blocks_reference[TypeToIndex(blk.command_blocks1[i].command)]);
                //temp.transform.parent = command_slots[0].gameObject.transform;
                //temp.transform.localPosition = new Vector3(0.1f, -0.1f, -0.03f);
                //Debug.Log(blk.command_blocks1[i].command);
                GameObject sub_command_blk = FillCommandBlock(blk.command_blocks1[i], command_slots[0].gameObject);
                command_slots[0].inserted_vertical_blocks = new List<GameObject>();
                command_slots[0].inserted_vertical_blocks.Add(sub_command_blk);
                command_slots[0].inserted_block = sub_command_blk;
                //Debug.Log(command_slots[0].inserted_vertical_blocks.Count);
            }
            command_slots[0].SetSubBlockPositionVertical();
        }
        if (blk.command_blocks2 != null && blk.command_blocks2.Count > 0)
        {
            for (int i = 0; i <= blk.command_blocks2.Count - 1; i++)
            {
                //Debug.Log(blk.command_blocks2[i].command);
                GameObject sub_command_blk = FillCommandBlock(blk.command_blocks2[i], command_slots[1].gameObject);
                command_slots[1].inserted_vertical_blocks = new List<GameObject>();
                command_slots[1].inserted_vertical_blocks.Add(sub_command_blk);
                //Debug.Log(command_slots[1].inserted_vertical_blocks.Count);
            }
            command_slots[1].SetSubBlockPositionVertical();
        }
        if (blk.value_blocks != null && blk.value_blocks.Count > 0)
        {
            for (int i = 0; i <= blk.value_blocks.Count - 1; i++)
            {
                GameObject sub_value_blk = FillValueBlock(blk.value_blocks[i], value_slots[i].gameObject);
                if (sub_value_blk != null)
                {
                    value_slots[i].inserted_block = sub_value_blk;
                    value_slots[i].SetSubBlockPositionHorizontal();
                }
            }
            
        }
        temp.GetComponent<BlockManager>().InititiateBlockSize();
        return temp;
    }

    Skill GetSkillFromSkillName(string name)
    {
        foreach(Skill skill in Player.Instance.data.skills)
        {
            if (name == skill.name) return skill;
        }
        return null;
    }

    public GameObject FillValueBlock(ValueBlock blk, GameObject parent)
    {
        if (TypeToGameObject(blk.value_operation) == null) return null;
        //GameObject temp = Instantiate(all_blocks_reference[TypeToIndex(blk.value_operation)]);
        GameObject temp = Instantiate(TypeToGameObject(blk.value_operation));
        if (blk.value_operation == "skill")
        {
            temp.GetComponent<SkillBlockInit>().skill = GetSkillFromSkillName(blk.value);
            temp.GetComponent<SkillBlockInit>().InitializeSkillBlock();
        }
        temp.transform.parent = parent.transform;
        temp.transform.localPosition = new Vector3(0.1f, -0.1f, -0.03f);
        List<BlockSiteManager> value_slots = new List<BlockSiteManager>();
        GetAllValueSlots(value_slots, temp);
        if (blk.value_blocks != null && blk.value_blocks.Count > 0)
        {
            for (int i = 0; i <= blk.value_blocks.Count - 1; i++)
            {
                GameObject sub_value_blk = FillValueBlock(blk.value_blocks[i], value_slots[i].gameObject);
                if (sub_value_blk != null)
                {
                    value_slots[i].inserted_block = sub_value_blk;
                    value_slots[i].SetSubBlockPositionHorizontal();
                }
            }
        }
        else if(temp.GetComponentInChildren<InputField>() != null && blk.value != "" && blk.value != null)
        {
            //Debug.Log(blk.value);
            temp.GetComponentInChildren<InputField>().text = blk.value;
        }
        temp.GetComponent<SubBlockManager>().InititiateBlockSize();
        return temp;
    }

    public void GetAllValueSlots(List<BlockSiteManager> slots, GameObject blk)
    {
        if (blk.GetComponent<BlockManager>() != null)
        {
            List<GameObject> blk_components = blk.GetComponent<BlockManager>().block_sites;
            for(int i = 0; i <= blk_components.Count - 1; i++)
            {
                if(blk_components[i].GetComponent<BlockSiteManager>() != null && blk_components[i].GetComponent<BlockSiteManager>().horizontal)
                {
                    slots.Add(blk_components[i].GetComponent<BlockSiteManager>());
                }
            }
        }
        else if (blk.GetComponent<SubBlockManager>() != null)
        {
            List<GameObject> blk_components = blk.GetComponent<SubBlockManager>().block_sites;
            for (int i = 0; i <= blk_components.Count - 1; i++)
            {
                if (blk_components[i].GetComponent<BlockSiteManager>() != null && blk_components[i].GetComponent<BlockSiteManager>().horizontal)
                {
                    slots.Add(blk_components[i].GetComponent<BlockSiteManager>());
                }
            }
        }
    }

    public void GetAllCommandSlots(List<BlockSiteManager> slots, GameObject blk)
    {
        if (blk.GetComponent<BlockManager>() != null)
        {
            List<GameObject> blk_components = blk.GetComponent<BlockManager>().block_sites;
            for (int i = 0; i <= blk_components.Count - 1; i++)
            {
                if (blk_components[i].GetComponent<BlockSiteManager>() != null && blk_components[i].GetComponent<BlockSiteManager>().vertical)
                {
                    slots.Add(blk_components[i].GetComponent<BlockSiteManager>());
                }
            }
        }
        else if (blk.GetComponent<SubBlockManager>() != null)
        {
            List<GameObject> blk_components = blk.GetComponent<SubBlockManager>().block_sites;
            for (int i = 0; i <= blk_components.Count - 1; i++)
            {
                if (blk_components[i].GetComponent<BlockSiteManager>() != null && blk_components[i].GetComponent<BlockSiteManager>().vertical)
                {
                    slots.Add(blk_components[i].GetComponent<BlockSiteManager>());
                }
            }
        }
    }

    public void SetCodingManager(GameObject blk)
    {
        LongPressDrag[] lpd = GetComponentsInChildren<LongPressDrag>();
        BlockSiteManager[] bsm = GetComponentsInChildren<BlockSiteManager>();
        foreach (LongPressDrag l in lpd)
        {
            l.coding_manager = GetComponent<CodingInterfaceManager>().gameObject;
            l.enabled = false;
        }
        foreach (BlockSiteManager b in bsm)
        {
            b.coding_manager = GetComponent<CodingInterfaceManager>().gameObject;
        }
    }

    public int TypeToIndex(string type)
    {
        switch (type)
        {
            case "num":
                return 0;
            case "char":
                return 1;
            case "variable":
                return 2;
            case "assign":
                return 3;
            case "if":
                return 4;
            case "input":
                return 5;
            case "jump":
                return 6;
            case "output":
                return 7;
            case "and":
                return 8;
            case "or":
                return 9;
            case "equal":
                return 10;
            case "larger":
                return 11;
            case "larger_equal":
                return 12;
            case "smaller":
                return 13;
            case "smaller_equal":
                return 14;
            case "plus":
                return 15;
            case "minus":
                return 16;
            case "multiply":
                return 17;
            case "divide":
                return 18;
            case "remainder":
                return 19;
        }
        return -1;
    }

    public GameObject TypeToGameObject(string type)
    {
        switch (type)
        {
            case "num":
                return Resources.Load("Prefab/CodeBlocks/NumBlock") as GameObject;
            case "char":
                return Resources.Load("Prefab/CodeBlocks/CharBlock") as GameObject;
            case "variable":
                return Resources.Load("Prefab/CodeBlocks/VariableBlock") as GameObject;
            case "assign":
                return Resources.Load("Prefab/CodeBlocks/AssignBlock") as GameObject;
            case "if":
                return Resources.Load("Prefab/CodeBlocks/IfBlock") as GameObject;
            case "input":
                return Resources.Load("Prefab/CodeBlocks/InputBlock") as GameObject;
            case "jump":
                return Resources.Load("Prefab/CodeBlocks/JumpBlock") as GameObject;
            case "output":
                return Resources.Load("Prefab/CodeBlocks/OutputBlock") as GameObject;
            case "and":
                return Resources.Load("Prefab/CodeBlocks/AndBlock") as GameObject;
            case "or":
                return Resources.Load("Prefab/CodeBlocks/OrBlock") as GameObject;
            case "equal":
                return Resources.Load("Prefab/CodeBlocks/EqualBlock") as GameObject;
            case "larger":
                return Resources.Load("Prefab/CodeBlocks/LargerBlock") as GameObject;
            case "larger_equal":
                return Resources.Load("Prefab/CodeBlocks/LargerEqualBlock") as GameObject;
            case "smaller":
                return Resources.Load("Prefab/CodeBlocks/SmallerBlock") as GameObject;
            case "smaller_equal":
                return Resources.Load("Prefab/CodeBlocks/SmallerEqualBlock") as GameObject;
            case "plus":
                return Resources.Load("Prefab/CodeBlocks/PlusBlock") as GameObject;
            case "minus":
                return Resources.Load("Prefab/CodeBlocks/MinusBlock") as GameObject;
            case "multiply":
                return Resources.Load("Prefab/CodeBlocks/MultiplyBlock") as GameObject;
            case "divide":
                return Resources.Load("Prefab/CodeBlocks/DivideBlock") as GameObject;
            case "remainder":
                return Resources.Load("Prefab/CodeBlocks/RemainderBlock") as GameObject;
            case "at":
                return Resources.Load("Prefab/CodeBlocks/AtBlock") as GameObject;
            case "not":
                return Resources.Load("Prefab/CodeBlocks/NotBlock") as GameObject;
            case "skill":
                return Resources.Load("Prefab/CodeBlocks/SkillBlock") as GameObject;
        }
        return null;
    }
}
