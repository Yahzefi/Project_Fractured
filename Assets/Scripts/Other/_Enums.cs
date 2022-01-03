using UnityEngine;

//      ANIMATION       \\
public enum Movement
{
    Spawn,
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
// Level One | Zerxis Graveyard
    S01_01,
    S01_02,
    S01_03,
    S01_04,
    S01_05,
// Level Two | Magus Forest
    S02_01
}

//      DATA MANAGEMENT     \\
public enum CharType
{
    Player,
    Rin,
    Skeleton

}

public enum Checkpoint
{
    Start,
    L01_01,
}

//      UI      \\
public enum HUD
{
    Start,
    PlayerHealth,
    PlayerMagic,
    SkillsUI
}
