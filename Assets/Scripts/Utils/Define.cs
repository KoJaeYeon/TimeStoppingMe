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
    // �߰� ȿ�� Ÿ��
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