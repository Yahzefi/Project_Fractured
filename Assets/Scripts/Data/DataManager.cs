using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Data playerData;

    Stats playerStats;
    Skill[] playerSkills;
    Checkpoint cPoint;
    public static Cutscene cutscene;

    Skill Lunge;
    Skill Quake;

    static string sceneString;
    static string cPointString;

    void Awake()
    {
// > STATS
        playerStats = LoadStats(CharType.Player);
// <
// > SKILLS
        Lunge = LoadSkill("Lunge");
        Quake = LoadSkill("Quake");
        playerSkills = new Skill[] { Lunge, Quake };
// <
// > ITEMS / INVENTORY
        //
// <
// > SELECTED CUTSCENE
        cutscene = LoadCutscene();
// <
// > CURRENT CHECKPOINT
        cPoint = LoadCheckpoint();
// <
// > DATA
        playerData = new Data(playerStats, playerSkills, cPoint);
// <
    }

    public static void Save (Checkpoint cPoint, Stats stats)
    {
        switch (cPoint)
        {
            case Checkpoint.Start:

                cPointString = "c0";
                PlayerPrefs.SetString("CurrentCheckpoint", cPointString);

                break;

// > Level 00 Checkpoint 01
            case Checkpoint.L00_01:

                cPointString = "c00_01";
                PlayerPrefs.SetString("CurrentCheckpoint", cPointString);

                break;

            default:
                break;

        }

        PlayerPrefs.SetInt("Player_HP", stats.HP);
        PlayerPrefs.SetInt("Player_MP", stats.MP);
        PlayerPrefs.SetFloat("Player_ATK", stats.ATK);
        PlayerPrefs.SetFloat("Player_DEF", stats.DEF);

        PlayerPrefs.Save();

    }

    void Restart ()
    {

    }

    void LoadNextLevel ()
    {

    }

    Cutscene LoadCutscene ()
    {
        sceneString = PlayerPrefs.HasKey("CurrentCutscene") ? PlayerPrefs.GetString("CurrentCutscene") : "S00_01";

        switch (sceneString)
        {
            case "S00_01":

                //

                return cutscene;

            default:
                return cutscene;

        }

    }
    Checkpoint LoadCheckpoint ()
    {
        cPointString = PlayerPrefs.HasKey("CurrentCheckpoint") ? PlayerPrefs.GetString("CurrentCheckpoint") : "c0";

        switch (cPointString)
        {
            case "c0":

                return Checkpoint.Start;

            default:
                return Checkpoint.Start;
        }
    }

    Stats LoadStats (CharType cType)
    {
        int health;
        int magic;
        float attack;
        float defense;

        switch (cType)
        {
            case CharType.Player:

                health = PlayerPrefs.HasKey("Player_HP") ? PlayerPrefs.GetInt("Player_HP") : 100;
                magic = PlayerPrefs.HasKey("Player_MP") ? PlayerPrefs.GetInt("Player_MP") : 100;
                attack = PlayerPrefs.HasKey("Player_ATK") ? PlayerPrefs.GetFloat("Player_ATK") : 1;
                defense = PlayerPrefs.HasKey("Player_DEF") ? PlayerPrefs.GetFloat("Player_DEF") : 1;

                Stats loadedStats = new Stats(health, magic, attack, defense);

                return loadedStats;

            default:
                return new Stats();
        }

    }

    Skill LoadSkill (string skillName)
    {
        string info;
        float DMG;
        bool isAccessible; // do you have it?
        int requiredMP;

        switch (skillName)
        {
            case "Lunge":

                info = PlayerPrefs.HasKey("SkillInfo_Lunge") ? PlayerPrefs.GetString("SkillInfo_Lunge") : "";
                requiredMP = PlayerPrefs.HasKey("MPCost_Lunge") ? PlayerPrefs.GetInt("MPCost_Lunge") : 10;
                DMG = PlayerPrefs.HasKey("SkillDamage_Lunge") ? PlayerPrefs.GetFloat("SkillDamage_Lunge") : 0;
                isAccessible = PlayerPrefs.HasKey("SkillAccess_Lunge") && PlayerPrefs.GetInt("SkillAccess_Lunge") == 1;

                Lunge = new Skill(skillName, info, requiredMP, DMG);

                return Lunge;

            case "Quake":

                info = PlayerPrefs.HasKey("SkillInfo_Quake") ? PlayerPrefs.GetString("SkillInfo_Quake") : "";
                requiredMP = PlayerPrefs.HasKey("MPCost_Quake") ? PlayerPrefs.GetInt("MPCost_Quake") : 30;
                DMG = PlayerPrefs.HasKey("SkillDamage_Quake") ? PlayerPrefs.GetFloat("SkillDamage_Quake") : 0;
                isAccessible = PlayerPrefs.HasKey("SkillAccess_Quake") && PlayerPrefs.GetInt("SkillAccess_Quake") == 1;

                Quake = new Skill(skillName, info, requiredMP, DMG);

                return Quake;

            default:
                return new Skill();

        }

    }

/*    Skill[] LoadSkills (string[] skillNames)
    {

        Skill[] loadedSkills = new Skill[skillNames.Length];

        for (int i = 0; i < skillNames.Length; i++)
        {
            Skill currSkill = LoadSkill(skillNames[i]);
            loadedSkills.SetValue(currSkill, i);
        }

        return loadedSkills;

    }*/

}
