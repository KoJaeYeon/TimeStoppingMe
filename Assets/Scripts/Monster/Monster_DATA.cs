using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable_Range", menuName ="Monster")]
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

    [Header("원거리 : 투사체 최대 거리\n근거리 : 공격 콜라이더 최대 거리설정")]
    public float projectile1_EndDistance;
    [Header("원거리 : 곡사 투사체 최대 높이")]
    public float projectile1_EndHeight;
    [Header("원거리 : 일반 투사체 최대 속도")]
    public float projectile1_Speed;
}
