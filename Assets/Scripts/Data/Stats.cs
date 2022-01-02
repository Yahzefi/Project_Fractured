using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public int HP;
    public int MP;
    public float ATK;
    public float DEF;

    /// <summary>
    /// Player Stats
    /// </summary>
    public Stats(int health, int magic, float attack, float defense)
    {
        HP = health;
        MP = magic;
        ATK = attack;
        DEF = defense;
    }

    /// <summary>
    /// Stats for a skeleton enemy
    /// </summary>
    /// <param name="spawnLevel">Determines how high or low the enemy stats will be</param>
    /// <param name="health"></param>
    /// <param name="attack"></param>
    /// <param name="defense"></param>
    public Stats(int health, float attack, float defense)
    {
        this.HP = health;
        this.ATK = attack;
        this.DEF = defense;
    }

}
