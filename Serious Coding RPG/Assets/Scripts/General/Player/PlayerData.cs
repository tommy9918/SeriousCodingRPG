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

    public PlayerData()    //initialize a new player
    {
        gender = Gender.Male;
        name = "Nameless wizard";
    }

    public PlayerData(Player player)    //get data from player
    {
        this.gender = player.data.gender;
        this.name = player.data.name;
    }
}
