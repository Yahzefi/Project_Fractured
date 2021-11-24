using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    static Stats playerStats;
    static Checkpoint cPoint;

// > player health
    static int health;
/*    static GameObject health20;
    static GameObject health40;
    static GameObject health60;
    static GameObject health80;
    static GameObject health100;*/
// <

// > player energy
    static int magic;
/*    static GameObject magic10;
    static GameObject magic20;
    static GameObject magic30;
    static GameObject magic40;
    static GameObject magic50;
    static GameObject magic60;
    static GameObject magic70;
    static GameObject magic80;
    static GameObject magic90;
    static GameObject magic100;*/
    // <

    // > player atk
    // \\
    // <
    // > player def
    // \\
    // <
    // > player level
    // \\
    // <
    // > player buffs
    // \\
    // <
    // > player debuffs
    // \\
    // <
    // > player HUD

    // >> player XP bar/number
    // \\
    // <<
    // >> player skill bar
    // \\
    // <<

    // <

    private void Start()
    {

        playerStats = DataManager.playerData.stats;
        cPoint = DataManager.playerData.cPoint;

        health = playerStats.HP;
        magic = playerStats.MP;

    }

    public static void ApplyDamage(int damage)
    {

        health -= damage;
        playerStats.HP = health;

        HUDManager.UpdateHUD(HUD.PlayerHealth, health, null);

        if (health <= 0) Die();

    }

    public static void DeductMP (int requiredMP)
    {   

        magic -= requiredMP;
        playerStats.MP = magic;

        HUDManager.UpdateHUD(HUD.PlayerMagic, null, magic);

    }

    public static void Heal(int healAmount)
    {
        health += healAmount;
        playerStats.HP = health;

        HUDManager.UpdateHUD(HUD.PlayerHealth, health, null);

    }

    public static void ApplyBuff()
    {

    }

    public static void ApplyDebuff()
    {

    }

    static void Die()
    {
        Debug.Log("Player has died");
        playerStats.HP = cPoint == Checkpoint.Start ? 100 : playerStats.HP;
        playerStats.MP = cPoint == Checkpoint.Start ? 100 : playerStats.MP;
        DataManager.Save(cPoint, playerStats);
        // death animation
        // restart scene/level
    }

}
