#region Monster
public enum MonsterState
{
    Idle,
    Tracking,
    Attack
}
#endregion

#region Weapon
public enum ProjectilePattern // 병렬형, 비산형
{
    Parallel,
    Spread
}

public enum FireMode // 연발, 단발
{
    Single,
    Burst
}
#endregion