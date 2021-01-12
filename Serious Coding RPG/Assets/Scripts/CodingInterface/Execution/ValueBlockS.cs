using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValueBlockS 
{
    public int v_block_id;
    public string value_type;
    public string value_operation;
    public List<int> value_blocks;
    public string value;

    public ValueBlockS(ValueBlock blk)
    {
        value_type = blk.value_type;
        value_operation = blk.value_operation;
        value_blocks = new List<int>();
        value = blk.value;
    }

    public ValueBlock ConvertBack()
    {
        ValueBlock blk = new ValueBlock();
        blk.value_type = value_type;
        blk.value_operation = value_operation;
        //blk.value_blocks = new List<ValueBlock>();
        blk.value = value;
        return blk;
    }
}
