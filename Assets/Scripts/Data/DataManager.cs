using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    // > Player Data
    public static Data playerData;
    Stats playerStats;

        // >> Player Skills
    Skill[] playerSkills;
    Skill Lunge;
    Skill Quake;
        // <<

        // >> Checkpoint Data
    Checkpoint cPoint;
    static string cPointString;
        // <<

        // >> Cutscene Data
    public static Cutscene cutscene;
/*    public static int levelNum;
    public static int sceneNum;*/
    static string sceneString;
        // <<

    // <

    // > Rin Data
    public static Data rinData;
    // < 

    // > Enemy Data
    static string chapterName; // enemy levels will change depending on current chapter

        // >> Skeleton
    public static Data skeletonData;
        // <<

    // <

    static Stats loadedStats; // Reference to stats requested from load data


    void Awake()
    {
// > STATS
        playerStats = LoadStats(CharType.Player);
// <

        if (!PlayerPrefs.HasKey("SkillAccess_Lunge")) PlayerPrefs.SetInt("SkillAccess_Lunge", 1); // temp
        if (!PlayerPrefs.HasKey("SkillAccess_Quake")) PlayerPrefs.SetInt("SkillAccess_Quake", 1); // temp

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
        playerData = new Data(playerStats, playerSkills, cPoint, cutscene);
        skeletonData = new Data(LoadStats(CharType.Skeleton));
// <
    }

    public static void Save (CharType cType, Data newData, bool saveToFile = false)
    {

        Stats stats = newData.stats;

        Checkpoint cPoint;
        Skill[] skills;
        Cutscene cutscene;

        if (cType == CharType.Player)
        {
            cPoint = newData.cPoint;
            skills = newData.skills;
            cutscene = newData.cutscene;
            Debug.Log(newData.cutscene);

            // > checkpoint
            switch (cPoint)
            {
                case Checkpoint.Start:

                    cPointString = "c0";

                    PlayerPrefs.SetString("CurrentCheckpoint", cPointString);

                    break;

                // > Level 01 Checkpoint 01
                case Checkpoint.L01_01:

                    cPointString = "c01_01";

                    PlayerPrefs.SetString("CurrentCheckpoint", cPointString);

                    break;

                default:
                    break;

            }
            // <

            // > stats
            PlayerPrefs.SetInt("Player_HP", stats.HP);
            PlayerPrefs.SetInt("Player_MP", stats.MP);
            PlayerPrefs.SetFloat("Player_ATK", stats.ATK);
            PlayerPrefs.SetFloat("Player_DEF", stats.DEF);
            // <

            // > skills
            foreach (Skill skill in skills)
            {
                PlayerPrefs.SetInt($"SkillAccess_{skill.name}", skill.isAccessible ? 1 : 0);
                PlayerPrefs.SetInt($"MPCost_{skill.name}", skill.requiredMP);
                PlayerPrefs.SetFloat($"SkillDamage_{skill.name}", skill.DMG);
            }
            // <

            // > cutscene
            switch (cutscene)
            {
                case Cutscene.S01_01:

                    PlayerPrefs.SetString("CurrentCutscene", "S01_01");

                    break;

                case Cutscene.S01_02:

                    PlayerPrefs.SetString("CurrentCutscene", "S01_02");

                    break;

                default:
                    break;

            }
            // <

            // Debug.Log("Lunge: " + playerData.skills[0].isSelected);
            // Debug.Log("Quake: " + playerData.skills[1].isSelected);
            playerData = newData;
            Debug.Log("Lunge: " + playerData.skills[0].isSelected);
            Debug.Log("Quake: " + playerData.skills[1].isSelected);
        }
        else if (cType == CharType.Rin)
        {

        }


        if (saveToFile)
        {
            PlayerPrefs.Save();
        }

    }

    void Restart ()
    {

    }

    void LoadNextLevel ()
    {

    }

    Stats LoadStats(CharType cType)
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
                attack = PlayerPrefs.HasKey("Player_ATK") ? PlayerPrefs.GetFloat("Player_ATK") : 1.5f;
                defense = PlayerPrefs.HasKey("Player_DEF") ? PlayerPrefs.GetFloat("Player_DEF") : 1;

                loadedStats = new Stats(health, magic, attack, defense);

                return loadedStats;

            case CharType.Skeleton:

                chapterName = SceneManager.GetActiveScene().name;

                if (chapterName == "ZerxisGraveyard")
                {
                    health = 3;
                    attack = 1.0f;
                    defense = 1.0f;

                    loadedStats = new Stats(health, attack, defense);
                }
                else if (chapterName == "MagusForest")
                {

                }
                

                return loadedStats;


            default:
                return null;
        }

    }

    Cutscene LoadCutscene ()
    {
        sceneString = PlayerPrefs.HasKey("CurrentCutscene") ? PlayerPrefs.GetString("CurrentCutscene") : "S01_01";

        switch (sceneString)
        {
            case "S01_01":

                cutscene = Cutscene.S01_01;

                return cutscene;

            case "S01_02":

                cutscene = Cutscene.S01_02;

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

            case "c01_01":

                return Checkpoint.L01_01;

            default:
                return Checkpoint.Start;
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

                info = PlayerPrefs.HasKey("SkillInfo_Lunge") ? PlayerPrefs.GetString("SkillInfo_Lunge") : 
                    "";

                requiredMP = PlayerPrefs.HasKey("MPCost_Lunge") ? PlayerPrefs.GetInt("MPCost_Lunge") : 10;
                DMG = PlayerPrefs.HasKey("SkillDamage_Lunge") ? PlayerPrefs.GetFloat("SkillDamage_Lunge") : 0;
                isAccessible = PlayerPrefs.HasKey("SkillAccess_Lunge") && PlayerPrefs.GetInt("SkillAccess_Lunge") == 1;

                Lunge = new Skill(skillName, info, requiredMP, DMG, isAccessible);

                return Lunge;

            case "Quake":

                info = PlayerPrefs.HasKey("SkillInfo_Quake") ? PlayerPrefs.GetString("SkillInfo_Quake") : 
                    "";

                requiredMP = PlayerPrefs.HasKey("MPCost_Quake") ? PlayerPrefs.GetInt("MPCost_Quake") : 30;
                DMG = PlayerPrefs.HasKey("SkillDamage_Quake") ? PlayerPrefs.GetFloat("SkillDamage_Quake") : 0;
                isAccessible = PlayerPrefs.HasKey("SkillAccess_Quake") && PlayerPrefs.GetInt("SkillAccess_Quake") == 1;

                Quake = new Skill(skillName, info, requiredMP, DMG, isAccessible);

                return Quake;

            default:
                return new Skill();

        }

    }

}
