using UnityEngine;

public struct Data
{

    public Stats stats;
    public Skill[] skills;
    public Checkpoint cPoint;

    public Data(Stats stats, Skill[] skills, Checkpoint cPoint)
    {
        this.stats = stats;
        this.skills = skills;
        this.cPoint = cPoint;
    }

}
public struct Stats
{
    public int HP;
    public int MP;
    public float ATK;
    public float DEF;

    public Stats(int health, int energy, float attack, float defense)
    {
        HP = health;
        MP = energy;
        ATK = attack;
        DEF = defense;
    }

}

public struct Skill
{

    public string name;
    public string info;

    public int requiredMP;
    public float DMG;

    public bool isAccessible; // do you have it?
    public bool isSelected; // is it the selected/chosen skill?

    public Skill(string skillName, string skillInfo = "", int neededMP = 10, float skillDamage = 0, bool hasSkill = false, bool hasSelected = false)
    {
        name = skillName;
        info = skillInfo;
        requiredMP = neededMP;
        DMG = skillDamage;
        isAccessible = hasSkill;
        isSelected = hasSelected;

    }

}

public struct HUDManager
{

    static GameObject health20 = GameObject.Find("Health_20");
    static GameObject health40 = GameObject.Find("Health_40");
    static GameObject health60 = GameObject.Find("Health_60");
    static GameObject health80 = GameObject.Find("Health_80");
    static GameObject health100 = GameObject.Find("Health_100");

    static GameObject magic10 = GameObject.Find("Magic_10");
    static GameObject magic20 = GameObject.Find("Magic_20");
    static GameObject magic30 = GameObject.Find("Magic_30");
    static GameObject magic40 = GameObject.Find("Magic_40");
    static GameObject magic50 = GameObject.Find("Magic_50");
    static GameObject magic60 = GameObject.Find("Magic_60");
    static GameObject magic70 = GameObject.Find("Magic_70");
    static GameObject magic80 = GameObject.Find("Magic_80");
    static GameObject magic90 = GameObject.Find("Magic_90");
    static GameObject magic100 = GameObject.Find("Magic_100");

/*    static GameObject[] healthSlots = new GameObject[]
    {
        health20,
        health40,
        health60,
        health80,
        health100

    };

    static GameObject[] magicSlots = new GameObject[]
    {
        magic10,
        magic20,
        magic30,
        magic40,
        magic50,
        magic60,
        magic70,
        magic80,
        magic90,
        magic100
    };*/


    public static void UpdateHUD (HUD hType, int? HP, int? MP)
    {
        switch (hType)
        {
            case HUD.PlayerHealth:

                health100.SetActive(HP == 100);
                health80.SetActive(HP >= 80);
                health60.SetActive(HP >= 60);
                health40.SetActive(HP >= 40);
                health20.SetActive(HP >= 20);

                break;

            case HUD.PlayerMagic:

                magic100.SetActive(false);
                magic90.SetActive(MP >= 90);
                magic80.SetActive(MP >= 80);
                magic70.SetActive(MP >= 70);
                magic60.SetActive(MP >= 60);
                magic50.SetActive(MP >= 50);
                magic40.SetActive(MP >= 40);
                magic30.SetActive(MP >= 30);
                magic20.SetActive(MP >= 20);
                magic10.SetActive(MP >= 10);

                break;

            default:
                break;

        }
    }
}
