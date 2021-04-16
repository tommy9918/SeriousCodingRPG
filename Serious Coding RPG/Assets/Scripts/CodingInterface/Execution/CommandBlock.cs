using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommandBlock 
{
    public string command;
    public List<ValueBlock> value_blocks;
    public List<CommandBlock> command_blocks1;     //then
    public List<CommandBlock> command_blocks2;     //else

    public CommandBlock()
    {
        command = "";
        value_blocks = new List<ValueBlock>();
        command_blocks1 = new List<CommandBlock>();
        command_blocks2 = new List<CommandBlock>();
    }

    public CommandBlock(BlockManager blk)
    {
        command = toType(blk.type);
        value_blocks = new List<ValueBlock>();
        for (int i = 0; i <= blk.block_sites.Count - 1; i++)
        {
            if (blk.block_sites[i].GetComponent<BlockSiteManager>() != null && blk.block_sites[i].GetComponent<BlockSiteManager>().horizontal == true)
            {
                if (blk.block_sites[i].GetComponent<BlockSiteManager>().inserted_block != null)
                {
                    SubBlockManager child_sub_blk = blk.block_sites[i].GetComponent<BlockSiteManager>().inserted_block.GetComponent<SubBlockManager>();
                    //Debug.Log(child_sub_blk);
                    value_blocks.Add(new ValueBlock(child_sub_blk));
                }
                else
                {
                    value_blocks.Add(new ValueBlock());
                    //value_blocks[value_blocks.Count - 1] = null;
                }
            }
        }
        if(command == "if")
        {
            command_blocks1 = new List<CommandBlock>();
            command_blocks2 = new List<CommandBlock>();
            int first_length = blk.block_sites[3].GetComponent<BlockSiteManager>().inserted_vertical_blocks.Count;
            int second_length = blk.block_sites[5].GetComponent<BlockSiteManager>().inserted_vertical_blocks.Count;
            for (int i = 0;i <= first_length - 1; i++)
            {
                BlockManager next_blk = blk.block_sites[3].GetComponent<BlockSiteManager>().inserted_vertical_blocks[i].GetComponent<BlockManager>();
                command_blocks1.Add(new CommandBlock(next_blk));
            }
            for (int i = 0; i <= second_length - 1; i++)
            {
                BlockManager next_blk = blk.block_sites[5].GetComponent<BlockSiteManager>().inserted_vertical_blocks[i].GetComponent<BlockManager>();
                command_blocks2.Add(new CommandBlock(next_blk));
            }

        }
    }

    public string toType(BlockManager.BlockType type)
    {
        switch (type)
        {
            case BlockManager.BlockType.ASSIGN:
                return "assign";
            case BlockManager.BlockType.IF:
                return "if";
            case BlockManager.BlockType.INPUT:
                return "input";
            case BlockManager.BlockType.OUTPUT:
                return "output";
            case BlockManager.BlockType.JUMP:
                return "jump";
        }
        return "";
    }
}
