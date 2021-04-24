using System.Collections;
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

    public void OpenLearnWindow()
    {
        Vector3 pos = Player.Instance.transform.position;
        GameObject temp = Instantiate(learn_window, new Vector3(pos.x, pos.y, learn_window.transform.position.z), Quaternion.identity);
        
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

    public void SaveGame()
    {
        SaveLoad.Save(Player.Instance);
    }
}
