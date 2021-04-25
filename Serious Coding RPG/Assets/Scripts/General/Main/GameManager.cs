﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject map_ui;
    public GameObject coding_ui;
    public GameObject battle_ui;
    public GameObject main_ui;

    public GameObject learn_window;
    public GameObject profile_window;
    public GameObject organise_spell_window;

    public GameObject black_transition;

    void Awake()
    {
        if (Instance == null)   //singleton Player instance, easy for referencing in other scripts
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenProfile()
    {
        SpawnWindowAtCamera(profile_window);
    }

    public void OpenOrganiseSpellWindow()
    {
        SpawnWindowAtCamera(organise_spell_window);
    }

    public void OpenLearnWindow()
    {
        Vector3 pos = Player.Instance.transform.position;
        GameObject temp = Instantiate(learn_window, new Vector3(pos.x, pos.y, learn_window.transform.position.z), Quaternion.identity);
        
    }

    public int RepairSpellLength(string spell_id)
    {
        foreach(PlayerSpell spell in Player.Instance.data.all_spells)
        {
            if(spell_id == spell.spell_id)
            {

                return GetSkillByName(GetSpellBySpellID(spell_id).required_skill).command_index.Count;
            }
        }
        return 0;
    }

    public bool NowCoding()
    {
        return CodingInterfaceManager.Instance != null && CodingInterfaceManager.Instance.gameObject.active == true;
    }

    public IEnumerator StartMission(string quest_id)
    {
        Vector3 pos = Player.Instance.gameObject.transform.position;
        black_transition.transform.position = new Vector3(pos.x, pos.y, black_transition.transform.position.z);       
        black_transition.GetComponent<FadeControl>().StartFadeIn();
        main_ui.GetComponent<MainUI>().HideMainUI();
        yield return new WaitForSeconds(0.5f);
        map_ui.SetActive(false);
        coding_ui.GetComponent<CodingInterfaceManager>().questID = quest_id;
        coding_ui.transform.position = (Vector2)Player.Instance.gameObject.transform.position;
        coding_ui.GetComponent<CodingInterfaceManager>().InitializeCodingUI();
        //coding_ui.transform.position = new Vector3(0, 0, coding_ui.transform.position.z);
        //main_ui.transform.parent.transform.position = new Vector3(0, 0, coding_ui.transform.position.z);
        coding_ui.SetActive(true);
        black_transition.GetComponent<FadeControl>().StartFadeOut();

    }

    public IEnumerator FinishMission(string quest_id)
    {
        main_ui.GetComponent<MainUI>().ShowMainUI();
        black_transition.GetComponent<FadeControl>().StartFadeIn();
        
        yield return new WaitForSeconds(0.5f);
        map_ui.SetActive(true);
        coding_ui.GetComponent<CodingInterfaceManager>().ResetCodingUI();
        coding_ui.SetActive(false);
        black_transition.GetComponent<FadeControl>().StartFadeOut();
        QuestManager.Instance.FinishQuest(quest_id);
        SaveLoad.Save(Player.Instance);
    }

    public BattleSpell GetSpellBySkillName(string skill)
    {
        foreach (BattleSpell bs in Resources.LoadAll("ScriptableObjects/BattleSpell") as BattleSpell[])
        {
            if (skill == bs.required_skill) return bs;
        }
        return null;
    }

    public Skill GetSkillByName(string name)
    {
        foreach(Skill skill in Player.Instance.data.skills)
        {
            if (name == skill.name) return skill;
        }
        return null;
    }

    public BattleSpell GetSpellBySpellID(string id)
    {
        foreach (BattleSpell bs in Resources.LoadAll("ScriptableObjects/BattleSpell"))
        {
            if (id == bs.spell_id)
            {
                return bs;
            }
        }
        return null;
    }

    public List<BattleSpell> GetBuyableSpell()
    {
        List<BattleSpell> battle_spell_list = new List<BattleSpell>();
        foreach (BattleSpell bs in Resources.LoadAll("ScriptableObjects/BattleSpell"))
        {
            if(Player.Instance.HaveSkill(bs.required_skill) && !Player.Instance.HaveSpell(bs.name))
            {
                battle_spell_list.Add(bs);
            }
        }
        return battle_spell_list;
        //return null;
    }

    public List<BattleSpell> GetLearntSpell()
    {
        List<BattleSpell> learnt_list = new List<BattleSpell>();
        foreach (BattleSpell bs in Resources.LoadAll("ScriptableObjects/BattleSpell"))
        {
            if (Player.Instance.HaveSpell(bs.name))
            {
                learnt_list.Add(bs);
            }
        }
        return learnt_list;
    }

    public List<BattleSpell> GetUnequippedSpell()
    {
        List<BattleSpell> uneqipped_spell = new List<BattleSpell>();
        foreach (BattleSpell spell in GetLearntSpell())
        {
            bool equipped = false;
            foreach(PlayerData.PlayerSpellList spell_list in Player.Instance.data.spells_in_channels)
            {
                foreach(PlayerSpell bspell in spell_list.spell_list)
                {
                    if(bspell.spell_id == spell.spell_id)
                    {
                        equipped = true;
                        break;
                    }

                }
                if (equipped) break;
            }
            if (!equipped) uneqipped_spell.Add(spell);
        }
        return uneqipped_spell;
    }

    public PlayerSpell FindPlayerSpellBySpellID(string spell_id)
    {
        foreach(PlayerSpell spell in Player.Instance.data.all_spells)
        {
            if(spell.spell_id == spell_id)
            {
                return spell;
            }
        }
        return null;
    }

    public GameObject SpawnWindowAtCamera(GameObject reference)
    {
        GameObject temp = Instantiate(reference);
        Vector3 pos = Player.Instance.gameObject.transform.position;
        temp.transform.position = new Vector3(pos.x, pos.y, reference.transform.position.z);
        return temp;
    }

    public void SaveGame()
    {
        SaveLoad.Save(Player.Instance);
    }
}
