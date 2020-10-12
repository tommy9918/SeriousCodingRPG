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
    public List<int> completedTask;

    public PlayerData()    //initialize a new player
    {
        gender = Gender.Male;
        name = "Nameless wizard";
        completedTask = new List<int>();
    }

    public PlayerData(Player player)    //get data from player
    {
        this.gender = player.data.gender;
        this.name = player.data.name;
        this.completedTask = new List<int>(player.data.completedTask);
    }

    
}
