using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    [System.Serializable]
    public class PlayerSpellList
    {
        public List<PlayerSpell> spell_list;

        public PlayerSpellList(List<PlayerSpell> og_spell_list)
        {
            spell_list = og_spell_list;
        }

        public PlayerSpellList()
        {
            spell_list = new List<PlayerSpell>();
        }
    }
    public enum Gender
    {
        Male,
        Female
    }
    public Gender gender;
    public string name;
    public int level;
    public int EXP;
    public int gold;
    public List<string> completedTask;
    public List<Skill> skills;
    public List<string> completedStage;
    public List<string> equiped_spells_id;
    public List<PlayerSpell> all_spells;
    public List<int> channel_chanting_speed;
    public List<PlayerSpellList> spells_in_channels;
    public List<string> unlocked_blocks;
    public int spell_channels;
    public string language;
    public string location;

    
    
    

    public PlayerData()    //initialize a new player
    {
        gender = Gender.Male;
        name = "Nameless wizard";
        level = 1;
        EXP = 0;
        gold = 0;
        completedTask = new List<string>();
        skills = new List<Skill>();
        completedStage = new List<string>();
        equiped_spells_id = new List<string>();
        all_spells = new List<PlayerSpell>();
        channel_chanting_speed = new List<int>();
        spells_in_channels = new List<PlayerSpellList>();
        unlocked_blocks = new List<string>();
        spell_channels = 2;
        location = "Village1";

        language = "en";
        
    }

    public PlayerData(Player player)    //get data from player
    {
        this.gender = player.data.gender;
        this.name = player.data.name;
        this.level = player.data.level;
        this.EXP = player.data.EXP;
        this.gold = player.data.gold;
        this.completedTask = new List<string>(player.data.completedTask);
        this.skills = new List<Skill>(player.data.skills);
        this.completedStage = new List<string>(player.data.completedStage);
        this.equiped_spells_id = new List<string>(player.data.equiped_spells_id);
        this.all_spells = new List<PlayerSpell>(player.data.all_spells);
        this.channel_chanting_speed = new List<int>(player.data.channel_chanting_speed);
        this.spells_in_channels = new List<PlayerSpellList>(player.data.spells_in_channels);
        this.unlocked_blocks = new List<string>(player.data.unlocked_blocks);
        this.spell_channels = 2;
        this.location = player.data.location;

        this.language = player.data.language;
        
    }

    string LanguageFileTarget(SystemLanguage language)
    {
        switch (language)
        {
            case SystemLanguage.English:
                return "en";
            case SystemLanguage.Chinese:
                return "ch";
        }
        return "en";
    }


}
