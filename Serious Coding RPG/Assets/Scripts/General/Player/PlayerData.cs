using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
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
    public List<int> completedTask;
    public List<Skill> skills;
    public string language;
    

    public PlayerData()    //initialize a new player
    {
        gender = Gender.Male;
        name = "Nameless wizard";
        level = 1;
        EXP = 0;
        gold = 0;
        completedTask = new List<int>();
        skills = new List<Skill>();
        language = "en";
        
    }

    public PlayerData(Player player)    //get data from player
    {
        this.gender = player.data.gender;
        this.name = player.data.name;
        this.level = player.data.level;
        this.EXP = player.data.EXP;
        this.gold = player.data.gold;
        this.completedTask = new List<int>(player.data.completedTask);
        this.skills = new List<Skill>(player.data.skills);
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
