using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public Stats stats;

    public Skill[] skills;
    public Checkpoint cPoint;
    public Cutscene cutscene;

    public CharType cType;

    public Data(Stats stats, Skill[] skills, Checkpoint cPoint, Cutscene cutscene)
    {
        this.stats = stats;
        this.skills = skills;
        this.cPoint = cPoint;
        this.cutscene = cutscene;
    }

    public Data(Stats stats)
    {
        this.stats = stats;
    }

}
