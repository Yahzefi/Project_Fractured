using UnityEngine;

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

    static GameObject health01 = GameObject.Find("Health_01");
    static GameObject health02 = GameObject.Find("Health_02");
    static GameObject health03 = GameObject.Find("Health_03");
    static GameObject health04 = GameObject.Find("Health_04");
    static GameObject health05 = GameObject.Find("Health_05");

    static GameObject magic01 = GameObject.Find("Magic_01");
    static GameObject magic02 = GameObject.Find("Magic_02");
    static GameObject magic03 = GameObject.Find("Magic_03");
    static GameObject magic04 = GameObject.Find("Magic_04");
    static GameObject magic05 = GameObject.Find("Magic_05");
    static GameObject magic06 = GameObject.Find("Magic_06");
    static GameObject magic07 = GameObject.Find("Magic_07");
    static GameObject magic08 = GameObject.Find("Magic_08");
    static GameObject magic09 = GameObject.Find("Magic_09");
    static GameObject magic10 = GameObject.Find("Magic_10");

    static GameObject activeUI_Lunge = GameObject.Find("activeStatus_Lunge");
    static GameObject activeUI_Quake = GameObject.Find("activeStatus_Quake");

    public static void UpdateHUD (HUD hType)
    {
        if (hType == HUD.Start)
        {
            activeUI_Lunge.SetActive(false);
            activeUI_Quake.SetActive(false);
        }
        else return;

    }

    public static void UpdateHUD (HUD hType, int? HP, int? MP)
    {
        switch (hType)
        {
            case HUD.PlayerHealth:

                health05.SetActive(HP == 5);
                health04.SetActive(HP >= 4);
                health03.SetActive(HP >= 3);
                health02.SetActive(HP >= 2);
                health01.SetActive(HP >= 1);

                break;

            case HUD.PlayerMagic:

                magic10.SetActive(false);
                magic09.SetActive(MP >= 9);
                magic08.SetActive(MP >= 8);
                magic07.SetActive(MP >= 7);
                magic06.SetActive(MP >= 6);
                magic05.SetActive(MP >= 5);
                magic04.SetActive(MP >= 4);
                magic03.SetActive(MP >= 3);
                magic02.SetActive(MP >= 2);
                magic01.SetActive(MP >= 1);

                break;

            default:
                break;

        }
    }

    public static void UpdateHUD (HUD hType, int skillNum)
    {
        if (hType == HUD.SkillsUI)
        {
            switch (skillNum)
            {
                case 1:

                    activeUI_Lunge.SetActive(!activeUI_Lunge.activeSelf);

                    activeUI_Quake.SetActive(false);
                    // skill 3
                    // skill 4

                    break;

                case 2:

                    activeUI_Quake.SetActive(!activeUI_Quake.activeSelf);

                    activeUI_Lunge.SetActive(false);
                    // skill 3
                    // skill 4

                    break;

                case 3:

                    //

                    break;

                case 4:

                    //

                    break;

                default:
                    break;

            }
        }
        else return;

    }

}
