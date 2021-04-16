using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ValueBlock 
{
    public string value_type;
    public string value_operation;
    public List<ValueBlock> value_blocks;
    public string value;

    public ValueBlock()
    {
        value_type = "";
        value_operation = "empty";
        value_blocks = new List<ValueBlock>();
        value = "";
    }

    public ValueBlock(SubBlockManager blk)
    {
        if (blk.value_type != null)
        {
            value_type = blk.value_type;
        }
        else value_type = "";
        value_operation = blk.block_type;
        value_blocks = new List<ValueBlock>();
        for(int i=0; i<=blk.block_sites.Count - 1; i++)
        {
            if(blk.block_sites[i].GetComponent<BlockSiteManager>() != null)
            {
                if (blk.block_sites[i].GetComponent<BlockSiteManager>().inserted_block != null)
                {
                    SubBlockManager child_sub_blk = blk.block_sites[i].GetComponent<BlockSiteManager>().inserted_block.GetComponent<SubBlockManager>();
                    value_blocks.Add(new ValueBlock(child_sub_blk));
                }
                else
                {
                    value_blocks.Add(new ValueBlock());
                    //value_blocks[value_blocks.Count - 1] = null;
                }
            }
        }
        if (blk.value_reference != null)
        {
            value = blk.value_reference.GetComponent<Text>().text;
        }
        else value = "";
        //else value = this.GetValue();      
    }

    public ValueBlock(ValueBlock vblk)
    {
        value_type = vblk.value_type;
        value_operation = vblk.value_operation;
        value_blocks = vblk.value_blocks;
        value = vblk.value;
    }

    public string GetValue()
    {
        switch (value_operation)
        {
            case "":
                //Debug.Log("guess correct!");
                return value;
            case "num":
                return value;
            case "char":
                return value;
            case "variable":
                return value;
            case "plus":
                return (double.Parse(value_blocks[0].GetValue()) + double.Parse(value_blocks[1].GetValue())).ToString();
            case "minus":
                return (double.Parse(value_blocks[0].GetValue()) - double.Parse(value_blocks[1].GetValue())).ToString();
            case "multiply":
                return (double.Parse(value_blocks[0].GetValue()) * double.Parse(value_blocks[1].GetValue())).ToString();
            case "divide":
                return ((int)(double.Parse(value_blocks[0].GetValue()) / double.Parse(value_blocks[1].GetValue()))).ToString();
            case "remainder":
                return (double.Parse(value_blocks[0].GetValue()) % double.Parse(value_blocks[1].GetValue())).ToString();

        }
        //Debug.Log(value_operation);
        return "";
    }

}
