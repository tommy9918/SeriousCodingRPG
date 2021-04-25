using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public GameObject monster_reference;
    public List<Monster> stage_monster;
    public List<GameObject> monster_summoned;
    public Image Player_HP_Bar;
    public Image Player_MP_Bar;
    public List<GameObject> spell_lists;
    public bool paused;
    public Coroutine MPAutoRestore;
    public AdventureProgressBar progress_bar;
    public GameObject repairing_spell;
    public GameObject repair_screen;

    public GameObject spell_list_ref;
    public List<GameObject> spell_list_list;
    public string stage_id;
    public int progress = 1;

    public GameObject health_gem;
    public GameObject mana_gem;
    public GameObject question_panel_ref;

    public Color mana_color;
    public Color attack_color;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)   //singleton instance, easy for referencing in other scripts
        {
            Instance = this;            
        }
    }

    private void Start()
    {
        StartStage();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MonsterAttackMissle(GameObject missle)
    {
        missle.GetComponent<MissleShoot>().Shoot(health_gem.transform.position);
    }

    public void RepairSpell(string skill_name, GameObject spell)
    {
        repairing_spell = spell;
        List<CommandBlock> spell_code = Player.Instance.GetSkillCode(skill_name);
        int start_index = 0;
        spell_code = Shuffle(spell_code, 3, out start_index);
        repair_screen.SetActive(true);
        repair_screen.GetComponent<SpellRepair>().block_list = repair_screen.GetComponent<CodeBlockReconstructor>().RebuildBlocksRepair(spell_code);
        repair_screen.GetComponent<SpellRepair>().repairing_spell = spell;
        repair_screen.GetComponent<SpellRepair>().InitializeRepairScreen(start_index, 3);

    }

    public List<CommandBlock> Shuffle(List<CommandBlock> original, int difficulty, out int start_index)
    {
        start_index = Random.Range(0, original.Count - difficulty);
        //Debug.Log(start_index);
        //List<CommandBlock> shuffled = new List<CommandBlock>();
        for (int i = start_index; i <= start_index + difficulty - 1; ++i)
        {
            int r = Random.Range(i, start_index + difficulty);
            CommandBlock tmp = original[i];
            original[i] = original[r];
            original[r] = tmp;
        }

        return original;
    }

    [ContextMenu("StartStage")]
    public void StartStage()
    {
        SummonMonsters(1);
        MPAutoRestore = StartCoroutine(AutoRestoreMP());
        
        for(int i = 0; i <= Player.Instance.data.spells_in_channels.Count - 1; i++)
        {
            GameObject temp = Instantiate(spell_list_ref, transform);
            float start = (Player.Instance.data.spells_in_channels.Count - 1) * 2.28f / 2 * -1;
            temp.transform.localPosition = new Vector3(start + 2.28f * i, 0, 0);
            temp.GetComponent<SpellList>().InitializeSpellList(i);
        }
    }

    [ContextMenu("UpdatePlayerStatus")]
    public void UpdatePlayerStatus()
    {
        Player_HP_Bar.fillAmount = (float)Player.Instance.health / Player.Instance.max_health;
        Player_MP_Bar.fillAmount = (float)Player.Instance.mana / Player.Instance.max_mana;
    }

    [ContextMenu("TestDamage100")]
    public void test_damage()
    {
        Player.Instance.damage(100);
        UpdatePlayerStatus();
    }

    public void damage(int damage_amt)
    {
        Debug.Log("Here");
        Player.Instance.damage(damage_amt);
        //UpdatePlayerStatus();
        Player_HP_Bar.GetComponent<BarChange>().ChangeTo((float)Player.Instance.health / Player.Instance.max_health);
        Player_HP_Bar.GetComponentInChildren<Flash>().StartFlash();
    }   

    public void heal(int heal_amt)
    {
        Player.Instance.heal(heal_amt);
        //UpdatePlayerStatus();
        Player_HP_Bar.GetComponent<BarChange>().ChangeTo((float)Player.Instance.health / Player.Instance.max_health);
    }

    public void restore(int restore_amt)
    {
        Player.Instance.restore(restore_amt);
        Player_MP_Bar.GetComponent<BarChange>().ChangeTo((float)Player.Instance.mana / Player.Instance.max_mana);
        //UpdatePlayerStatus();

    }

    public bool consume(int consume_amt)
    {
        bool enough_mana = Player.Instance.consume(consume_amt);
        //UpdatePlayerStatus();
        Player_MP_Bar.GetComponent<BarChange>().ChangeTo((float)Player.Instance.mana / Player.Instance.max_mana);
        Player_MP_Bar.GetComponentInChildren<Flash>().StartFlash();
        return enough_mana;
    }

    public void ActivateSpell(BattleSpell spell, GameObject origin)
    {
        if (consume(spell.GetAverageStep())) //player have enough mana
        {
            GameManager.Instance.MissleEffect(mana_gem, origin, mana_color);
            if (origin.GetComponent<SpellManager>().black_mist == null)
            {
                
                switch (spell.usage)
                {
                    case "attack":
                        GameObject atk_obj = Instantiate(spell.instance_reference, origin.transform.position, Quaternion.identity);
                        int target_index = Random.Range(0, monster_summoned.Count);
                        atk_obj.GetComponent<FlyingAttackObject>().target = monster_summoned[target_index];
                        atk_obj.GetComponent<FlyingAttackObject>().InitializeFAO();
                        int real_atk = Player.Instance.ATK + (int)spell.effect_value - monster_summoned[target_index].GetComponent<MonsterManager>().DEF;
                        atk_obj.GetComponent<FlyingAttackObject>().damage_carried = real_atk;
                        break;
                    case "heal":
                        heal((int)spell.effect_value);
                        break;
                    case "restore":
                        restore((int)spell.effect_value);
                        break;
                }
            }
            //else
            //{
            //    foreach (ParticleSystem ps in origin.GetComponentsInChildren<ParticleSystem>())
            //    {
            //        ps.enableEmission = false;
            //    }
            //}
        }
        if (origin.transform.childCount >= 3)
        {
            foreach (ParticleSystem ps in origin.GetComponentsInChildren<ParticleSystem>())
            {
                ps.enableEmission = false;
            }
        }
    }

    public void SummonMonsters(int no_of_monster)
    {
        monster_summoned = new List<GameObject>();
        for (int i = 0; i <= no_of_monster - 1; i++)
        {
            GameObject temp_monster = Instantiate(monster_reference, new Vector3(0, 3.5f, 0), Quaternion.identity, transform);
            temp_monster.GetComponent<MonsterManager>().monster = stage_monster[Random.Range(0, stage_monster.Count)];
            temp_monster.GetComponent<MonsterManager>().InitializeMonster();
            monster_summoned.Add(temp_monster);
        }
        
    }

    IEnumerator AutoRestoreMP()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            restore(0);
        }

    }

    public void StartQuestion()
    {
        //progress++;
        string question_id = "QUESTION" + stage_id + "-" + progress.ToString();
        GameObject temp = GameManager.Instance.SpawnWindowAtCamera(question_panel_ref);
        temp.GetComponent<QuestionPanel>().InitializeQuestionPanel(question_id);
    }

    public void ToNextStage()
    {
        //progress_bar.Progress();
        

        //StartStage();
        StartCoroutine(BattleBreak());
        //SummonMonsters(1);
    }

    IEnumerator BattleBreak()
    {
        paused = true;
        yield return new WaitForSeconds(1.5f);
        SummonMonsters(1);
        yield return new WaitForSeconds(0.5f);
        paused = false;
    }
    
}
