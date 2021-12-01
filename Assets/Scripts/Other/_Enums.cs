using UnityEngine;

//      ANIMATION       \\
public enum Movement
{
    Walk,
    Run,
    Jump,
    Landing,
    Falling,
    Attack,
    Hit,
    Death
}

public enum Direction
{
    Idle,
    Left,
    Right,
    Up,
    Down
}

public enum PlayerRun
{
    Right,
    Left,
    Idle
}

public enum PlayerJump
{
    Grounded,
    Ascend,
    MaxHeight,
    Landing,
    Suspended
}

//      CUTSCENES       \\
public enum Cutscene
{
    S00_01,
    S00_02,
    S01_01,
}

//      DIALOGUE        \\
public enum Script
{
    Intro00_01,
    Intro00_02,
    InteractionWithNPC01,
    InteractionWithNPC02
}

//      DATA MANAGEMENT     \\
public enum CharType
{
    Player,
    Enemy01,
    Enemy02
}

public enum Checkpoint
{
    Start,
    L00_01,
}

//      UI      \\
public enum HUD
{
    Start,
    PlayerHealth,
    PlayerMagic,
    SkillsUI
}
