using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/Monster", order = 1)]
public class Monster : ScriptableObject
{
    public Sprite sprite;
    public string monster_id;
    public int level;
    public int attack_interval;
    public int ATK;
    public int DEF;
    public int health;

}
