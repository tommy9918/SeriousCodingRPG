using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill 
{
    public string name;
    public int number_of_slots;
    public List<CommandBlockS> command_blocks;
    public List<ValueBlockS> value_blocks;
    public List<int> command_index;
    public int average_steps;
    public int average_memory;

    public Skill(List<CommandBlock> original_commands, string skill_name, int slot_no)
    {
        name = skill_name;
        number_of_slots = slot_no;
        command_blocks = new List<CommandBlockS>();
        value_blocks = new List<ValueBlockS>();
        command_index = new List<int>();
        for(int i = 0; i <= original_commands.Count - 1; i++)
        {
            command_index.Add(GetSerializeCommandBlockID(original_commands[i]));
        }
    }

    public int GetSerializeCommandBlockID(CommandBlock blk)
    {
        CommandBlockS temp = new CommandBlockS(blk);
        temp.cm_block_id = command_blocks.Count;
        command_blocks.Add(temp);
        for(int i = 0; i <= blk.value_blocks.Count - 1; i++)
        {
            temp.value_blocks.Add(GetSerializeValueBlockID(blk.value_blocks[i]));
        }
        if (blk.command_blocks1 != null)
        {
            for (int i = 0; i <= blk.command_blocks1.Count - 1; i++)
            {
                temp.command_blocks1.Add(GetSerializeCommandBlockID(blk.command_blocks1[i]));
            }
        }
        if (blk.command_blocks2 != null)
        {
            for (int i = 0; i <= blk.command_blocks2.Count - 1; i++)
            {
                temp.command_blocks2.Add(GetSerializeCommandBlockID(blk.command_blocks2[i]));
            }
        }
        return temp.cm_block_id;
    }

    public int GetSerializeValueBlockID(ValueBlock blk)
    {
        ValueBlockS temp = new ValueBlockS(blk);
        temp.v_block_id = value_blocks.Count;
        value_blocks.Add(temp);
        for(int i = 0; i <= blk.value_blocks.Count - 1; i++)
        {
            temp.value_blocks.Add(GetSerializeValueBlockID(blk.value_blocks[i]));
        }
        return temp.v_block_id;
    }

    public List<CommandBlock> GetOriginalCommandBlockList()
    {
        List<CommandBlock> original_list = new List<CommandBlock>();
        for(int i = 0; i <= command_index.Count - 1; i++)
        {
            original_list.Add(CBConversion(command_blocks[command_index[i]]));
        }
        return original_list;
    }

    public CommandBlock CBConversion(CommandBlockS blk)
    {
        CommandBlock original_blk = blk.ConvertBack();
        for (int i = 0; i <= blk.value_blocks.Count - 1; i++)
        {
            original_blk.value_blocks.Add(VBConversion(value_blocks[blk.value_blocks[i]]));
        }
        for (int i = 0; i <= blk.command_blocks1.Count - 1; i++)
        {
            original_blk.command_blocks1.Add(CBConversion(command_blocks[blk.command_blocks1[i]]));
        }
        for (int i = 0; i <= blk.command_blocks2.Count - 1; i++)
        {
            original_blk.command_blocks2.Add(CBConversion(command_blocks[blk.command_blocks2[i]]));
        }
        return original_blk;
    }

    public ValueBlock VBConversion(ValueBlockS blk)
    {
        ValueBlock original_blk = blk.ConvertBack();
        for (int i = 0; i <= blk.value_blocks.Count - 1; i++)
        {
            original_blk.value_blocks.Add(VBConversion(value_blocks[blk.value_blocks[i]]));
        }
        return original_blk;
    }
}
