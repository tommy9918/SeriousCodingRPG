﻿using System.Collections;
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

    [ContextMenu("StartStage")]
    public void StartStage()
    {
        SummonMonsters(1);
        MPAutoRestore = StartCoroutine(AutoRestoreMP());
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
        Player.Instance.damage(damage_amt);
        UpdatePlayerStatus();
    }   

    public void heal(int heal_amt)
    {
        Player.Instance.heal(heal_amt);
        UpdatePlayerStatus();
    }

    public void restore(int restore_amt)
    {
        Player.Instance.restore(restore_amt);
        UpdatePlayerStatus();
    }

    public void consume(int consume_amt)
    {
        Player.Instance.consume(consume_amt);
        UpdatePlayerStatus();
    }

    public void ActivateSpell(BattleSpell spell, GameObject origin)
    {
        consume(spell.average_steps);
        //Debug.Log(origin.transform.childCount);
        if (origin.transform.childCount < 4)
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
        else
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
            restore(5);
        }

    }

    public void ToNextStage()
    {
        progress_bar.Progress();
        StartStage();
    }
    
}
