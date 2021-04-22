using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BattleSpell", menuName = "ScriptableObjects/BattleSpell", order = 1)]
public class BattleSpell : ScriptableObject
{
    public Sprite sprite;
    public string spell_id;
    public string required_skill;

    //public int average_steps;
    //public int average_memory;

    public string usage;
    public float effect_value;

    public GameObject instance_reference;

    public int GetAverageStep()
    {
        foreach(PlayerSpell spell in Player.Instance.data.all_spells)
        {
            if(spell_id == spell.spell_id)
            {
                return spell.average_steps;
            }
        }
        return 1;
    }

    public int GetAverageMemory()
    {
        foreach (PlayerSpell spell in Player.Instance.data.all_spells)
        {
            if (spell_id == spell.spell_id)
            {
                return spell.average_memory;
            }
        }
        return 1;
    }
}
