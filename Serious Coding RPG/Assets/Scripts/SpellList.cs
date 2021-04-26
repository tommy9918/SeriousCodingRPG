using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour
{
    public GameObject spell_base;
    public int target_spell_list_id;
    public List<BattleSpell> spell_info_list;
    public List<GameObject> spell_list;
    //public List<GameObject> spell_list_ref;
    public int current_index;
    float this_x;
    public int pop_index;
    public GameObject magic_effect;
    public GameObject black_mist;
    public SpellCircle progress_control;

    float black_mist_probability = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        //InitializeSpellList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeSpellList(int index)
    {
        target_spell_list_id = index;
        this_x = 0;
        spell_info_list = new List<BattleSpell>();
        for(int i = 0; i <= Player.Instance.data.spells_in_channels[target_spell_list_id].spell_list.Count - 1; i++)
        {
            spell_info_list.Add(Player.Instance.data.spells_in_channels[target_spell_list_id].spell_list[i].GetSpellObjInfo());
        }
        spell_list = new List<GameObject>();
        float y = -2.17f;
        for(int i = 0; i <= 5; i++)
        {
            //spell_list.Add(Instantiate(spell_list_ref[current_index], transform));
            spell_list.Add(Instantiate(spell_base, transform));
            spell_list[i].GetComponent<SpellManager>().spell = spell_info_list[current_index];
            spell_list[i].GetComponent<SpellManager>().InitializeSpell();
            spell_list[i].GetComponent<SpellManager>().DisableNamePart();
            spell_list[i].GetComponent<SpellManager>().in_battle = true;

            spell_list[i].transform.localPosition = new Vector3(this_x, y, -1.35f);
            y -= 1.44f;
            

            if (Random.Range(0f, 1f) < black_mist_probability && SpellCorruptable(spell_info_list[current_index]))
            {
                GameObject temp_mist = Instantiate(black_mist, spell_list[i].transform);
                temp_mist.transform.localPosition = new Vector3(0, 0, -0.1f);
                spell_list[i].GetComponent<SpellManager>().black_mist = temp_mist;
            }
            current_index = (current_index + 1) % spell_info_list.Count;
        }
        pop_index = 0;
        progress_control.InitializeSpellCircle(target_spell_list_id);
        progress_control.list = gameObject;
    }

    public void GoNextSpell()
    {
        BattleManager.Instance.ActivateSpell(spell_info_list[pop_index], spell_list[0]);
        Vector3 effect_pos = spell_list[0].transform.position;
        effect_pos.z -= 1f;
        Destroy(Instantiate(magic_effect, effect_pos, Quaternion.identity), 1f);
        pop_index = (pop_index + 1) % spell_info_list.Count;
        spell_list[0].GetComponent<FadeControl>().StartFadeOut();
        //spell_list.Add(Instantiate(spell_list_ref[current_index], transform));
        spell_list.Add(Instantiate(spell_base, transform));
        spell_list[6].GetComponent<SpellManager>().spell = spell_info_list[current_index];
        spell_list[6].GetComponent<SpellManager>().InitializeSpell();
        spell_list[6].GetComponent<SpellManager>().DisableNamePart();
        spell_list[6].GetComponent<SpellManager>().in_battle = true;
        spell_list[6].transform.localPosition = new Vector3(this_x, -10.88f, -1.35f);
        if (Random.Range(0f, 1f) < black_mist_probability && SpellCorruptable(spell_info_list[current_index]))
        {
            GameObject temp_mist = Instantiate(black_mist, spell_list[6].transform);
            temp_mist.transform.localPosition = new Vector3(0, 0, -0.1f);
            spell_list[6].GetComponent<SpellManager>().black_mist = temp_mist;
        }
        current_index = (current_index + 1) % spell_info_list.Count;
        foreach(GameObject obj in spell_list)
        {
            obj.GetComponent<MoveTo>().startPosition = obj.transform.localPosition;
            obj.GetComponent<MoveTo>().destination = new Vector3(this_x, obj.transform.localPosition.y + 1.44f, -1.35f);
            obj.GetComponent<MoveTo>().ReplayMotion();
        }
        Destroy(spell_list[0], 1f);
        spell_list.RemoveAt(0);
        
    }

    bool SpellCorruptable(BattleSpell spell)
    {
        Debug.Log(spell.spell_id);
        //if (spell.spell_id == "MIRACLE") Debug.Log(GameManager.Instance.RepairSpellLength(spell.spell_id));
        return GameManager.Instance.RepairSpellLength(spell.spell_id) >= 3;
    }
 

}
