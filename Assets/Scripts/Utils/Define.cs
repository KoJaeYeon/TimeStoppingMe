#region Monster
public enum MonsterState
{
    Idle,
    Tracking,
    Attack
}
#endregion

#region Weapon
public enum ProjectilePattern // ������, �����
{
    Parallel,
    Spread
}

public enum FireMode // ����, �ܹ�
{
    Single,
    Burst
}
#endregion