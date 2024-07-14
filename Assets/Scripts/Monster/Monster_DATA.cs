using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_DATA : ScriptableObject
{
    public float AttackDistance;
    public float TrackDistance;

    public float MaxHealth;
    public int Damage;

    public float MoveSpeed;
    public float AngularSpeed;

    [Header("대기상태 탐색 범위")]
    public float IdleRange_Min;
    public float IdleRange_Max;

    [Header("원거리, 근거리 : 부채꼴 공격범위")]
    public float Search_Range;
}

[CreateAssetMenu(fileName = "Monster_DATA_Range", menuName = "Monster/Range")]
public class Monster_DATA_Range : Monster_DATA
{
    [Header("원거리 : 투사체 최대 거리")]
    public float projectile1_EndDistance;
    [Header("원거리 : 곡사 투사체 최대 높이")]
    public float projectile1_EndHeight;
}


[CreateAssetMenu(fileName = "Monster_Data_Melee", menuName = "Monster/Melee")]
public class Monster_Data_Melee : Monster_DATA
{
    [Header("근거리 : 공격 콜라이더 최대 거리설정")]
    public float attack_MaxDistance;
}


[CreateAssetMenu(fileName = "Monster_Data_Boss", menuName = "Monster/Boss")]
public class Monster_Data_Boss : Monster_DATA
{
    public float projectile2_Damage;
}
