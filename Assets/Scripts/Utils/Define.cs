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

public enum DebuffType
{
    Burn,
    Poison,
    Mind
}

public enum BuffType
{

}
#endregion

#region Item
public enum ItemEffectType
{
    IncreaseDamage,
    IncreaseFireRate,
    ChangeProjectile,
    // 추가 효과 타입
}

#endregion

#region GameState
public enum GameState
{
    StartStage,
    CombatStage,
    BossStage
}
#endregion