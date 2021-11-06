public enum Movement
{
    Update,
    Run,
    Jump,
    Landing,
    Falling,
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
    Left
}

public enum PlayerJump
{
    Grounded,
    Ascend,
    MaxHeight,
    Landing,
    Suspended
}