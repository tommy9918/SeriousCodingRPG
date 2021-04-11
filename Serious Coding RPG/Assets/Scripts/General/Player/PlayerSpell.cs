using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSpell 
{
    public string spell_id;

    public int average_steps;
    public int average_memory;

    public PlayerSpell(int step, int memory, string spell_id)
    {
        this.spell_id = spell_id;
        this.average_steps = step;
        this.average_memory = memory;
    }

    public BattleSpell GetSpellObjInfo()
    {
        return Resources.Load("ScriptableObjects/BattleSpell/" + spell_id) as BattleSpell;
    }
}
