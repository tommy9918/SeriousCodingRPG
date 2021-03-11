﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour
{
    public GameObject spell_base;
    public List<BattleSpell> spell_info_list;
    public List<GameObject> spell_list;
    public List<GameObject> spell_list_ref;
    public int current_index;
    public float this_x;
    public int pop_index;
    public GameObject magic_effect;
    public GameObject black_mist;
    // Start is called before the first frame update
    void Start()
    {
        InitializeSpellList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeSpellList()
    {
        spell_list = new List<GameObject>();
        float y = -2.17f;
        for(int i = 0; i <= 5; i++)
        {
            spell_list.Add(Instantiate(spell_list_ref[current_index], transform));
            spell_list[i].transform.localPosition = new Vector3(this_x, y, -11.35f);
            y -= 1.44f;
            current_index = (current_index + 1) % spell_list_ref.Count;

            if (Random.Range(0f, 1f) < 0.1f)
            {
                GameObject temp_mist = Instantiate(black_mist, spell_list[i].transform);
                temp_mist.transform.localPosition = new Vector3(0, 0, -0.1f);
            }
        }
        pop_index = 0;
    }

    public void GoNextSpell()
    {
        BattleManager.Instance.ActivateSpell(spell_info_list[pop_index], spell_list[0]);
        Vector3 effect_pos = spell_list[0].transform.position;
        effect_pos.z -= 1f;
        Destroy(Instantiate(magic_effect, effect_pos, Quaternion.identity), 1f);
        pop_index = (pop_index + 1) % spell_list_ref.Count;
        spell_list[0].GetComponent<FadeControl>().StartFadeOut();
        spell_list.Add(Instantiate(spell_list_ref[current_index], transform));
        spell_list[6].transform.localPosition = new Vector3(this_x, -10.88f, -11.35f);
        if (Random.Range(0f, 1f) < 0.1f)
        {
            GameObject temp_mist = Instantiate(black_mist, spell_list[6].transform);
            temp_mist.transform.localPosition = new Vector3(0, 0, -0.1f);
        }
        current_index = (current_index + 1) % spell_list_ref.Count;
        foreach(GameObject obj in spell_list)
        {
            obj.GetComponent<MoveTo>().startPosition = obj.transform.localPosition;
            obj.GetComponent<MoveTo>().destination = new Vector3(this_x, obj.transform.localPosition.y + 1.44f, -11.35f);
            obj.GetComponent<MoveTo>().ReplayMotion();
        }
        Destroy(spell_list[0], 1f);
        spell_list.RemoveAt(0);
        
    }

    bool SpellDestroyable(BattleSpell spell)
    {
        return true;
    }
 

}
