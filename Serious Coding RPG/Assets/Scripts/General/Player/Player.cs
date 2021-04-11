using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public PlayerData data;
    public GameObject character;

    //---------------Stats------------------
    public int health;
    public int max_health;

    public int mana;
    public int max_mana;

    public int ATK;
    public int DEF;

    public int spell_channels;
    public int chanting_speed;
    public int max_equip_spells;

    [ContextMenu("TestAddSpellChannels")]
    public void TestAddSpells()
    {
        data.TestAddSpellChannels();
    }

    public List<CommandBlock> GetSkillCode(string skill_name)
    {
        foreach(Skill skill in data.skills)
        {
            if(skill_name == skill.name)
            {
                return skill.GetOriginalCommandBlockList();
            }
        }
        return null;
    }


    // Start is called before the first frame update
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

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        data = LoadData();     //load player data
        InitializeCalculatableData();
    }   

    public bool checkQuestStatus(int id)    //0 = not finished, 1 = finished
    {
        foreach(int questid in data.completedTask)
        {
            if(id == questid)
            {
                return true;
            }
        }
        return false;
    }

    public PlayerData LoadData()
    {
        return SaveLoad.Load();
        
    }

    void InitializeCalculatableData()
    {
        max_health = health = (data.level - 1) * 50 + 100;
        max_mana = mana = (data.level - 1) * 50 + 100;
        ATK = (data.level - 1) * 5 + 35;
        DEF = (data.level - 1) * 3 + 20;
        spell_channels = Mathf.Min(5, data.level / 7 + 1);
        chanting_speed = (data.level - 1) * 5 + 15;
        max_equip_spells = (data.level - 1) * 1 + 3;
    }

    public void SaveData()
    {

        SaveLoad.Save(this);

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveLoad.Save(this);           
        }
        
    }

    void OnApplicationQuit()
    {
        SaveLoad.Save(this);        
    }

    //----------------Game Event---------------------------------------
    public bool damage(int damage_amt)
    {
        health -= damage_amt;
        if(health <= 0)
        {
            health = 0;
            die();
            return false;    //no health left, can't continue adventure
        }
        return true;      //still have health left, can continue adventure
    }

    public void die()
    {
        //die
        Debug.Log("You died!");
    }

    public void revive()
    {
        health = 1;
    }

    public void heal(int heal_amt)
    {
        health += heal_amt;
        if(health > max_health)
        {
            health = max_health;
        }
    }

    public void restore(int restore_amt)
    {
        mana += restore_amt;
        if(mana > max_mana)
        {
            mana = max_mana;
        }
    }

    public bool consume(int consume_amt)
    {
        mana -= consume_amt;
        if(mana < 0)
        {
            mana += consume_amt;
            return false;        //cast spell fail, not enough mana
        }
        return true;      //cast spell successfully, mana consumed
    }
}
