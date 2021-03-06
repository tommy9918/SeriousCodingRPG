﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest", order = 1)]
public class Quest : ScriptableObject
{   
    public string questNPCId;
    public string questId;    

    public string[] input;
    public string[] output;

    public int rewardEXP;
    public int rewardGold;

    public string skill_name;
    public int input_slots_length;

    public string prerequisite;
}
