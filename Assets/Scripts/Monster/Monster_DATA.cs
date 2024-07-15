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