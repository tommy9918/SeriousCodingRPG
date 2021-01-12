using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CommandBlockS
{
    public int cm_block_id;
    public string command;
    public List<int> value_blocks;
    public List<int> command_blocks1;     //then
    public List<int> command_blocks2;     //else

    public CommandBlockS(CommandBlock blk)
    {
        command = blk.command;
        value_blocks = new List<int>();
        command_blocks1 = new List<int>();
        command_blocks2 = new List<int>();
    }

    public CommandBlock ConvertBack()
    {
        CommandBlock blk = new CommandBlock();
        blk.command = command;
        blk.value_blocks = new List<ValueBlock>();
        blk.command_blocks1 = new List<CommandBlock>();
        blk.command_blocks2 = new List<CommandBlock>();
        return blk;
    }
}
