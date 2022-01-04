using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    static Data playerData;
    static Stats playerStats;
    //static Checkpoint cPoint;
    static int[] cPoint;

    // > player health
    static int health;
// <

// > player energy
    static int magic;
    // <

    private void Start()
    {

        playerData = DataManager.playerData;
        playerStats = playerData.stats;
        cPoint = playerData.cPoint;

        health = playerStats.HP;
        magic = playerStats.MP;

        HUDManager.UpdateHUD(HUD.Start);

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
        playerStats.HP = cPoint[0] == 1 && cPoint[1] == 0 ? 100 : playerStats.HP;
        playerStats.MP = cPoint[0] == 1 && cPoint[1] == 0 ? 100 : playerStats.MP;

        playerData.stats = playerStats;
        playerData.cPoint = cPoint;

        DataManager.Save(CharType.Player, playerData, true);
        // death animation
        // restart scene/level
    }

}
